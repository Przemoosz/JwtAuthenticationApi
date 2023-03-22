namespace JwtAuthenticationApi.Exceptions
{
	using System.Diagnostics.CodeAnalysis;

	[ExcludeFromCodeCoverage]
	public sealed class CommandExecutionException: Exception
	{
		public CommandExecutionException(): base()
		{
		}

		public CommandExecutionException(string message): base(message)
		{
		}

		public CommandExecutionException(string message, Exception innerException): base(message, innerException)
		{
		}
	}
}
