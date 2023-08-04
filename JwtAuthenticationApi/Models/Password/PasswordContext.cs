namespace JwtAuthenticationApi.Models.Password
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Password Context for rule engine.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public sealed class PasswordContext : IEquatable<PasswordContext>
	{
		/// <summary>
		/// Gets password.
		/// </summary>
		public string Password { get; init; }

		/// <summary>
		/// Gets password confirmation.
		/// </summary>
		public string PasswordConfirmation { get; init; }

		/// <summary>
		/// Gets password length.
		/// </summary>
		public int PasswordLength { get; init; }

		/// <summary>
		/// Gets total upper case letters in password.
		/// </summary>
		public int TotalUpperCaseLetters { get; init; }

		/// <summary>
		/// Gets total lower case letters in password.
		/// </summary>
		public int TotalLowerCaseLetters { get; init; }

		/// <summary>
		/// Gets total special characters in password.
		/// </summary>
		/// <remarks>
		///	Checkout <see cref="Constants.JaaConstants"/> to see which counts as a special characters.
		/// </remarks>
		public int TotalSpecialCharacters { get; init; }

		/// <summary>
		/// Initializes new <see cref="PasswordContext"/>.
		/// </summary>
		/// <param name="password">Password.</param>
		/// <param name="passwordConfirmation">Password confirmation.</param>
		/// <param name="passwordLength">Password total length.</param>
		/// <param name="totalUpperCaseLetters">Total upper case letters in password.</param>
		/// <param name="totalLowerCaseLetters">Total lower case letters in password.</param>
		/// <param name="totalSpecialCharacters">Total special characters in password.</param>
		public PasswordContext(string password, string passwordConfirmation, int passwordLength, int totalUpperCaseLetters,
			int totalLowerCaseLetters, int totalSpecialCharacters)
		{
			Password = password;
			PasswordConfirmation = passwordConfirmation;
			PasswordLength = passwordLength;
			TotalUpperCaseLetters = totalUpperCaseLetters;
			TotalLowerCaseLetters = totalLowerCaseLetters;
			TotalSpecialCharacters = totalSpecialCharacters;
		}

		public bool Equals(PasswordContext other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Password == other.Password && PasswordConfirmation == other.PasswordConfirmation &&
			       PasswordLength == other.PasswordLength && TotalUpperCaseLetters == other.TotalUpperCaseLetters &&
			       TotalLowerCaseLetters == other.TotalLowerCaseLetters && TotalSpecialCharacters == other.TotalSpecialCharacters;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((PasswordContext)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Password, PasswordConfirmation, PasswordLength, TotalUpperCaseLetters, TotalLowerCaseLetters,
				TotalSpecialCharacters);
		}
	}

}