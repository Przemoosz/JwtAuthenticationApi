namespace JwtAuthenticationApi.UnitTests.Security.Password.Salt
{
	using JwtAuthenticationApi.Factories.Polly;
	using Serilog;
	using JwtAuthenticationApi.Commands.Models;
	using DatabaseContext;
	using Models;
	using JwtAuthenticationApi.Security.Password.Salt;
	using JwtAuthenticationApi.Wrappers;
	using Microsoft.EntityFrameworkCore;
	using MockQueryable.NSubstitute;
	using TddXt.AnyRoot.Strings;
	using JwtAuthenticationApi.Factories.Wrappers;
	using JwtAuthenticationApi.Wrappers.Threading;
	using NSubstitute.ExceptionExtensions;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
	public class SaltServiceTests
	{
		private IPasswordSaltContext _passwordSaltContext;
		private SaltService _uut;
		private IGuidWrapper _guidWrapper;
		private IMutexWrapperFactory _mutexWrapperFactory;
		private IMutexWrapper _mutexWrapper;
		private IPollySleepingIntervalsFactory _pollySleepingIntervalsFactory;
		private ILogger _logger;

		[SetUp]
		public void SetUp()
		{
			_passwordSaltContext = Substitute.For<IPasswordSaltContext>();
			_guidWrapper = Substitute.For<IGuidWrapper>();
			_mutexWrapperFactory = Substitute.For<IMutexWrapperFactory>();
			_mutexWrapper = Substitute.For<IMutexWrapper>();
			_mutexWrapperFactory.Create(Arg.Any<bool>(), Arg.Any<string>()).Returns(_mutexWrapper);
			_pollySleepingIntervalsFactory = Substitute.For<IPollySleepingIntervalsFactory>();
			_logger = Substitute.For<ILogger>();
			_uut = new SaltService(_passwordSaltContext, _guidWrapper, _mutexWrapperFactory,
				_pollySleepingIntervalsFactory, _logger);
		}

		[Test]
		public async Task ShouldCreateAndSaveNewSalt()
		{
			// Arrange
			UserModel user = Any.Instance<UserModel>();
			Guid salt = Guid.NewGuid();
			IEnumerable<PasswordSaltModel> saltSource = Enumerable.Empty<PasswordSaltModel>();
			DbSet<PasswordSaltModel> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_guidWrapper.CreateGuid().Returns(salt);
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);

			// Act
			var actual = await _uut.CreateAndSaveSaltAsync(user);

			// Assert
			actual.Should().Be(salt.ToString());
			await _passwordSaltContext.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
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
		public async Task ShouldRetryGettingSaltIfExceptionOccurs()
		{
			// Arrange
			Guid userId = Guid.NewGuid();
			string salt = Any.String();
			UserModel user = new UserModel() { Id = userId };
			PasswordSaltModel passwordSalt = new PasswordSaltModel() { Id = Guid.Empty, UserId = userId, Salt = salt };
			IEnumerable<PasswordSaltModel> saltSource = new List<PasswordSaltModel>(1) { passwordSalt };
			DbSet<PasswordSaltModel> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_passwordSaltContext.PasswordSalt.Returns(x => throw new Exception(), x => saltDbSet);
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(2) { TimeSpan.Zero, TimeSpan.Zero });
			// Act
			var actual = await _uut.GetSaltAsync(user);

			// Assert
			actual.IsSuccessful.Should().BeTrue();
			actual.Value.Should().Be(salt);
			_passwordSaltContext.PasswordSalt.ReceivedWithAnyArgs(2);
			_mutexWrapper.Received(1).ReleaseMutex();
		}

		[Test]
		public async Task ShouldReturnNotSuccessfulResultIfGettingSaltThrowsExceptionAfterRetries()
		{
			// Arrange
			Guid userId = Guid.NewGuid();
			string salt = Any.String();
			UserModel user = new UserModel() { Id = userId };
			PasswordSaltModel passwordSalt = new PasswordSaltModel() { Id = Guid.Empty, UserId = userId, Salt = salt };
			IEnumerable<PasswordSaltModel> saltSource = new List<PasswordSaltModel>(1) { passwordSalt };
			DbSet<PasswordSaltModel> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_passwordSaltContext.PasswordSalt.Returns(x => throw new Exception(), x => throw new Exception(), x => throw new Exception(),x => saltDbSet);
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(2) { TimeSpan.Zero, TimeSpan.Zero });

			// Act
			var actual = await _uut.GetSaltAsync(user);

			// Assert
			actual.IsSuccessful.Should().BeFalse();
			actual.Value.Should().Be(null);
			_passwordSaltContext.PasswordSalt.ReceivedWithAnyArgs(3);
			_mutexWrapper.Received(1).ReleaseMutex();
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

		[Test]
		public async Task ShouldReleaseMutexAfterDatabaseExceptionOccursInSavingSaltMethod()
		{
			// Arrange
			UserModel user = Any.Instance<UserModel>();
			Guid salt = Guid.NewGuid();
			IEnumerable<PasswordSaltModel> saltSource = Enumerable.Empty<PasswordSaltModel>();
			DbSet<PasswordSaltModel> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_guidWrapper.CreateGuid().Returns(salt);
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);
			_passwordSaltContext.SaveChangesAsync(Arg.Any<CancellationToken>())
				.ThrowsAsync(new DbUpdateException());

			Func<Task<string>> func = async () => await _uut.CreateAndSaveSaltAsync(user);

			// Act & Assert
			await func.Should().ThrowAsync<DbUpdateException>();
			_mutexWrapper.Received(1).ReleaseMutex();
		}

		[Test]
		public async Task ShouldReleaseMutexAfterExceptionOccursInGettingSaltMethod()
		{
			// Arrange
			Guid userId = Guid.NewGuid();
			string salt = Any.String();
			UserModel user = new UserModel() { Id = userId };
			PasswordSaltModel passwordSalt = new PasswordSaltModel() { Id = Guid.Empty, UserId = userId, Salt = salt };
			IEnumerable<PasswordSaltModel> saltSource = new List<PasswordSaltModel>(1) { passwordSalt };
			DbSet<PasswordSaltModel> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Throws<Exception>();
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);

			Func<Task<Result<string>>> func = async () => await _uut.GetSaltAsync(user);
				
			// Act & Assert
			await func.Should().ThrowAsync<Exception>();
			_mutexWrapper.Received(1).ReleaseMutex();
		}

		[Test]
		public async Task ShouldRetrySavingChangesIfExceptionOccurs()
		{
			// Arrange
			UserModel user = Any.Instance<UserModel>();
			Guid salt = Guid.NewGuid();
			IEnumerable<PasswordSaltModel> saltSource = Enumerable.Empty<PasswordSaltModel>();
			DbSet<PasswordSaltModel> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_guidWrapper.CreateGuid().Returns(salt);
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);
			_passwordSaltContext.SaveChangesAsync(Arg.Any<CancellationToken>())
				.Returns(x => throw new DbUpdateException(), x => throw new DbUpdateException(), x => 1);
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(2) { TimeSpan.Zero, TimeSpan.Zero });
			
			// Act
			var actual = await _uut.CreateAndSaveSaltAsync(user);

			// Assert
			actual.Should().Be(salt.ToString());
			await _passwordSaltContext.ReceivedWithAnyArgs(3).SaveChangesAsync(Arg.Any<CancellationToken>());
			_mutexWrapper.Received(1).ReleaseMutex();
		}

		[Test]
		public async Task ShouldThrowExceptionIfExceptionStillOccursAfterRetries()
		{
			// Arrange
			UserModel user = Any.Instance<UserModel>();
			Guid salt = Guid.NewGuid();
			IEnumerable<PasswordSaltModel> saltSource = Enumerable.Empty<PasswordSaltModel>();
			DbSet<PasswordSaltModel> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_guidWrapper.CreateGuid().Returns(salt);
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);
			_passwordSaltContext.SaveChangesAsync(Arg.Any<CancellationToken>())
				.Returns(x => throw new DbUpdateException(), x => throw new DbUpdateException(),
					x => throw new DbUpdateException(), x => 1);
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(2) { TimeSpan.Zero, TimeSpan.Zero });

			Func<Task<string>> function = async () => await _uut.CreateAndSaveSaltAsync(user);

			// Act & Assert
			await function.Should().ThrowAsync<DbUpdateException>();
			await _passwordSaltContext.ReceivedWithAnyArgs(3).SaveChangesAsync(Arg.Any<CancellationToken>());
			_mutexWrapper.Received(1).ReleaseMutex();
		}
	}
}