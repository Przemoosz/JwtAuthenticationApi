namespace JwtAuthenticationApi.Logger
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provides paths for JwtAuthenticationApi Web API Project.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class LoggerPath
    {
        /// <summary>
        /// Provides full path for logs storage.
        /// </summary>
        /// <value>
        /// <see cref="string"/> value of logs storage full path. 
        /// </value>
        public static string LogsStorageFullPath => Path.Combine(Path.GetTempPath(), "JwtAuthenticationApi", "Logs", "Jaa-Logs-.txt");
    }
}