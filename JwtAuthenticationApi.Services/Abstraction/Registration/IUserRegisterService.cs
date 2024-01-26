namespace JwtAuthenticationApi.Services.Abstraction.Registration
{
	using JwtAuthenticationApi.Services.Models.Registration.Requests;
	using JwtAuthenticationApi.Services.Models.Registration.Responses;

	/// <summary>
	/// Defines method for user registration.
	/// </summary>
	public interface IUserRegisterService
	{
		/// <summary>
		/// Register user - create user model, hash password and save this data in databases.
		/// </summary>
		/// <param name="registerUserRequest">user registration request.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>A task that represents the asynchronous operation of registration
		/// The task result contains <see cref="RegisterUserResponse"/>.</returns>"/>
		Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken);
	}
}

