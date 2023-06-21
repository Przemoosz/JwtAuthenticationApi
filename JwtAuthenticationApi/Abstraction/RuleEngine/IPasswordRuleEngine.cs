using JwtAuthenticationApi.Validators.Password.RuleEngine;

namespace JwtAuthenticationApi.Abstraction.RuleEngine;

public interface IPasswordRuleEngine
{
	void Validate(PasswordContext context, IEnumerable<IPasswordRule> rules);
}