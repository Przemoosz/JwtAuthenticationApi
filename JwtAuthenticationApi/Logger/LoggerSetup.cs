using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace JwtAuthenticationApi.Container
{
	[ExcludeFromCodeCoverage]
	public static class LoggerSetup
	{
		public static void SetupSerilog(this WebApplicationBuilder builder)
		{
			Logger logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.Enrich.WithThreadId()
				.Enrich.WithHttpRequestId()
				.Enrich.WithThreadName()
				.WriteTo.Async(w => w.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} [ThreadId:{ThreadId}]] {Message:lj}{NewLine}{Exception}"))
				.WriteTo.Async(w => w.File("Logs.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [ThreadId:{ThreadId}] {Message:lj}{NewLine}{Exception}", restrictedToMinimumLevel: LogEventLevel.Warning, rollOnFileSizeLimit: true))
				.CreateLogger();
			logger.Information("Serilog setup finished.");
			builder.Logging.ClearProviders();
			builder.Logging.AddSerilog(logger);
			builder.Services.AddSingleton<ILogger>(logger);
		}
	}
}
