namespace JwtAuthenticationApi.Handlers
{
    using Abstraction.Commands;
    using Commands.Models;

	/// <summary>
	/// Define method for handling <see cref="ICommand{TResult}"/> requests in asynchronous way.
	/// </summary>
	public interface ICommandHandler
    {
		/// <summary>
		/// Handles <see cref="ICommand{TResult}"/> command and returns its result.
		/// </summary>
		/// <typeparam name="TResult">Defines type that will be returned after handling command.</typeparam>
		/// <param name="command">Command that implements <see cref="ICommand{TResult}"/> interface.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <exception cref="T:JwtAuthenticationApi.Exceptions.CommandExecutionException"/>
		/// <returns>A task that represents the asynchronous password mix operation. The task result of handling provided command.</returns>
		Task<Result<TResult>> HandleAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken);
    }
}