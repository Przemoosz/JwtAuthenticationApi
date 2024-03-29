﻿namespace JwtAuthenticationApi.Validators.Password
{
    using Abstraction.RuleEngine;

    public class RuleEngine<T> : IRuleEngine<T>
    {
        public void Validate(T context, IEnumerable<IRule<T>> rules)
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