namespace JwtAuthenticationApi.Services.Validators.Password.Rules
{
	using Common.Constants;
	using Abstraction.RuleEngine;
	using Exceptions;
	using JwtAuthenticationApi.Services.Models.Password;

	internal class UpperLettersRule : IRule<PasswordContext>
    {
        public void Evaluate(PasswordContext context)
        {
            if (context.TotalUpperCaseLetters < JaaConstants.MinPasswordUpperLettersCount)
            {
                throw new PasswordValidationException("Provided passwords does not contain upper letters.");
            }
        }

        public bool CanEvaluateRule(PasswordContext context) => true;
    }
}
