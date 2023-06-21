namespace JwtAuthenticationApi.Security.Password.Salt
{
	using Commands.Models;

	/// <summary>
	/// Defines methods for getting and creating password salts.
	/// </summary>
	public interface ISaltService
	{
		/// <summary>
		/// Creates new password salt associated with <paramref name="userId"/> and saves this salt in salt database.
		/// </summary>
		/// <param name="userId">UserId for whom password salt will be created.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>A task that represents the asynchronous operation of creating and saving password salt.
		/// The task result contains <see cref="string"/> value of password salt.</returns>
		Task<string> CreateAndSaveSaltAsync(Guid userId, CancellationToken cancellationToken = new CancellationToken());

		/// <summary>
		/// Gets password salt associated with <paramref name="userId"/>.
		/// </summary>
		/// <param name="userId">UserId for whom password salt will be queried.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>A task that represents the asynchronous operation of getting password salt from database.
		/// The task result contains <see cref="Result{TResult}"/> value of operation result.</returns>
		Task<Result<string>> GetSaltAsync(Guid userId, CancellationToken cancellationToken = new CancellationToken());
	}
}