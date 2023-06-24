namespace JwtAuthenticationApi.UnitTests.Validators.Password.RuleEngine.Rules
{
	using Constants;
	using Exceptions;
	using JwtAuthenticationApi.Models.Password;
	using TestHelpers.Attributes;
	using JwtAuthenticationApi.Validators.Password.RuleEngine.Rules;


	[TestFixture, Parallelizable, RuleTest]
	public sealed class PasswordLengthRuleTests
	{
		private PasswordLengthRule _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new PasswordLengthRule();
		}

		[Test]
		public void ShouldReturnTrueForCanEvaluateRule()
		{
			// Act & Assert
			_sut.CanEvaluateRule(null).Should().BeTrue();
		}

		[Test]
		public void ShouldThrowPasswordValidationExceptionIfPasswordsAreTooShort()
		{
			// Arrange
			var passwordContext = new PasswordContext(null, null, JaaConstants.MinPasswordLength - 1, 0, 0, 0);
			Action action = () => _sut.Evaluate(passwordContext);

			// Act & Assert
			action.Should().Throw<PasswordValidationException>().WithMessage("Password length is not valid.");
		}

		[Test]
		public void ShouldThrowPasswordValidationExceptionIfPasswordsAreTooLong()
		{
			// Arrange
			var passwordContext = new PasswordContext(null, null, JaaConstants.MaxPasswordLength + 9, 0, 0, 0);
			Action action = () => _sut.Evaluate(passwordContext);

			// Act & Assert
			action.Should().Throw<PasswordValidationException>().WithMessage("Password length is not valid.");
		}

		[Test]
		public void ShouldNotThrowPasswordValidationExceptionIfPasswordsLengthIsCorrect()
		{
			// Arrange
			var passwordContext = new PasswordContext(null, null, JaaConstants.MaxPasswordLength - 9, 0, 0, 0);
			Action action = () => _sut.Evaluate(passwordContext);

			// Act & Assert
			action.Should().NotThrow<PasswordValidationException>();
		}

	}
}
