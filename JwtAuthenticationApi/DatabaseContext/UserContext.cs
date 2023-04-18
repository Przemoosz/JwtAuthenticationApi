namespace JwtAuthenticationApi.DatabaseContext
{
	using Microsoft.EntityFrameworkCore;
	using Models;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Represents database context that contains user identity table and handles saving changes.
	/// Inherits from <see cref="DbContext"/>.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class UserContext: DbContext, IUserContext
	{
		/// <inheritdoc	/>
		public DbSet<UserModel> Users { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="UserContext"/> class.
		/// </summary>
		public UserContext(): base()
		{
		}

		/// <summary>
		/// Initializes new instance of <see cref="UserContext"/> class with database context options.
		/// </summary>
		/// <param name="dbContextOptions">Database context options.</param>
		public UserContext(DbContextOptions<UserContext> dbContextOptions) : base(dbContextOptions)
		{
		}

		/// <inheritdoc	/>
		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}
	}
}