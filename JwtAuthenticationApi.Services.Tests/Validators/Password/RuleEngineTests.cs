namespace JwtAuthenticationApi.Services.Tests.Validators.Password
{
	using Abstraction.RuleEngine;
	using Extensions;
	using Models.Password;
	using NSubstitute;
	using NUnit.Framework;
	using Services.Validators.Password;

	[TestFixture, Parallelizable]
    public sealed class RuleEngineTests
    {
        private RuleEngine<PasswordContext> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new RuleEngine<PasswordContext>();
        }

        [Test]
        public void Validate_EvaluatesAllRules()
        {
            // Arrange
            PasswordContext passwordContext = new PasswordContext(null, null, 0, 0, 0, 0);
            IRule<PasswordContext> firstRule = Substitute.For<IRule<PasswordContext>>();
            IRule<PasswordContext> secondRule = Substitute.For<IRule<PasswordContext>>();
            List<IRule<PasswordContext>> rules = new List<IRule<PasswordContext>>(2);
            rules.AddRules(firstRule, secondRule);
            firstRule.CanEvaluateRule(passwordContext).Returns(true);
            secondRule.CanEvaluateRule(passwordContext).Returns(true);

            // Act 
            _sut.Validate(passwordContext, rules);

            // Assert
            firstRule.Received(1).Evaluate(passwordContext);
            secondRule.Received(1).Evaluate(passwordContext);
        }
    }
}
