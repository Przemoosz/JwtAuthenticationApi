namespace JwtAuthenticationApi.Commands.Factory
{
	using Abstraction.Commands;

	/// <summary>
	/// Implementation of <see cref="ICommandsFactory"/> interface. Provides methods to create different implementation of <see cref="ICommand{TResult}"/>.
	/// </summary>
	public sealed class CommandsFactory: ICommandsFactory
	{
		/// <inheritdoc/>
		public ICommand<string> CreatePasswordMixCommand(string password, string salt, string pepper)
		{
			return new PasswordMixCommand(password, salt, pepper);
		}
	}
}
