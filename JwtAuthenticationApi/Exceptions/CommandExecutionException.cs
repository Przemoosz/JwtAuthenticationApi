namespace JwtAuthenticationApi.Exceptions
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Represents one or more errors that occurs during command execution.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class CommandExecutionException: Exception
	{
		/// <summary>
		/// Initializes new instance of <see cref="CommandExecutionException"/> class.
		/// </summary>
		public CommandExecutionException(): base()
		{
			var c = new AggregateException();
		}

		/// <summary>
		/// Initializes new instance of <see cref="CommandExecutionException"/> class with error message.
		/// </summary>
		public CommandExecutionException(string message): base(message)
		{
		}

		/// <summary>
		/// Initializes new instance of <see cref="CommandExecutionException"/> class with error message and inner exception.
		/// </summary>
		public CommandExecutionException(string message, Exception innerException): base(message, innerException)
		{
		}
	}
}
