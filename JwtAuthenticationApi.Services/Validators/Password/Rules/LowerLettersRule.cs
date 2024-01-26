namespace JwtAuthenticationApi.Services.Validators.Password.Rules
{
	using Common.Constants;
	using Abstraction.RuleEngine;
	using Exceptions;
	using JwtAuthenticationApi.Services.Models.Password;

	internal class LowerLettersRule : IRule<PasswordContext>
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