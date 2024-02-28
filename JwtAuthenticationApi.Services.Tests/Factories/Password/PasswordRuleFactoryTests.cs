namespace JwtAuthenticationApi.Services.Tests.Factories.Password
{
	using Abstraction.RuleEngine;
	using FluentAssertions;
	using Models.Password;
	using NUnit.Framework;
	using Services.Factories.Password;
	using Services.Validators.Password.Rules;

	[TestFixture, Parallelizable]
	public sealed class PasswordRuleFactoryTests
	{
		private PasswordRuleFactory _sut;

		[SetUp]
		public void Setup()
		{
			_sut = new PasswordRuleFactory();
		}

		[Test]
		public void CreateEqualityRule_CreatesEqualityRule()
		{
			// Act
			var actual = _sut.CreateEqualityRule();

			// Assert
			actual.Should().BeOfType<EqualityRule>();
			actual.Should().BeAssignableTo<IRule<PasswordContext>>();
		}

		[Test]
		public void CreateLengthRule_CreatesLengthRule()
		{
			// Act
			var actual = _sut.CreateLengthRule();

			// Assert
			actual.Should().BeOfType<LengthRule>();
			actual.Should().BeAssignableTo<IRule<PasswordContext>>();
		}

		[Test]
		public void CreateLowerLettersRule_CreatesLowerLettersRule()
		{
			// Act
			var actual = _sut.CreateLowerLettersRule();

			// Assert
			actual.Should().BeOfType<LowerLettersRule>();
			actual.Should().BeAssignableTo<IRule<PasswordContext>>();
		}

		[Test]
		public void CreateUpperLettersRule_CreatesUpperLettersRule()
		{
			// Act
			var actual = _sut.CreateUpperLettersRule();

			// Assert
			actual.Should().BeOfType<UpperLettersRule>();
			actual.Should().BeAssignableTo<IRule<PasswordContext>>();
		}

		[Test]
		public void CreateSpecialLetterRule_CreatesSpecialLettersRule()
		{
			// Act
			var actual = _sut.CreateSpecialLetterRule();

			// Assert
			actual.Should().BeOfType<SpecialLettersRule>();
			actual.Should().BeAssignableTo<IRule<PasswordContext>>();
		}
	}
}
