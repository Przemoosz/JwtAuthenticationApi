using JwtAuthenticationApi.Models;

namespace JwtAuthenticationApi.Security.Password.Salt
{
	using DatabaseContext;

	public sealed class SaltProvider
	{
		private readonly IPasswordSaltContext _context;

		public SaltProvider(IPasswordSaltContext context)
		{
			_context = context;
		}

		public async Task<string> GetPasswordSalt(UserModel user)
		{

		}


	}
}
