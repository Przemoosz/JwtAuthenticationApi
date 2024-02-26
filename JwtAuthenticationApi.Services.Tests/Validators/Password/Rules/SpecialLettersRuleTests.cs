namespace JwtAuthenticationApi.Services.Tests.Validators.Password.Rules
{
	using Exceptions;
	using FluentAssertions;
	using Models.Password;
	using NUnit.Framework;
	using Services.Validators.Password.Rules;

	[TestFixture, Parallelizable]
    public sealed class SpecialLettersRuleTests
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