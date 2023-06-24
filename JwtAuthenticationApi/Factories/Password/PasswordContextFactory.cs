namespace JwtAuthenticationApi.Factories.Password
{
	using Models.Password;
	using Constants;

	public class PasswordContextFactory: IPasswordContextFactory
	{

		public PasswordContext Create(string password, string passwordConfirmation)
		{
			int totalUpperCase = 0;
			int totalLowerCase = 0;
			int totalSpecialCharacters = 0;
			foreach (char c in password)
			{
				if (char.IsUpper(c))
				{
					totalUpperCase++;
				}
				else if (char.IsLower(c))
				{
					totalLowerCase++;
				}
				else if (JaaConstants.SpecialCharacters.Contains(c))
				{
					totalSpecialCharacters++;
				}
			}

			return new PasswordContext(password, passwordConfirmation, password.Length, totalUpperCase, totalLowerCase,
				totalSpecialCharacters);
		}
	}
}
