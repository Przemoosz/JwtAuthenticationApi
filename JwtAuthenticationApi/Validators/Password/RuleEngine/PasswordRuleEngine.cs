namespace JwtAuthenticationApi.Validators.Password.RuleEngine
{
    using Abstraction.RuleEngine;
    using Models.Password;

    public class PasswordRuleEngine : IPasswordRuleEngine
    {
        public void Validate(PasswordContext context, IEnumerable<IPasswordRule> rules)
        {
            foreach (var passwordRule in rules)
            {
                if (passwordRule.CanEvaluateRule(context))
                {
                    passwordRule.Evaluate(context);
                }
            }
        }
    }
}