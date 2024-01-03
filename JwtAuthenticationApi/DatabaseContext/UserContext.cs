namespace JwtAuthenticationApi.DatabaseContext
{
    using Microsoft.EntityFrameworkCore;
    using Entities;
    using System.Diagnostics.CodeAnalysis;
    using Abstraction.DatabaseContext;

	/// <summary>
	/// Represents database context that contains user identity table and handles saving changes.
	/// Inherits from <see cref="DbContext"/>.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class UserContext: DbContext, IUserContext
	{
		/// <inheritdoc	/>
		public DbSet<UserEntity> Users { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="UserContext"/> class.
		/// </summary>
		public UserContext(): base()
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		/// <summary>
		/// Initializes new instance of <see cref="UserContext"/> class with database context options.
		/// </summary>
		/// <param name="dbContextOptions">Database context options.</param>
		public UserContext(DbContextOptions<UserContext> dbContextOptions) : base(dbContextOptions)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		/// <inheritdoc	/>
		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserEntity>().HasIndex(u => u.Username).IsUnique(true);
			////base.OnModelCreating(modelBuilder);
		}
	}
}