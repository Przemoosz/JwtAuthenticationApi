namespace JwtAuthenticationApi.Security.Password.Salt
{
	using Commands.Models;

	/// <summary>
	/// Defines methods for generating, receiving and saving password salts.
	/// </summary>
	public interface ISaltService
	{
		/// <summary>
		/// Saves salt associated with <paramref name="userId"/> in salt database.
		/// </summary>
		/// <param name="salt">User associated salt.</param>
		/// <param name="userId">UserId for whom password salt will be created.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>A task that represents the asynchronous operation of creating and saving password salt.
		/// The task result contains id of newly created salt.</returns>
		Task<int?> SaveSaltAsync(string salt, int userId, CancellationToken cancellationToken = new CancellationToken());

		/// <summary>
		/// Gets password salt associated with <paramref name="userId"/>.
		/// </summary>
		/// <param name="userId">UserId for whom password salt will be queried.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>A task that represents the asynchronous operation of getting password salt from database.
		/// The task result contains <see cref="Result{TResult}"/> value of operation result.</returns>
		Task<Result<string>> GetSaltAsync(int userId, CancellationToken cancellationToken = new CancellationToken());

		/// <summary>
		/// Generates new password salt.
		/// </summary>
		/// <returns><see cref="string"/> value of salt.</returns>
		string GenerateSalt();
	}
}