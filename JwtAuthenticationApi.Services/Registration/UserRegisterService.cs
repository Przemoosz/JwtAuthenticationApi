namespace JwtAuthenticationApi.Services.Registration
{
	using Abstraction.Identity.User;
	using JwtAuthenticationApi.Common.Abstraction.Commands;
	using JwtAuthenticationApi.Common.Abstraction.Handlers;
	using JwtAuthenticationApi.Common.Models;
	using Infrastructure.Entities;
	using JwtAuthenticationApi.Security.Abstraction.Cryptography;
	using JwtAuthenticationApi.Security.Abstraction.Salt;
	using JwtAuthenticationApi.Services.Abstraction.Registration;
	using JwtAuthenticationApi.Services.Abstraction.Validators.Password;
	using Commands;
	using JwtAuthenticationApi.Services.Models.Registration.Requests;
	using JwtAuthenticationApi.Services.Models.Registration.Responses;
	using Microsoft.EntityFrameworkCore;
	using Models.Enums;
	using Serilog;

	/// <summary>
	/// Service that is responsible for registering user.
	/// </summary>
	internal class UserRegisterService : IUserRegisterService
    {
        private readonly ISaltService _saltService;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ICommandHandler _commandHandler;

        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserRegisterService(ISaltService saltService,
            IPasswordValidator passwordValidator,
            IPasswordHashingService passwordHashingService,
            ICommandHandler commandHandler,
            IUserService userService,
            ILogger logger)
        {
            _saltService = saltService;
            _passwordValidator = passwordValidator;
            _passwordHashingService = passwordHashingService;
            _commandHandler = commandHandler;
            _userService = userService;
            _logger = logger;
        }

        public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
        {
            int? userId;
            try
            {
                if (!_passwordValidator.Validate(registerUserRequest.Password,
                        registerUserRequest.PasswordConfirmation))
                {
                    return new RegisterUserResponse()
                    {
                        IsSuccessful = false,
                        ErrorMessage =
                            "Error occurred during password validation - Provided password or password confirmation is now valid.",
                        ErrorType = ErrorType.PasswordValidationError
                    };
                }

                string salt = _saltService.GenerateSalt();
                string hashedPassword =
                    await _passwordHashingService.HashPasswordAsync(registerUserRequest.Password, salt,
                        cancellationToken);
                ICommand<UserEntity> command = new ConvertRequestToUserEntityCommand(registerUserRequest, hashedPassword);
                Result<UserEntity> userEntity = await _commandHandler.HandleAsync(command, cancellationToken);
                if (!userEntity.IsSuccessful)
                {
                    return new RegisterUserResponse()
                    {
                        IsSuccessful = false,
                        ErrorMessage = "Error occurred during entity creation",
                        ErrorType = ErrorType.InternalError
                    };
                }

                // Save user model
                userId = await _userService.SaveUserAsync(userEntity.Value, cancellationToken);

                // Save salt
                if (userId.HasValue)
                {
                    await _saltService.SaveSaltAsync(salt, userId.Value, cancellationToken);
                }
                else
                {
                    return new RegisterUserResponse()
                    {
                        IsSuccessful = false,
                        ErrorMessage = "Error occurred during saving user in database - No Id retrieved for new user.",
                        ErrorType = ErrorType.DbError
                    };
                }
            }
            catch (DbUpdateException exception)
            {
	            if (await _userService.UserExistsAsync(registerUserRequest.Username))
	            {
		            _logger.Error($"User with username {registerUserRequest.Username} already exists in database");
					return new RegisterUserResponse()
		            {
			            IsSuccessful = false,
			            ErrorMessage = $"User with username {registerUserRequest.Username} already exists in database. Change username.",
			            ErrorType = ErrorType.DbErrorEntityExists
		            };
				}

                _logger.Error(exception, "Error occurred during database update");
                return new RegisterUserResponse()
                {
                    IsSuccessful = false,
                    ErrorMessage = "Error occurred during database update",
                    ErrorType = ErrorType.DbError
                };
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unhandled Exception occurred during user registration process");
                return new RegisterUserResponse()
                {
                    IsSuccessful = false,
                    ErrorMessage = "Unhandled Exception occurred during user registration process",
                    ErrorType = ErrorType.InternalError
                };
            }

            return new RegisterUserResponse()
            {
                IsSuccessful = true,
                UserId = userId.Value,
            };
        }


    }
}
