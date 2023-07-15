using JwtAuthenticationApi.Commands.Factory;
using JwtAuthenticationApi.Commands.Models;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace JwtAuthenticationApi.Registration
{
    using Abstraction.Commands;
    using Entities;
    using Handlers;
    using Identity.User;
    using Models.Requests;
    using Security.Password;
    using Security.Password.Salt;
    using Validators.Password;

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
                    _commandFactory.CreateUserEntityFromRequestCommand(registerUserRequest, hashedPassword);
                Result<UserEntity> userEntity = await _commandHandler.HandleAsync(command, cancellationToken);
                if (!userEntity.IsSuccessful)
                {
                    return new RegisterUserResponse()
                    {
                        IsSuccessful = false,
                        ErrorMessage =
                            "Error occurred during password validation - Provided password or password confirmation is now valid.",
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
                        ErrorMessage = "Error occurred during saving user in database - No Id retrieved for new user ",
                        ErrorType = ErrorType.InternalError
                    };
                }
            }
            catch (DbUpdateException)
            {
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

    public class RegisterUserResponse
    {
        public int UserId { get; set; }
        public bool IsSuccessful { get; set; }
        public ErrorType ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum ErrorType
    {
        DbError,
        PasswordValidationError,
        InternalError,
    }
}
