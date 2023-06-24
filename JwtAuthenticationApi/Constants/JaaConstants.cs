namespace JwtAuthenticationApi.Constants
{
	public static class JaaConstants
	{
		public const string SpecialCharacters = @"0123456789!@#$%^&*()_+[]\;',./{}|-=:<>?~`";
		public const int MinPasswordLength = 8;
		public const int MaxPasswordLength = 100;
		public const int MinPasswordLowerLettersCount = 1;
		public const int MinPasswordUpperLettersCount = 1;
		public const int MinPasswordSpecialLettersCount = 1;
	}
}
