namespace JwtAuthenticationApi.Infrastructure.Database
{
	using System.Diagnostics.CodeAnalysis;
	using JwtAuthenticationApi.Infrastructure.Abstraction.Database;
	using Entities;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// Represents database context that contains password salt table and handles saving changes.
	/// Inherits from <see cref="DbContext"/>.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class PasswordSaltContext : DbContext, IPasswordSaltContext
	{
		/// <inheritdoc/>
		public DbSet<PasswordSaltEntity> PasswordSalt { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="PasswordSaltContext"/> class.
		/// </summary>
		public PasswordSaltContext() : base()
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		/// <summary>
		/// Initializes new instance of <see cref="PasswordSaltContext"/> class with database context options.
		/// </summary>
		/// <param name="dbContextOptions">Database context options.</param>
		public PasswordSaltContext(DbContextOptions<PasswordSaltContext> dbContextOptions) : base(dbContextOptions)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		/// <inheritdoc/>
		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}
	}
}