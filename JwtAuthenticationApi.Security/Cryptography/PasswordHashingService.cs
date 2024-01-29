namespace JwtAuthenticationApi.Security.Cryptography
{
	using System.Security.Cryptography;
	using System.Text;
	using JwtAuthenticationApi.Common.Abstraction.Handlers;
	using Common.Options;
	using JwtAuthenticationApi.Security.Abstraction.Cryptography;
	using Microsoft.Extensions.Options;
	using Commands;

	/// <inheritdoc/>
	public class PasswordHashingService: IPasswordHashingService
	{
		private readonly IOptions<PasswordPepper> _passwordPepperOptions;
		private readonly ICommandHandler _commandHandler;

		/// <summary>
		/// Initializes new instance of <see cref="PasswordHashingService"/> class.
		/// </summary>
		/// <param name="passwordPepperOptions">Password pepper option as <see cref="IOptions{TOptions}"/>.</param>
		/// <param name="commandHandler">Command Handler.</param>
		public PasswordHashingService(IOptions<PasswordPepper> passwordPepperOptions, ICommandHandler commandHandler)
		{
			_passwordPepperOptions = passwordPepperOptions;
			_commandHandler = commandHandler;
		}

		/// <inheritdoc/>
		public async Task<string> HashPasswordAsync(string password, string salt, CancellationToken cancellationToken = new CancellationToken())
		{
			using (var hashingAlgorithm = SHA256.Create())
			{
				PasswordMixCommand passwordMixCommand = new PasswordMixCommand(password, salt, _passwordPepperOptions.Value.Pepper);
				var passwordMixResult = await _commandHandler.HandleAsync(passwordMixCommand, cancellationToken);
				var mixedPasswordBytes = Encoding.UTF8.GetBytes(passwordMixResult.Value);
				var hashedPassword = await hashingAlgorithm.ComputeHashAsync(new MemoryStream(mixedPasswordBytes), cancellationToken);
				var hashedPasswordInBase64 = Convert.ToBase64String(hashedPassword);
				return hashedPasswordInBase64;
			}
		}
	}
}