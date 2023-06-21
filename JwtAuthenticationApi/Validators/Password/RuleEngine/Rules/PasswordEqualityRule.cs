using JwtAuthenticationApi.Exceptions;

namespace JwtAuthenticationApi.Validators.Password.RuleEngine.Rules
{
	using Abstraction.RuleEngine;


	public class PasswordEqualityRule: IPasswordRule
	{
		public void Evaluate(PasswordContext context)
		{
			if (!context.Password.Equals(context.PasswordConfirmation))
			{
				throw new PasswordValidationException();
			}
		}

		public bool CanEvaluateRule(PasswordContext context) => true;
	}
}
