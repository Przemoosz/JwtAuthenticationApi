namespace JwtAuthenticationApi.Abstraction.DatabaseContext
{
	public interface IContext
	{
		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync"/>
		Task<int> SaveChangesAsync();
		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync"/>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}