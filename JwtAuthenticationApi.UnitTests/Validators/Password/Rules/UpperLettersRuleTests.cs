﻿namespace JwtAuthenticationApi.UnitTests.Validators.Password.Rules
{
    using Exceptions;
    using JwtAuthenticationApi.Models.Password;
    using TestHelpers.Attributes;
    using JwtAuthenticationApi.Validators.Password.Rules;

    [TestFixture, Parallelizable, RuleTest]
    public sealed class UpperLettersRuleTests
    {
        private UpperLettersRule _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpperLettersRule();
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
            action.Should().Throw<PasswordValidationException>().WithMessage("Provided passwords does not contain upper letters.");
        }

        [Test]
        public void ShouldNotThrowPasswordValidationExceptionIfPasswordsContainsUpperLetters()
        {
            // Arrange
            var passwordContext = new PasswordContext(null, null, 0, 7, 0, 0);
            Action action = () => _sut.Evaluate(passwordContext);

            // Act & Assert
            action.Should().NotThrow<PasswordValidationException>();
        }
    }
}
