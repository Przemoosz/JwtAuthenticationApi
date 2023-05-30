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

	[TestFixture, Parallelizable]
	public class SaltServiceTests
	{
		private IPasswordSaltContext _passwordSaltContext;
		private SaltService _uut;
		private IGuidWrapper _guidWrapper;
		private IPollySleepingIntervalsFactory _pollySleepingIntervalsFactory;
		private ILogger _logger;
		private ISemaphoreWrapper _semaphoreWrapper;
		private ISemaphoreWrapperFactory _semaphoreWrapperFactory;

		[SetUp]
		public void SetUp()
		{
			_passwordSaltContext = Substitute.For<IPasswordSaltContext>();
			_guidWrapper = Substitute.For<IGuidWrapper>();
			_semaphoreWrapper = Substitute.For<ISemaphoreWrapper>();
			_semaphoreWrapperFactory = Substitute.For<ISemaphoreWrapperFactory>();
			_semaphoreWrapperFactory.Create(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>())
				.Returns(_semaphoreWrapper);
			_pollySleepingIntervalsFactory = Substitute.For<IPollySleepingIntervalsFactory>();
			_logger = Substitute.For<ILogger>();
			_uut = new SaltService(_passwordSaltContext, _guidWrapper,
				_pollySleepingIntervalsFactory,_semaphoreWrapperFactory, _logger);
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
			_semaphoreWrapper.Received(1).Release();
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
			_semaphoreWrapper.Received(1).Release();
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
		public async Task ShouldReleaseSemaphoreAfterDatabaseExceptionOccursInSavingSaltMethod()
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
			_semaphoreWrapper.Received(1).Release();
		}

		[Test]
		public async Task ShouldReleaseSemaphoreAfterExceptionOccursInGettingSaltMethod()
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

			await _uut.GetSaltAsync(user);
				
			// Act & Assert
			_logger.Received(1).Error(Arg.Any<Exception>(), Arg.Any<string>());
			_semaphoreWrapper.Received(1).Release();
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
			_semaphoreWrapper.Received(1).Release();
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
			_semaphoreWrapper.Received(1).Release();
		}
	}
}