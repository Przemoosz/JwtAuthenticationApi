namespace JwtAuthenticationApi.Commands
{
	using Abstraction.Commands;
	using Exceptions;
	using Models;

	/// <summary>
	/// Command that is responsible for mixing password in predefined way. Implements <see cref="ICommand{TResult}"/>.
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
		/// <exception cref="CommandExecutionException"/>
		/// <exception cref="ArgumentException"/>
		/// <returns>A task that represents the asynchronous password mix operation.
		/// The task result contains <see cref="Result{TResult}"/> object that contains mixed password.</returns>
		public Task<Result<string>> ExecuteAsync(CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(_password))
			{
				throw new CommandExecutionException($"Cannot execute {nameof(PasswordMixCommand)} command.",
					new ArgumentException("Provided password can not be null or empty value."));
			}

			if (string.IsNullOrEmpty(_salt))
			{
				throw new CommandExecutionException($"Cannot execute {nameof(PasswordMixCommand)} command.",
					new ArgumentException("Provided salt can not be null or empty value."));
			}

			if (string.IsNullOrEmpty(_pepper))
			{
				throw new CommandExecutionException($"Cannot execute {nameof(PasswordMixCommand)} command.",
					new ArgumentException("Provided pepper can not be null or empty value."));
			}
			var mixedPassword = $"{_password}{_pepper}{_salt}";
			return Task.FromResult(new Result<string>(mixedPassword, true));
		}
	}
}