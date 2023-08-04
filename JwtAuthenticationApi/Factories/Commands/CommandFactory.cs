using JwtAuthenticationApi.Commands;
using JwtAuthenticationApi.Models.Registration.Requests;

namespace JwtAuthenticationApi.Factories.Commands
{
    using Entities;
    using Abstraction.Commands;

    /// <summary>
    /// Implementation of <see cref="ICommandFactory"/> interface. Provides methods to create different implementation of <see cref="ICommand{TResult}"/>.
    /// </summary>
    public sealed class CommandFactory : ICommandFactory
    {
        public ICommand<string> CreatePasswordMixCommand(string password, string salt, string pepper)
        {
            return new PasswordMixCommand(password, salt, pepper);
        }

        public ICommand<UserEntity> CreateConvertRequestToUserEntityCommand(RegisterUserRequest registerUserRequest, string hashedPassword)
        {
            return new ConvertRequestToUserEntityCommand(registerUserRequest, hashedPassword);
        }
    }
}
