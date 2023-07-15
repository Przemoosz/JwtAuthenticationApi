using JwtAuthenticationApi.Models.Requests;

namespace JwtAuthenticationApi.Registration
{
    public interface IUserRegisterService
    {
        Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken);
    }
}