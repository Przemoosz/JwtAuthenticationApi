namespace JwtAuthenticationApi.Services.Factories.Password
{
	using Common.Constants;
	using JwtAuthenticationApi.Services.Abstraction.Factories.Password;
	using JwtAuthenticationApi.Services.Models.Password;

	/// <summary>
	/// Password context factory, responsible for creating <see cref="PasswordContext"/>. Implements <see cref="IPasswordContextFactory"/>
	/// </summary>
	internal sealed class PasswordContextFactory: IPasswordContextFactory
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