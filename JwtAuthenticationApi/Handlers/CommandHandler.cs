namespace JwtAuthenticationApi.Handlers
{
    using Abstraction.Commands;
    using Commands.Models;

	/// <summary>
	/// Implementation of <see cref="ICommandHandler"/>. Provides environment that will handle commands in asynchronous way.
	/// </summary>
	public sealed class CommandHandler : ICommandHandler
    {
	    /// <inheritdoc/>
		public async Task<Result<TResult>> HandleAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = new CancellationToken())
        {
            return await command.ExecuteAsync(cancellationToken);
        }
    }
}