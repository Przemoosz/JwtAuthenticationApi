namespace JwtAuthenticationApi.Models.Password
{
	using System.Diagnostics.CodeAnalysis;

	[ExcludeFromCodeCoverage]
	public sealed class PasswordContext : IEquatable<PasswordContext>
	{
		public string Password { get; init; }
		public string PasswordConfirmation { get; init; }
		public int PasswordLength { get; init; }
		public int TotalUpperCaseLetters { get; init; }
		public int TotalLowerCaseLetters { get; init; }
		public int TotalSpecialCharacters { get; init; }

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