namespace JwtAuthenticationApi.Services.Validators.Password.Rules
{
	using JwtAuthenticationApi.Services.Models.Password;
	using Abstraction.RuleEngine;
	using Exceptions;

	internal class EqualityRule : IRule<PasswordContext>
    {
        public void Evaluate(PasswordContext context)
        {
            if (!context.Password.Equals(context.PasswordConfirmation))
            {
                throw new PasswordValidationException("Provided passwords are not equal.");
            }
        }

        public bool CanEvaluateRule(PasswordContext context) => true;
    }
}
