namespace JwtAuthenticationApi.Commands.Factory
{
	using Abstraction.Commands;
	using Entities;
	using global::JwtAuthenticationApi.Models.Requests;

	/// <summary>
	/// Defines methods for creating instances of classes that implements <see cref="ICommand{TResult}"/>.
	/// </summary>
	public interface ICommandFactory
	{
		/// <summary>
		/// Creates and returns instance of <see cref="PasswordMixCommand"/>
		/// </summary>
		/// <param name="password">Password.</param>
		/// <param name="salt">Password salt.</param>
		/// <param name="pepper">Password pepper.</param>
		/// <returns>Instance of <see cref="PasswordMixCommand"/> that implements <see cref="ICommand{TResult}"/></returns>
		ICommand<string> CreatePasswordMixCommand(string password, string salt, string pepper);

		ICommand<UserEntity> CreateUserEntityFromRequestCommand(RegisterUserRequest registerUserRequest,
			string hashedPassword);
	}
}