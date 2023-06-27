namespace JwtAuthenticationApi.UnitTests.Factories.Password
{
	using Abstraction.RuleEngine;
	using JwtAuthenticationApi.Factories.Password;
	using JwtAuthenticationApi.Models.Password;
	using TestHelpers.Attributes;
	using JwtAuthenticationApi.Validators.Password.RuleEngine.Rules;

	[TestFixture, Parallelizable, FactoryTest]
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
