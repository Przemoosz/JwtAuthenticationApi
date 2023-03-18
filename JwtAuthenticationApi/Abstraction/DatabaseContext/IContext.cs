namespace JwtAuthenticationApi.Abstraction.DatabaseContext;

public interface IContext
{
    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}