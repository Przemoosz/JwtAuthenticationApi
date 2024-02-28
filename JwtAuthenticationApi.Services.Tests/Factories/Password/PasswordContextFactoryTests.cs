namespace JwtAuthenticationApi.Services.Tests.Factories.Password
{
	using FluentAssertions;
	using NUnit.Framework;
	using Services.Factories.Password;

	[TestFixture, Parallelizable]
	public class PasswordContextFactoryTests
	{
		private PasswordContextFactory _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new PasswordContextFactory();
		}

		[TestCase("AAAaaaAAA",6)]
		[TestCase("CvCvCC", 4)]
		[TestCase("aaa", 0)]
		public void Create_CountsCorrectlyUpperLetters(string password, int totalUpperLetters)
		{
			// Act
			var actual = _sut.Create(password, password);

			// Assert
			actual.TotalUpperCaseLetters.Should().Be(totalUpperLetters);
		}

		[TestCase("AAAaaaAAA", 3)]
		[TestCase("CvCvCC", 2)]
		[TestCase("AAA", 0)]
		public void Create_CountsCorrectlyLowerLetters(string password, int totalUpperLetters)
		{
			// Act
			var actual = _sut.Create(password, password);

			// Assert
			actual.TotalLowerCaseLetters.Should().Be(totalUpperLetters);
		}

		[TestCase("!Password!", 2)]
		[TestCase("!;[]ggg78CCSs&^", 8)]
		[TestCase("AAA", 0)]
		public void Create_CountsCorrectlySpecialLetters(string password, int totalUpperLetters)
		{
			// Act
			var actual = _sut.Create(password, password);

			// Assert
			actual.TotalSpecialCharacters.Should().Be(totalUpperLetters);
		}

		[Test]
		public void Create_CorrectlyCreatePasswordContext()
		{
			// Arrange
			const string password = "!HArDToBr3a4Passw0rd%";
			const string passwordConfirmation = "Confirm";
			const int totalUpperLetters = 6;
			const int totalLowerLetters = 10;
			const int totalSpecialCharacters = 5;

			// Act
			var actual = _sut.Create(password, passwordConfirmation);

			// Assert
			actual.PasswordLength.Should().Be(password.Length);
			actual.Password.Should().Be(password);
			actual.PasswordConfirmation.Should().Be(passwordConfirmation);
			actual.TotalLowerCaseLetters.Should().Be(totalLowerLetters);
			actual.TotalUpperCaseLetters.Should().Be(totalUpperLetters);
			actual.TotalSpecialCharacters.Should().Be(totalSpecialCharacters);
		}

	}
}
