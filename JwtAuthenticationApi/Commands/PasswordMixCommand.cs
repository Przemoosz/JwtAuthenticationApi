namespace JwtAuthenticationApi.Commands
{
	using Abstraction.Commands;

	/// <summary>
	/// Command that is responsible to mix password in predefined way. Implements <see cref="ICommand{TResult}"/>.
	/// </summary>
	public sealed class PasswordMixCommand: ICommand<string>
	{
		private readonly string _password;
		private readonly string _salt;
		private readonly string _pepper;

		/// <summary>
		/// Initializes a new <see cref="PasswordMixCommand"/> with password, salt and pepper.
		/// </summary>
		/// <param name="password">Password.</param>
		/// <param name="salt">Password salt.</param>
		/// <param name="pepper">Password pepper.</param>
		public PasswordMixCommand(string password, string salt, string pepper)
		{
			_password = password;
			_salt = salt;
			_pepper = pepper;
		}

		/// <summary>
		/// Executes password mixing with pepper and salt.
		/// </summary>
		/// <param name="cancellationToken">CancellationToken.</param>
		/// <returns>A task that represents the asynchronous password mix operation. The task result contains mixed password.</returns>
		public Task<string> ExecuteAsync(CancellationToken cancellationToken)
		{
			var mixedPassword = $"{_password}{_pepper}{_salt}";
			return Task.FromResult(mixedPassword);
		}
	}
}