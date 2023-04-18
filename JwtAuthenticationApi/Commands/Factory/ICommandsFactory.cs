namespace JwtAuthenticationApi.Commands.Factory
{
	using Abstraction.Commands;

	/// <summary>
	/// Defines methods for creating instances of classes that implements <see cref="ICommand{TResult}"/>.
	/// </summary>
	public interface ICommandsFactory
	{
		/// <summary>
		/// Creates and returns instance of <see cref="PasswordMixCommand"/>
		/// </summary>
		/// <param name="password">Password.</param>
		/// <param name="salt">Password salt.</param>
		/// <param name="pepper">Password pepper.</param>
		/// <returns>Instance of <see cref="PasswordMixCommand"/> that implements <see cref="ICommand{TResult}"/></returns>
		ICommand<string> CreatePasswordMixCommand(string password, string salt, string pepper);
	}
}