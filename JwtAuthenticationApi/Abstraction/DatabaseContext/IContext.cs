namespace JwtAuthenticationApi.Abstraction.DatabaseContext
{
	/// <summary>
	/// Base interface for database contexts. Defines saving methods.
	/// </summary>
	public interface IContext
	{
		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync"/>
		Task<int> SaveChangesAsync();
		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync"/>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}