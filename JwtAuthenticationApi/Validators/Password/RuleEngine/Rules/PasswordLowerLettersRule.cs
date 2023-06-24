namespace JwtAuthenticationApi.Validators.Password.RuleEngine.Rules
{
	using Abstraction.RuleEngine;
	using Constants;
	using Exceptions;
	using Models.Password;

	public class PasswordLowerLettersRule : IPasswordRule
	{
		public void Evaluate(PasswordContext context)
		{
			if (context.TotalLowerCaseLetters < JaaConstants.MinPasswordLowerLettersCount)
			{
				throw new PasswordValidationException("Provided passwords does not contain lower letters.");
			}
		}

		public bool CanEvaluateRule(PasswordContext context) => true;
	}
}