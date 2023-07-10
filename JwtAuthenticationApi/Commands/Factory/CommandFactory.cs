using JwtAuthenticationApi.Models;
using JwtAuthenticationApi.Models.Requests;

namespace JwtAuthenticationApi.Commands.Factory
{
	using Abstraction.Commands;

	/// <summary>
	/// Implementation of <see cref="ICommandFactory"/> interface. Provides methods to create different implementation of <see cref="ICommand{TResult}"/>.
	/// </summary>
	public sealed class CommandFactory: ICommandFactory
	{
		/// <inheritdoc/>
		public ICommand<string> CreatePasswordMixCommand(string password, string salt, string pepper)
		{
			return new PasswordMixCommand(password, salt, pepper);
		}

		public ICommand<UserModel> CreateUserModelFromRequestCommand(RegisterUserRequest registerUserRequest, string hashedPassword)
		{
			return new CreateUserModelFromRequestCommand(registerUserRequest, hashedPassword);
		}
	}
}
