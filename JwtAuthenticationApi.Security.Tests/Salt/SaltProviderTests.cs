namespace JwtAuthenticationApi.Security.Tests.Salt
{
	using Abstraction.Salt;
	using Common.Models;
	using FluentAssertions;
	using NSubstitute;
	using NUnit.Framework;
	using Security.Salt;
	using TddXt.AnyRoot.Numbers;
	using static TddXt.AnyRoot.Root;

	[TestFixture, Parallelizable]
	public class SaltProviderTests
	{
		private ISaltService _saltService;
		private SaltProvider _uut;

		[SetUp]
		public void SetUp()
		{
			_saltService = Substitute.For<ISaltService>();
			_uut = new SaltProvider(_saltService);
		}

		[Test]
		public async Task ShouldCreateSaltIfUserIsNotInDatabase()
		{
			// Arrange
			int userId = Any.Integer();
			string salt = Guid.NewGuid().ToString();
			Result<string> getSaltResult = new Result<string>(null, false);
			_saltService.GetSaltAsync(userId).Returns(getSaltResult);
			_saltService.GenerateSalt().Returns(salt);
			
			// Act
			string actual = await _uut.GetPasswordSaltAsync(userId, CancellationToken.None);

			// Assert
			actual.Should().Be(salt);
		}

		[Test]
		public async Task ShouldReturnSaltFromDatabaseIfUserIsInDatabase()
		{
			// Arrange
			int userId = Any.Integer();
			string salt = Guid.NewGuid().ToString();
			Result<string> getSaltResult = new Result<string>(salt, true);
			_saltService.GetSaltAsync(userId).Returns(getSaltResult);

			// Act
			string actual = await _uut.GetPasswordSaltAsync(userId, CancellationToken.None);

			// Assert
			actual.Should().Be(salt);
		}
	}
}