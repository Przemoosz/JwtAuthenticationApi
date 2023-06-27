using JwtAuthenticationApi.Abstraction.RuleEngine;
using JwtAuthenticationApi.Extensions;
using JwtAuthenticationApi.Models.Password;

namespace JwtAuthenticationApi.UnitTests.Validators.Password.RuleEngine
{
	using JwtAuthenticationApi.Validators.Password.RuleEngine;

	[TestFixture, Parallelizable]
	public sealed class PasswordRuleEngineTests
	{
		private PasswordRuleEngine _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new PasswordRuleEngine();
		}

		[Test]
		public void ShouldEvaluateAllRules()
		{
			// Arrange
			PasswordContext passwordContext = new PasswordContext(null, null, 0, 0, 0, 0);
			IRule<PasswordContext> firstRule = Substitute.For<IRule<PasswordContext>>();
			IRule<PasswordContext> secondRule = Substitute.For<IRule<PasswordContext>>();
			List<IRule<PasswordContext>> rules = new List<IRule<PasswordContext>>(2);
			rules.AddRules<PasswordContext>(firstRule, secondRule);
			firstRule.CanEvaluateRule(passwordContext).Returns(true);
			secondRule.CanEvaluateRule(passwordContext).Returns(true);

			// Act 
			_sut.Validate(passwordContext,rules);

			// Assert
			firstRule.Received(1).Evaluate(passwordContext);
			secondRule.Received(1).Evaluate(passwordContext);
		}
	}
}
