namespace JwtAuthenticationApi.Services.Validators.Password.Rules
{
	using JwtAuthenticationApi.Services.Models.Password;
	using Abstraction.RuleEngine;
	using Exceptions;
	using Common.Constants;

	internal sealed class LengthRule : IRule<PasswordContext>
    {
        public void Evaluate(PasswordContext context)
        {
            if (context.PasswordLength < JaaConstants.MinPasswordLength || context.PasswordLength > JaaConstants.MaxPasswordLength)
            {
                throw new PasswordValidationException("Password length is not valid.");
            }
        }

        public bool CanEvaluateRule(PasswordContext context) => true;
    }
}
