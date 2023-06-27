namespace JwtAuthenticationApi.Factories.Password
{
	using Abstraction.RuleEngine;
	using Models.Password;
	using Validators.Password.RuleEngine.Rules;

	public sealed class PasswordRuleFactory: IPasswordRuleFactory
	{
		public IRule<PasswordContext> CreateEqualityRule() => new EqualityRule();
		public IRule<PasswordContext> CreateLengthRule() => new LengthRule();
		public IRule<PasswordContext> CreateLowerLettersRule() => new LowerLettersRule();
		public IRule<PasswordContext> CreateSpecialLetterRule() => new SpecialLettersRule();
		public IRule<PasswordContext> CreateUpperLettersRule() => new UpperLettersRule();
	}
}
