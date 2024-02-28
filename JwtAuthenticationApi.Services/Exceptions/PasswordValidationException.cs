namespace JwtAuthenticationApi.Services.Exceptions
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Represents one or more errors that occurs during password validation process.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal class PasswordValidationException : Exception
	{
		/// <summary>
		/// Initializes new instance of <see cref="PasswordValidationException"/> class.
		/// </summary>
		public PasswordValidationException() : base()
		{
		}

		/// <summary>
		/// Initializes new instance of <see cref="PasswordValidationException"/> class with error.
		/// </summary>
		public PasswordValidationException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes new instance of <see cref="PasswordValidationException"/> class with error message and inner exception.
		/// </summary>
		public PasswordValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}