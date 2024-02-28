namespace JwtAuthenticationApi.Services.Tests.Validators.Password
{
	using Abstraction.Factories.Password;
	using Abstraction.RuleEngine;
	using Exceptions;
	using FluentAssertions;
	using Models.Password;
	using NSubstitute;
	using NUnit.Framework;
	using Serilog;
	using Services.Validators.Password;
	using TddXt.AnyRoot.Strings;
	using static TddXt.AnyRoot.Root;

	[TestFixture, Parallelizable]
	public sealed class PasswordValidatorTests
	{
		private IPasswordContextFactory _passwordContextFactory;
		private IPasswordRuleFactory _passwordRuleFactory;
		private IRuleEngine<PasswordContext> _passwordRuleEngine;
		private ILogger _logger;
		private PasswordValidator _sut;

		[SetUp]
		public void SetUp()
		{
			_passwordContextFactory = Substitute.For<IPasswordContextFactory>();
			_passwordRuleFactory = Substitute.For<IPasswordRuleFactory>();
			_passwordRuleEngine = Substitute.For<IRuleEngine<PasswordContext>>();
			_logger = Substitute.For<ILogger>();
			_sut = new PasswordValidator(_passwordContextFactory, _passwordRuleEngine, _passwordRuleFactory, _logger);
		}

		[Test]
		public void Validate_IfPasswordIsValid_ReturnsTrue()
		{
			// Arrange
			string password = Any.String();
			string passwordConfirmation = Any.String();
			var passwordContext = Any.Instance<PasswordContext>();
			var rule = Substitute.For<IRule<PasswordContext>>();
			_passwordRuleFactory.CreateEqualityRule().Returns(rule);
			_passwordRuleFactory.CreateLengthRule().Returns(rule);
			_passwordRuleFactory.CreateLowerLettersRule().Returns(rule);
			_passwordRuleFactory.CreateSpecialLetterRule().Returns(rule);
			_passwordRuleFactory.CreateUpperLettersRule().Returns(rule);
			_passwordContextFactory.Create(password, passwordConfirmation).Returns(passwordContext);

			// Act
			var actual = _sut.Validate(password, passwordConfirmation);

			// Assert
			actual.Should().BeTrue();
		}

		[Test]
		public void Validate_IfPasswordIsInvalid_ReturnsFalse()
		{
			// Arrange
			string password = Any.String();
			string passwordConfirmation = Any.String();
			var passwordContext = Any.Instance<PasswordContext>();
			var rule = Substitute.For<IRule<PasswordContext>>();
			_passwordRuleEngine.When(s =>
				s.Validate(Arg.Any<PasswordContext>(), Arg.Any<IEnumerable<IRule<PasswordContext>>>())).Do(
				x => throw new PasswordValidationException());
			_passwordRuleFactory.CreateEqualityRule().Returns(rule);
			_passwordRuleFactory.CreateLengthRule().Returns(rule);
			_passwordRuleFactory.CreateLowerLettersRule().Returns(rule);
			_passwordRuleFactory.CreateSpecialLetterRule().Returns(rule);
			_passwordRuleFactory.CreateUpperLettersRule().Returns(rule);
			_passwordContextFactory.Create(password, passwordConfirmation).Returns(passwordContext);

			// Act
			var actual = _sut.Validate(password, passwordConfirmation);

			// Assert
			actual.Should().BeFalse();
		}
	}
}
