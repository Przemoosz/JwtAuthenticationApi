namespace JwtAuthenticationApi.Authentication.Registration
{
	using Abstraction.Commands;
	using Commands.Factory;
	using Commands.Models;
	using Entities;
	using Handlers;
	using Identity.User;
	using Models.Requests;
	using Security.Password;
	using Security.Password.Salt;
	using Validators.Password;

	public class UserRegisterService: IUserRegisterService
	{
		private readonly ISaltService _saltService;
		private readonly IPasswordValidator _passwordValidator;
		private readonly IPasswordHashingService _passwordHashingService;
		private readonly ICommandHandler _commandHandler;

		private readonly ICommandFactory _commandFactory;
		private readonly IUserService _userService;

		public UserRegisterService(ISaltService saltService,
			IPasswordValidator passwordValidator,
			IPasswordHashingService passwordHashingService, 
			ICommandHandler commandHandler, 
			ICommandFactory commandFactory,
			IUserService userService)
		{
			_saltService = saltService;
			_passwordValidator = passwordValidator;
			_passwordHashingService = passwordHashingService;
			_commandHandler = commandHandler;
			_commandFactory = commandFactory;
			_userService = userService;
		}

		public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
		{
			if (!_passwordValidator.Validate(registerUserRequest.Password, registerUserRequest.PasswordConfirmation))
			{
				return  new RegisterUserResponse()
				{
					IsSuccessful = false,
					ErrorMessage = "Error occurred during password validation - Provided password or password confirmation is now valid.",
					ErrorType = ErrorType.PasswordValidationError
				};
			}
			string salt = _saltService.GenerateSalt();
			string hashedPassword =  await _passwordHashingService.HashPasswordAsync(registerUserRequest.Password, salt, cancellationToken);
			ICommand<UserEntity> command = _commandFactory.CreateUserEntityFromRequestCommand(registerUserRequest, hashedPassword);
			Result<UserEntity> userEntity = await _commandHandler.HandleAsync(command, cancellationToken);
			if (!userEntity.IsSuccessful)
			{
				return new RegisterUserResponse()
				{
					IsSuccessful = false,
					ErrorMessage = "Error occurred during password validation - Provided password or password confirmation is now valid.",
					ErrorType = ErrorType.InternalError
				};
			}
			// Save user model
			var userId = await _userService.SaveUserAsync(userEntity.Value, cancellationToken);
			
			// Save salt
			if (userId.HasValue)
			{
				await _saltService.SaveSaltAsync(salt, userId.Value, cancellationToken);
			}

			return null;
		}


	}

	public class RegisterUserResponse
	{
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
