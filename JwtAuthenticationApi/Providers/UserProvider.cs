using JwtAuthenticationApi.Commands.Models;
using JwtAuthenticationApi.DatabaseContext;
using JwtAuthenticationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationApi.Providers
{
	public class UserProvider: IUserProvider
	{
		private readonly IUserContext _userContext;

		public UserProvider(IUserContext userContext)
		{
			_userContext = userContext;
		}

		public async Task<Result<UserModel>> GetUserById(Guid id)
		{
			UserModel user = await _userContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(id));
			if (user is null)
			{
				return new Result<UserModel>(null, false);
			}
			return new Result<UserModel>(user, true);
		}
	}

	public interface IUserProvider
	{

		Task<Result<UserModel>> GetUserById(Guid id);

	}
}
