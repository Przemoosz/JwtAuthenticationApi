﻿using JwtAuthenticationApi.Models.Enums;
using JwtAuthenticationApi.Models.Registration.Requests;
using JwtAuthenticationApi.Models.Registration.Responses;

namespace JwtAuthenticationApi.Registration
{
    using Abstraction.Commands;
    using Entities;
    using Handlers;
    using Identity.User;
    using Security.Password;
    using Security.Password.Salt;
    using Validators.Password;
    using Commands.Models;
    using Factories.Commands;
    using Microsoft.EntityFrameworkCore;
    using ILogger = Serilog.ILogger;

    /// <summary>
    /// Service that is responsible for registering user.
    /// </summary>
    public class UserRegisterService : IUserRegisterService
    {
        private readonly ISaltService _saltService;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ICommandHandler _commandHandler;

        private readonly ICommandFactory _commandFactory;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserRegisterService(ISaltService saltService,
            IPasswordValidator passwordValidator,
            IPasswordHashingService passwordHashingService,
            ICommandHandler commandHandler,
            ICommandFactory commandFactory,
            IUserService userService,
            ILogger logger)
        {
            _saltService = saltService;
            _passwordValidator = passwordValidator;
            _passwordHashingService = passwordHashingService;
            _commandHandler = commandHandler;
            _commandFactory = commandFactory;
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
                ICommand<UserEntity> command =
                    _commandFactory.CreateConvertRequestToUserEntityCommand(registerUserRequest, hashedPassword);
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
