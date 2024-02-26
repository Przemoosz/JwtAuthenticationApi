namespace JwtAuthenticationApi.Services.Tests.Factories.Password
{
	using Abstraction.RuleEngine;
	using FluentAssertions;
	using Models.Password;
	using NUnit.Framework;
	using Services.Factories.Password;
	using Validators.Password.Rules;

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
		public void ShouldCreateEqualityRule()
		{
			// Act
			var actual = _sut.CreateEqualityRule();

			// Assert
			actual.Should().BeOfType<EqualityRule>();
			actual.Should().BeAssignableTo<IRule<PasswordContext>>();
		}

		[Test]
		public void ShouldCreateLengthRule()
		{
			// Act
			var actual = _sut.CreateLengthRule();

			// Assert
			actual.Should().BeOfType<LengthRule>();
			actual.Should().BeAssignableTo<IRule<PasswordContext>>();
		}

		[Test]
		public void ShouldCreateLowerLettersRule()
		{
			// Act
			var actual = _sut.CreateLowerLettersRule();

			// Assert
			actual.Should().BeOfType<LowerLettersRule>();
			actual.Should().BeAssignableTo<IRule<PasswordContext>>();
		}

		[Test]
		public void ShouldCreateUpperLettersRule()
		{
			// Act
			var actual = _sut.CreateUpperLettersRule();

			// Assert
			actual.Should().BeOfType<UpperLettersRule>();
			actual.Should().BeAssignableTo<IRule<PasswordContext>>();
		}

		[Test]
		public void ShouldCreateSpecialLettersRule()
		{
			// Act
			var actual = _sut.CreateSpecialLetterRule();

			// Assert
			actual.Should().BeOfType<SpecialLettersRule>();
			actual.Should().BeAssignableTo<IRule<PasswordContext>>();
		}
	}
}
