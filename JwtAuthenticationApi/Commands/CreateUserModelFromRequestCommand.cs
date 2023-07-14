using JwtAuthenticationApi.Abstraction.Commands;
using JwtAuthenticationApi.Commands.Models;
using JwtAuthenticationApi.Entities;
using JwtAuthenticationApi.Models;
using JwtAuthenticationApi.Models.Requests;

namespace JwtAuthenticationApi.Commands
{
    public class CreateUserModelFromRequestCommand : ICommand<UserEntity>
    {
        private readonly RegisterUserRequest _registerUserRequest;
        private readonly string _hashedPassword;

        public CreateUserModelFromRequestCommand(RegisterUserRequest registerUserRequest, string hashedPassword)
        {
            _registerUserRequest = registerUserRequest;
            _hashedPassword = hashedPassword;
        }

        public Task<Result<UserEntity>> ExecuteAsync(CancellationToken cancellationToken)
        {
            UserEntity userModel = new UserEntity(_registerUserRequest.UserName, _hashedPassword, _registerUserRequest.Email);
            Result<UserEntity> result = new Result<UserEntity>(userModel, true);
            return Task.FromResult(result);
        }
    }
}
