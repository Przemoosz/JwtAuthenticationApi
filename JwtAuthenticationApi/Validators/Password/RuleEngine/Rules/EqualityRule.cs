namespace JwtAuthenticationApi.Validators.Password.RuleEngine.Rules
{
    using Abstraction.RuleEngine;
    using Exceptions;
    using Models.Password;

	public class EqualityRule: IRule<PasswordContext>
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
