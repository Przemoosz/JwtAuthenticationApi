namespace JwtAuthenticationApi.UnitTests.Security.Password.Salt
{
	using JwtAuthenticationApi.Commands.Models;
	using Models;
	using JwtAuthenticationApi.Security.Password.Salt;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
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
			UserModel user = Any.Instance<UserModel>();
			string salt = Guid.NewGuid().ToString();
			Result<string> getSaltResult = new Result<string>(null, false);
			_saltService.GetSaltAsync(user).Returns(getSaltResult);
			_saltService.CreateAndSaveSaltAsync(user).Returns(salt);
			
			// Act
			string actual = await _uut.GetPasswordSaltAsync(user);

			// Assert
			actual.Should().Be(salt);
		}

		[Test]
		public async Task ShouldReturnSaltFromDatabaseIfUserIsInDatabase()
		{
			// Arrange
			UserModel user = Any.Instance<UserModel>();
			string salt = Guid.NewGuid().ToString();
			Result<string> getSaltResult = new Result<string>(salt, true);
			_saltService.GetSaltAsync(user).Returns(getSaltResult);

			// Act
			string actual = await _uut.GetPasswordSaltAsync(user);

			// Assert
			actual.Should().Be(salt);
		}
	}
}