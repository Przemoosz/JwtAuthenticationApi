namespace JwtAuthenticationApi.Common.Options
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Stores connection strings to databases.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class DatabaseConnectionStrings
	{
		/// <summary>
		/// Gets connection string to database where users data are stored.
		/// </summary>
		/// <value><see cref="string"/> value of database connection string.</value>
		public string IdentityDatabaseConnectionString { get; init; }

		/// <summary>
		/// Gets connections string to a database where salt for each user is stored.
		/// </summary>
		/// <value><see cref="string"/> value of database connection string.</value>
		public string SaltDatabaseConnectionString { get; init; }
	}
}
