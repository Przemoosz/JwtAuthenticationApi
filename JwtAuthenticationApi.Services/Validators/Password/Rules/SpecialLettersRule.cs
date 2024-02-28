namespace JwtAuthenticationApi.Services.Validators.Password.Rules
{
	using Common.Constants;
	using Abstraction.RuleEngine;
	using Exceptions;
	using JwtAuthenticationApi.Services.Models.Password;

	internal class SpecialLettersRule : IRule<PasswordContext>
    {
        public void Evaluate(PasswordContext context)
        {
            if (context.TotalSpecialCharacters < JaaConstants.MinPasswordSpecialLettersCount)
            {
                throw new PasswordValidationException("Provided passwords does not contain special letters.");
            }
        }

        public bool CanEvaluateRule(PasswordContext context) => true;
    }
}