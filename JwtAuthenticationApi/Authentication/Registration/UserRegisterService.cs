using JwtAuthenticationApi.Commands.Models;
using JwtAuthenticationApi.Models.Requests;
using JwtAuthenticationApi.Security.Password.Salt;

namespace JwtAuthenticationApi.Authentication.Registration
{

	public class UserRegisterService: IUserRegisterService
	{
		private readonly ISaltService _saltService;

		public UserRegisterService(ISaltService saltService)
		{
			_saltService = saltService;
		}

		public Task<Result<bool>> RegisterUserAsync(RegisterUserRequest registerUserRequest)
		{
			// Validate Passwords, check numbers(at least one), special signs(at least one), capital (at least one) and lower letters and length (min 8)
			// Create and save salt
			// Create Password Hashes
			// Validate Again password hashes
			// Create User Model
			// Save user model
			// If can not create user clean salt for user
			Guid userId = Guid.NewGuid();

			// var saltForUser = _saltService.CreateAndSaveSaltAsync()
			throw new NotImplementedException();
		}
	}

	public interface IUserRegisterService
	{
		Task<Result<bool>> RegisterUserAsync(RegisterUserRequest registerUserRequest);
	}
}
