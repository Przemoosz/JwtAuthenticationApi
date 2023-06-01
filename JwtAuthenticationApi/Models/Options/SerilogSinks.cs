using System.Diagnostics.CodeAnalysis;

namespace JwtAuthenticationApi.Models.Options
{
	[ExcludeFromCodeCoverage]
	public sealed class SerilogSinks
	{
		public string ConsoleOutputTemplate { get; set; }
		public string FileOutputTemplate { get; set; }
	}
}
