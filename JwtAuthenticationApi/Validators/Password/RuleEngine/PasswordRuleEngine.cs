namespace JwtAuthenticationApi.Validators.Password.RuleEngine
{
    using Abstraction.RuleEngine;
    using Models.Password;

    public class PasswordRuleEngine : IRuleEngine<PasswordContext>
    {
        public void Validate(PasswordContext context, IEnumerable<IRule<PasswordContext>> rules)
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