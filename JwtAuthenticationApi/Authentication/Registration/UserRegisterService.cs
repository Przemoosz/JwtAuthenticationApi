using JwtAuthenticationApi.Commands.Factory;
using JwtAuthenticationApi.Commands.Models;
using JwtAuthenticationApi.Handlers;
using JwtAuthenticationApi.Models.Requests;
using JwtAuthenticationApi.Security.Password;
using JwtAuthenticationApi.Security.Password.Salt;
using JwtAuthenticationApi.Validators.Password;

namespace JwtAuthenticationApi.Authentication.Registration
{

	public class UserRegisterService: IUserRegisterService
	{
		private readonly ISaltService _saltService;
		private readonly IPasswordValidator _passwordValidator;
		private readonly IPasswordHashingService _passwordHashingService;
		private readonly ICommandHandler _commandHandler;
		private readonly ICommandFactory _commandFactory;

		public UserRegisterService(ISaltService saltService, IPasswordValidator passwordValidator,
			IPasswordHashingService passwordHashingService, ICommandHandler commandHandler, ICommandFactory commandFactory)
		{
			_saltService = saltService;
			_passwordValidator = passwordValidator;
			_passwordHashingService = passwordHashingService;
			_commandHandler = commandHandler;
			_commandFactory = commandFactory;
		}

		public async Task<Result<bool>> RegisterUserAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
		{
			// Validate Passwords, check numbers(at least one), special signs(at least one), capital (at least one) and lower letters and length (min 8)
			var result = _passwordValidator.Validate(registerUserRequest.Password, registerUserRequest.PasswordConfirmation);
			var userId = Guid.NewGuid(); // TODO change to int to boost performance
			// Create and save salt
			var salt =  await _saltService.CreateAndSaveSaltAsync(userId, cancellationToken);
			// Create Password Hashes
			var hashedPassword =
				await _passwordHashingService.HashAsync(registerUserRequest.Password, salt, cancellationToken);
			// Create User Model
			var command = _commandFactory.CreateUserModelFromRequestCommand(registerUserRequest, hashedPassword);

			var userModel = await _commandHandler.HandleAsync(command, cancellationToken);
			// Save user model

			// If can not create user clean salt for user

			// var saltForUser = _saltService.CreateAndSaveSaltAsync()
			throw new NotImplementedException();
		}
	}

	public interface IUserRegisterService
	{
		Task<Result<bool>> RegisterUserAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken);
	}
}
