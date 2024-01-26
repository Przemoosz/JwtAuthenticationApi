namespace JwtAuthenticationApi.Services.Factories.Password
{
	using JwtAuthenticationApi.Services.Abstraction.Factories.Password;
	using Abstraction.RuleEngine;
	using JwtAuthenticationApi.Services.Validators.Password.Rules;
	using JwtAuthenticationApi.Services.Models.Password;

	/// <summary>
	/// Password rule factory responsible for creating <see cref="IRule{TContext}"/>. Implements <see cref="IPasswordRuleFactory"/>.
	/// </summary>
	internal sealed class PasswordRuleFactory: IPasswordRuleFactory
	{
		public IRule<PasswordContext> CreateEqualityRule() => new EqualityRule();
		public IRule<PasswordContext> CreateLengthRule() => new LengthRule();
		public IRule<PasswordContext> CreateLowerLettersRule() => new LowerLettersRule();
		public IRule<PasswordContext> CreateSpecialLetterRule() => new SpecialLettersRule();
		public IRule<PasswordContext> CreateUpperLettersRule() => new UpperLettersRule();
	}
}
