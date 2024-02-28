namespace JwtAuthenticationApi.Common.Constants
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Contains constants for Jwt Authentication Api app.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static class JaaConstants
	{
		/// <summary>
		/// String of special characters.
		/// </summary>
		public const string SpecialCharacters = @"0123456789!@#$%^&*()_+[]\;',./{}|-=:<>?~`";

		/// <summary>
		/// Minimal password length;
		/// </summary>
		public const int MinPasswordLength = 8;

		/// <summary>
		/// Maximal password Length.
		/// </summary>
		public const int MaxPasswordLength = 100;
		
		/// <summary>
		/// Minimal count of lower letters in password.
		/// </summary>
		public const int MinPasswordLowerLettersCount = 1;

		/// <summary>
		/// Minimal count of upper letters in password.
		/// </summary>
		public const int MinPasswordUpperLettersCount = 1;

		/// <summary>
		/// Minimal count of special letters in password.
		/// </summary>
		public const int MinPasswordSpecialLettersCount = 1;
	}
}