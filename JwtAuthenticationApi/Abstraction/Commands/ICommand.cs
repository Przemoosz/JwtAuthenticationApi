namespace JwtAuthenticationApi.Abstraction.Commands
{
	/// <summary>
	/// Defines base method that should be implemented command classes.
	/// </summary>
	/// <typeparam name="TResult">Defines type that should be returned after Executing command.</typeparam>
	public interface ICommand<TResult>
	{
		/// <summary>
		/// Executes command in asynchronous way.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>A task that represents the asynchronous command execution. The task result contains command execution result.</returns>
		Task<TResult> ExecuteAsync(CancellationToken cancellationToken);
	}
}