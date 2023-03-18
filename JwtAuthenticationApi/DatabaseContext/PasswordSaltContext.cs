namespace JwtAuthenticationApi.DatabaseContext
{
	using Models;
	using Microsoft.EntityFrameworkCore;

	public sealed class PasswordSaltContext : DbContext, IPasswordSaltContext
	{
		public DbSet<PasswordSaltModel> PasswordSalt { get; set; }

		public PasswordSaltContext() : base()
		{
		}

		public PasswordSaltContext(DbContextOptions<PasswordSaltContext> dbContext) : base(dbContext)
		{
		}

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}
	}
}