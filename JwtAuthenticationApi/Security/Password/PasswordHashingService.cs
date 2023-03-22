namespace JwtAuthenticationApi.Security.Password
{
	using System.Security.Cryptography;
	using System.Text;
	using Models.Options;
	using Microsoft.Extensions.Options;
	using Commands.Factory;
	using Handlers;

	public class PasswordHashingService: IPasswordHashingService
	{
		private readonly IOptions<PasswordPepper> _passwordPepperOptions;
		private readonly ICommandHandler _commandHandler;
		private readonly ICommandsFactory _commandsFactory;

		public PasswordHashingService(IOptions<PasswordPepper> passwordPepperOptions, ICommandHandler commandHandler, ICommandsFactory commandsFactory)
		{
			_passwordPepperOptions = passwordPepperOptions;
			_commandHandler = commandHandler;
			_commandsFactory = commandsFactory;
		}

		public async Task<string> HashAsync(string password, string salt, CancellationToken cancellationToken = new CancellationToken())
		{
			using (var hashingAlgorithm = SHA256.Create())
			{
				var passwordMixCommand =
					_commandsFactory.CreatePasswordMixCommand(password, salt, _passwordPepperOptions.Value.Pepper);
				var passwordMixResult = await _commandHandler.HandleAsync(passwordMixCommand, cancellationToken);
				var mixedPasswordBytes = Encoding.UTF8.GetBytes(passwordMixResult.Value);
				var hashedPassword = await hashingAlgorithm.ComputeHashAsync(new MemoryStream(mixedPasswordBytes), cancellationToken);
				var hashedPasswordInBase64 = Convert.ToBase64String(hashedPassword);
				return hashedPasswordInBase64;
			}
		}
	}
}