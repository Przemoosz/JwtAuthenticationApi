namespace JwtAuthenticationApi.Security.Password.Salt
{
	using Models;

	/// <summary>
	/// Defines method for providing password salt from database.
	/// </summary>
	public interface ISaltProvider
	{
		/// <summary>
		/// Gets or creates password salt associated with <paramref name="user"/>.
		/// </summary>
		/// <param name="user">User for whom password salt will be queried.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Password salt.</returns>
		Task<string> GetPasswordSaltAsync(UserModel user, CancellationToken cancellationToken = new CancellationToken());
	}
}