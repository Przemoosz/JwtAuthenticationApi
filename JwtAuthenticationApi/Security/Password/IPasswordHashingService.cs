namespace JwtAuthenticationApi.Security.Password
{
	public interface IPasswordHashingService
	{
		Task<string> HashAsync(string password, string salt, CancellationToken cancellationToken);
	}
}