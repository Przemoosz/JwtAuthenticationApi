namespace JwtAuthenticationApi.Common.Options
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Stores password pepper.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class PasswordPepper
	{
		/// <summary>
		/// Gets password pepper.
		/// </summary>
		/// <value><see cref="string"/> value of password pepper.</value>
		public string Pepper { get; init; }
	}
}
