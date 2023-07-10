namespace JwtAuthenticationApi.UnitTests.Validators.Password.RuleEngine.Rules
{
    using Exceptions;
    using JwtAuthenticationApi.Models.Password;
    using TestHelpers.Attributes;
    using JwtAuthenticationApi.Validators.Password.Rules;

    [TestFixture, Parallelizable, RuleTest]
	public sealed class PasswordSpecialLettersRuleTests
	{
		private SpecialLettersRule _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new SpecialLettersRule();
		}

		[Test]
		public void ShouldReturnTrueForCanEvaluateRule()
		{
			// Act & Assert
			_sut.CanEvaluateRule(null).Should().BeTrue();
		}

		[Test]
		public void ShouldThrowPasswordValidationExceptionIfPasswordsDoesNotContainUpperLetters()
		{
			// Arrange
			var passwordContext = new PasswordContext(null, null, 0, 0, 0, 0);
			Action action = () => _sut.Evaluate(passwordContext);

			// Act & Assert
			action.Should().Throw<PasswordValidationException>().WithMessage("Provided passwords does not contain special letters.");
		}

		[Test]
		public void ShouldNotThrowPasswordValidationExceptionIfPasswordsContainsUpperLetters()
		{
			// Arrange
			var passwordContext = new PasswordContext(null, null, 0, 0, 0, 10);
			Action action = () => _sut.Evaluate(passwordContext);

			// Act & Assert
			action.Should().NotThrow<PasswordValidationException>();
		}
	}
}