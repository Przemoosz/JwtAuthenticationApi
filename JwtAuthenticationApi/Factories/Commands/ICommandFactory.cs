using JwtAuthenticationApi.Commands;
using JwtAuthenticationApi.Models.Registration.Requests;

namespace JwtAuthenticationApi.Factories.Commands
{
    using Abstraction.Commands;
    using Entities;

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

		/// <summary>
		/// Creates and returns instance of <see cref="ConvertRequestToUserEntityCommand"/>
		/// </summary>
		/// <param name="registerUserRequest">Register user request.</param>
		/// <param name="hashedPassword">Hashed user password.</param>
		/// <returns>Instance of <see cref="ConvertRequestToUserEntityCommand"/> that implements <see cref="ICommand{TResult}"/></returns>
		ICommand<UserEntity> CreateConvertRequestToUserEntityCommand(RegisterUserRequest registerUserRequest, string hashedPassword);
    }
}