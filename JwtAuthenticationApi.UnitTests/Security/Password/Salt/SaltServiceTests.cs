namespace JwtAuthenticationApi.UnitTests.Security.Password.Salt
{
	using JwtAuthenticationApi.Commands.Models;
	using DatabaseContext;
	using Models;
	using JwtAuthenticationApi.Security.Password.Salt;
	using JwtAuthenticationApi.Wrappers;
	using Microsoft.EntityFrameworkCore;
	using MockQueryable.NSubstitute;
	using TddXt.AnyRoot.Strings;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
	public class SaltServiceTests
	{
		private IPasswordSaltContext _passwordSaltContext;
		private SaltService _uut;
		private IGuidWrapper _guidWrapper;

		[SetUp]
		public void SetUp()
		{
			_passwordSaltContext = Substitute.For<IPasswordSaltContext>();
			_guidWrapper = Substitute.For<IGuidWrapper>();
			_uut = new SaltService(_passwordSaltContext,_guidWrapper);
		}

		[Test]
		public async Task ShouldCreateAndSaveNewSalt()
		{
			// Arrange
			UserModel user = Any.Instance<UserModel>();
			Guid salt = Guid.NewGuid();
			_guidWrapper.CreateGuid().Returns(salt);
			IEnumerable<PasswordSaltModel> saltSource = Enumerable.Empty<PasswordSaltModel>();
			var saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);

			// Act
			var actual = await _uut.CreateAndSaveSaltAsync(user);

			// Assert
			actual.Should().Be(salt.ToString());
			await _passwordSaltContext.Received(1).SaveChangesAsync();
		}

		[Test]
		public async Task ShouldReturnNotSuccessfulResultIfUserDoesNotExists()
		{
			// Arrange
			UserModel user = Any.Instance<UserModel>();
			IEnumerable<PasswordSaltModel> saltSource = Enumerable.Empty<PasswordSaltModel>();
			DbSet<PasswordSaltModel> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);

			// Act
			Result<string> actual = await _uut.GetSaltAsync(user);

			// Assert
			actual.IsSuccessful.Should().BeFalse();
			actual.Value.Should().BeNull();
		}

		[Test]
		public async Task ShouldReturnSuccessfulResultWithSaltIfUserExists()
		{
			// Arrange
			Guid userId = Guid.NewGuid();
			string salt = Any.String();
			UserModel user = new UserModel(){Id = userId};
			PasswordSaltModel passwordSalt = new PasswordSaltModel(){Id = Guid.Empty, UserId = userId, Salt = salt};
			IEnumerable<PasswordSaltModel> saltSource = new List<PasswordSaltModel>(1) { passwordSalt };
			DbSet<PasswordSaltModel> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);

			// Act
			var actual = await _uut.GetSaltAsync(user);

			// Assert
			actual.IsSuccessful.Should().BeTrue();
			actual.Value.Should().Be(salt);
		}
	}
}