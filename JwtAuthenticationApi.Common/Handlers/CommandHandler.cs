namespace JwtAuthenticationApi.Common.Handlers
{
	using JwtAuthenticationApi.Common.Abstraction.Handlers;
	using Abstraction.Commands;
	using Models;

	/// <summary>
	/// Implementation of <see cref="ICommandHandler"/>. Provides environment that will handle commands in asynchronous way.
	/// </summary>
	internal sealed class CommandHandler : ICommandHandler
    {
	    /// <inheritdoc/>
		public async Task<Result<TResult>> HandleAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = new CancellationToken())
        {
            return await command.ExecuteAsync(cancellationToken);
        }
    }
}