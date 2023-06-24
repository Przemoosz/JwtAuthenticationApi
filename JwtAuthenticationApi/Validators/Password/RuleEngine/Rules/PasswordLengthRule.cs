﻿namespace JwtAuthenticationApi.Validators.Password.RuleEngine.Rules
{
    using Abstraction.RuleEngine;
    using Exceptions;
    using Constants;
    using Models.Password;

	public sealed class PasswordLengthRule: IPasswordRule
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
