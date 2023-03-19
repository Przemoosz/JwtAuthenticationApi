namespace JwtAuthenticationApi.DatabaseContext
{
	using Microsoft.EntityFrameworkCore;
	using Models;

	public sealed class UserContext: DbContext, IUserContext
	{
		public DbSet<UserModel> Users { get; set; }

		public UserContext(): base()
		{
		}

		public UserContext(DbContextOptions<UserContext> dbContext): base(dbContext)
		{
		}

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}

	}
}