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
		/// <param name="userId">UserId for whom password salt will be queried.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Password salt.</returns>
		Task<string> GetPasswordSaltAsync(int userId, CancellationToken cancellationToken = new CancellationToken());
	}
}