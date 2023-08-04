using System.Diagnostics.CodeAnalysis;

namespace JwtAuthenticationApi.Models.Options
{
	/// <summary>
	/// Stores options for Serilog sinks.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class SerilogSinks
	{
		/// <summary>
		/// Gets console output template for serilog.
		/// </summary>
		public string ConsoleOutputTemplate { get; set; }
		/// <summary>
		/// Gets file output template for serilog.
		/// </summary>
		public string FileOutputTemplate { get; set; }
	}
}
