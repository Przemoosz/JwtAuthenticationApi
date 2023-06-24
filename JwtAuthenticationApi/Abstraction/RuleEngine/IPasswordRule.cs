using JwtAuthenticationApi.Models.Password;

namespace JwtAuthenticationApi.Abstraction.RuleEngine
{
    public interface IPasswordRule
	{
		void Evaluate(PasswordContext context);
		bool CanEvaluateRule(PasswordContext context);
	}
}
