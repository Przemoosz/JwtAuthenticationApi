namespace JwtAuthenticationApi.Validators.Password.Rules
{
    using Exceptions;
    using Models.Password;
    using Abstraction.RuleEngine;
    using Constants;


    public class UpperLettersRule : IRule<PasswordContext>
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
