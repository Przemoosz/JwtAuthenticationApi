namespace JwtAuthenticationApi.Handlers
{
    using Abstraction.Commands;

    /// <summary>
    /// Implementation of <see cref="ICommandHandler"/>.Provides environment that will handle commands in asynchronous way.
    /// </summary>
    public sealed class CommandHandler : ICommandHandler
    {
	    public async Task<TResult> HandleAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = new CancellationToken())
        {
            return await command.ExecuteAsync(cancellationToken);
        }
    }
}