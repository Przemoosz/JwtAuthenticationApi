namespace JwtAuthenticationApi.DatabaseContext
{
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics.CodeAnalysis;
    using Entities;
    using Abstraction.DatabaseContext;

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
		}

		/// <summary>
		/// Initializes new instance of <see cref="PasswordSaltContext"/> class with database context options.
		/// </summary>
		/// <param name="dbContextOptions">Database context options.</param>
		public PasswordSaltContext(DbContextOptions<PasswordSaltContext> dbContextOptions) : base(dbContextOptions)
		{
		}

		/// <inheritdoc/>
		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}
	}
}