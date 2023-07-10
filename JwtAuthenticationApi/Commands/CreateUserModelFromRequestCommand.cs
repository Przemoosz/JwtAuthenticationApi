using JwtAuthenticationApi.Abstraction.Commands;
using JwtAuthenticationApi.Commands.Models;
using JwtAuthenticationApi.Models;
using JwtAuthenticationApi.Models.Requests;

namespace JwtAuthenticationApi.Commands
{
    public class CreateUserModelFromRequestCommand : ICommand<UserModel>
    {
        private readonly RegisterUserRequest _registerUserRequest;
        private readonly string _hashedPassword;

        public CreateUserModelFromRequestCommand(RegisterUserRequest registerUserRequest, string hashedPassword)
        {
            _registerUserRequest = registerUserRequest;
            _hashedPassword = hashedPassword;
        }

        public Task<Result<UserModel>> ExecuteAsync(CancellationToken cancellationToken)
        {
            UserModel userModel = new UserModel(Guid.NewGuid(), _registerUserRequest.UserName, _hashedPassword,
                _registerUserRequest.Email);
            var result = new Result<UserModel>(userModel, true);
            return Task.FromResult(result);
        }
    }
}
