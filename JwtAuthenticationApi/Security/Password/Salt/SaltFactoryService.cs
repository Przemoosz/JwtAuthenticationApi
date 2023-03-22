namespace JwtAuthenticationApi.Security.Password.Salt
{
	using DatabaseContext;
	using Models;

	public sealed class SaltFactoryService
	{
		private readonly IPasswordSaltContext _context;

		public SaltFactoryService(IPasswordSaltContext context)
		{
			_context = context;
		}

		public async Task<string> CreateAndSaveSalt(UserModel user)
		{
			var salt = Guid.NewGuid().ToString();
			var passwordSaltContext = new PasswordSaltModel(){Salt = salt, UserId = user.Id};
			await _context.PasswordSalt.AddAsync(passwordSaltContext);
			await _context.SaveChangesAsync();
			return salt;
		}
	}
}
