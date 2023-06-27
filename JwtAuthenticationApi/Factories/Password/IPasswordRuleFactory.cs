namespace JwtAuthenticationApi.Factories.Password
{
	using Abstraction.RuleEngine;
	using Models.Password;

	public interface IPasswordRuleFactory
	{
		IRule<PasswordContext> CreateEqualityRule();
		IRule<PasswordContext> CreateLengthRule();
		IRule<PasswordContext> CreateLowerLettersRule();
		IRule<PasswordContext> CreateSpecialLetterRule();
		IRule<PasswordContext> CreateUpperLettersRule();
	}
}