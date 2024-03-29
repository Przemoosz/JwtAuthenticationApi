﻿namespace JwtAuthenticationApi.Validators.Password.Rules
{
    using Abstraction.RuleEngine;
    using Constants;
    using Exceptions;
    using Models.Password;

    public class LowerLettersRule : IRule<PasswordContext>
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