namespace JwtAuthenticationApi.Validators.Password.Rules
{
    using Abstraction.RuleEngine;
    using Constants;
    using Exceptions;
    using Models.Password;

    public class SpecialLettersRule : IRule<PasswordContext>
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