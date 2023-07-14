using JwtAuthenticationApi.Models.Requests;

namespace JwtAuthenticationApi.Authentication.Registration
{
	public interface IUserRegisterService
	{
		Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken);
	}
}