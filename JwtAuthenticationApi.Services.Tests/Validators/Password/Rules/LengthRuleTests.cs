namespace JwtAuthenticationApi.Services.Tests.Validators.Password.Rules
{
	using Common.Constants;
	using Exceptions;
	using FluentAssertions;
	using Models.Password;
	using NUnit.Framework;
	using Services.Validators.Password.Rules;

	[TestFixture, Parallelizable]
    public sealed class LengthRuleTests
    {
        private LengthRule _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new LengthRule();
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
