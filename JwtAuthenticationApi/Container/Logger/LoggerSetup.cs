namespace JwtAuthenticationApi.Container.Logger
{
    using System.Diagnostics.CodeAnalysis;
    using Exceptions;
    using Models.Options;
    using Serilog;
    using Serilog.Events;
    using ILogger = Serilog.ILogger;
    using Models;


    [ExcludeFromCodeCoverage]
    public static class LoggerSetup
    {
        public static void SetupSerilog(this WebApplicationBuilder builder)
        {
            string consoleOutputTemplate = builder.Configuration
                .GetSection($"{nameof(SerilogSinks)}:{nameof(SerilogSinks.ConsoleOutputTemplate)}").Value;
            SettingsNotProvidedException.ThrowIfSettingIsNullOrEmpty(consoleOutputTemplate, nameof(SerilogSinks.ConsoleOutputTemplate));
            string fileOutputTemplate = builder.Configuration
                .GetSection($"{nameof(SerilogSinks)}:{nameof(SerilogSinks.FileOutputTemplate)}").Value;
            SettingsNotProvidedException.ThrowIfSettingIsNullOrEmpty(fileOutputTemplate, nameof(SerilogSinks.FileOutputTemplate));
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .WriteTo.Async(w => w.Console(outputTemplate: consoleOutputTemplate!))
                .WriteTo.Async(w => w.File(
                    path: JaaPaths.LogsStorageFullPath,
                    outputTemplate: fileOutputTemplate!,
                    restrictedToMinimumLevel: LogEventLevel.Warning,
                    rollOnFileSizeLimit: true,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 31))
                .CreateLogger();
            logger.Information("Serilog setup finished.");
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            builder.Services.AddSingleton<ILogger>(logger);
        }
    }
}