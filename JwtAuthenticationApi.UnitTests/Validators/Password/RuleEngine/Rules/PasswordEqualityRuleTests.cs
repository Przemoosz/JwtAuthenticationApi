namespace JwtAuthenticationApi.UnitTests.Validators.Password.RuleEngine.Rules
{
    using Exceptions;
    using JwtAuthenticationApi.Models.Password;
    using TestHelpers.Attributes;
    using JwtAuthenticationApi.Validators.Password.Rules;

    [TestFixture, Parallelizable, RuleTest]
	public class PasswordEqualityRuleTests
	{
		private EqualityRule _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new EqualityRule();
		}

		[Test]
		public void ShouldReturnTrueForCanEvaluateRule()
		{
			// Act & Assert
			_sut.CanEvaluateRule(null).Should().BeTrue();
		}

		[Test]
		public void ShouldThrowPasswordValidationExceptionIfPasswordsAreNotEqual()
		{
			// Arrange
			const string password = "AAAAA";
			const string passwordConfirmation = "XYYX";
			PasswordContext passwordContext = new PasswordContext(password,
				passwordConfirmation, 0, 0, 0, 0);
			Action action = () => _sut.Evaluate(passwordContext);

			// Act & Assert
			action.Should().Throw<PasswordValidationException>().WithMessage("Provided passwords are not equal.");
		}

		[Test]
		public void ShouldNotThrowPasswordValidationExceptionIfPasswordsAreEqual()
		{
			// Arrange
			const string password = "AAAAA";
			const string passwordConfirmation = "AAAAA";
			PasswordContext passwordContext = new PasswordContext(password,
				passwordConfirmation, 0, 0, 0, 0);
			Action action = () => _sut.Evaluate(passwordContext);

			// Act & Assert
			action.Should().NotThrow<PasswordValidationException>();
		}
	}
}
