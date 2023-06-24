using JwtAuthenticationApi.Models.Password;

namespace JwtAuthenticationApi.Abstraction.RuleEngine;

public interface IPasswordRuleEngine
{
	void Validate(PasswordContext context, IEnumerable<IPasswordRule> rules);
}