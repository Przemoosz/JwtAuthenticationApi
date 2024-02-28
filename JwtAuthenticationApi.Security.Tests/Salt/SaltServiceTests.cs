namespace JwtAuthenticationApi.Security.Tests.Salt
{
	using Common.Abstraction.Factories;
	using Common.Abstraction.Factories.Polly;
	using Common.Abstraction.Factories.Wrappers;
	using Common.Abstraction.Wrappers.Threading;
	using Common.Models;
	using FluentAssertions;
	using Infrastructure.Abstraction.Database;
	using Infrastructure.Entities;
	using Microsoft.EntityFrameworkCore;
	using MockQueryable.NSubstitute;
	using NSubstitute;
	using NSubstitute.ExceptionExtensions;
	using NUnit.Framework;
	using Security.Salt;
	using Serilog;
	using TddXt.AnyRoot.Numbers;
	using TddXt.AnyRoot.Strings;
	using static TddXt.AnyRoot.Root;

	[TestFixture, Parallelizable]
	public class SaltServiceTests
	{
		private IPasswordSaltContext _passwordSaltContext;
		private SaltService _uut;
		private IGuidFactory _guidWrapper;
		private IPollySleepingIntervalsFactory _pollySleepingIntervalsFactory;
		private ILogger _logger;
		#pragma warning disable NUnit1032
		private ISemaphoreWrapper _semaphoreWrapper;
		#pragma warning restore NUnit1032
		private ISemaphoreWrapperFactory _semaphoreWrapperFactory;

		[SetUp]
		public void SetUp()
		{
			_passwordSaltContext = Substitute.For<IPasswordSaltContext>();
			_guidWrapper = Substitute.For<IGuidFactory>();
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
		public async Task SaveSaltAsync_CreatesAndSavesNewSalt()
		{
			// Arrange
			int userId = Any.Integer();
			Guid salt = Guid.NewGuid();
			IEnumerable<PasswordSaltEntity> saltSource = Enumerable.Empty<PasswordSaltEntity>();
			DbSet<PasswordSaltEntity> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_guidWrapper.CreateGuid().Returns(salt);
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);

			// Act
			await _uut.SaveSaltAsync(salt.ToString(), userId);

			// Assert
			await _passwordSaltContext.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
		}

		[Test]
		public async Task GetSaltAsync_IfUserDoesNotExists_ReturnsNotSuccessfulResult()
		{
			// Arrange
			int userId = Any.Integer();
			IEnumerable<PasswordSaltEntity> saltSource = Enumerable.Empty<PasswordSaltEntity>();
			DbSet<PasswordSaltEntity> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);

			// Act
			Result<string> actual = await _uut.GetSaltAsync(userId);

			// Assert
			actual.IsSuccessful.Should().BeFalse();
			actual.Value.Should().BeNull();
		}

		[Test]
		public async Task GetSaltAsync_IfExceptionOccurs_ShouldRetryGettingSalt()
		{
			// Arrange
			int userId = Any.Integer();
			string salt = Any.String();
			PasswordSaltEntity passwordSalt = new PasswordSaltEntity(salt, userId);
			IEnumerable<PasswordSaltEntity> saltSource = new List<PasswordSaltEntity>(1) { passwordSalt };
			DbSet<PasswordSaltEntity> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_passwordSaltContext.PasswordSalt.Returns(_ => throw new Exception(), _ => saltDbSet);
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(2) { TimeSpan.Zero, TimeSpan.Zero });
			// Act
			var actual = await _uut.GetSaltAsync(userId);

			// Assert
			actual.IsSuccessful.Should().BeTrue();
			actual.Value.Should().Be(salt);
			_passwordSaltContext.PasswordSalt.ReceivedWithAnyArgs(2);
			_semaphoreWrapper.Received(1).Release();
		}

		[Test]
		public async Task GetSaltAsync_IfGettingSaltThrowsExceptionAfterRetries_ReturnsNotSuccessfulResult()
		{
			// Arrange
			int userId = Any.Integer();
			string salt = Any.String();
			PasswordSaltEntity passwordSalt = new PasswordSaltEntity(salt, userId);
			IEnumerable<PasswordSaltEntity> saltSource = new List<PasswordSaltEntity>(1) { passwordSalt };
			DbSet<PasswordSaltEntity> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_passwordSaltContext.PasswordSalt.Returns(x => throw new Exception(), x => throw new Exception(), x => throw new Exception(),x => saltDbSet);
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(2) { TimeSpan.Zero, TimeSpan.Zero });

			// Act
			var actual = await _uut.GetSaltAsync(userId);

			// Assert
			actual.IsSuccessful.Should().BeFalse();
			actual.Value.Should().Be(null);
			_passwordSaltContext.PasswordSalt.ReceivedWithAnyArgs(3);
			_semaphoreWrapper.Received(1).Release();
		}

		[Test]
		public async Task GetSaltAsync_IfUserExists_ReturnsSuccessfulResultWithSalt()
		{
			// Arrange
			int userId = Any.Integer();
			string salt = Any.String();
			PasswordSaltEntity passwordSalt = new PasswordSaltEntity(salt, userId);
			IEnumerable<PasswordSaltEntity> saltSource = new List<PasswordSaltEntity>(1) { passwordSalt };
			DbSet<PasswordSaltEntity> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);

			// Act
			var actual = await _uut.GetSaltAsync(userId);

			// Assert
			actual.IsSuccessful.Should().BeTrue();
			actual.Value.Should().Be(salt);
		}

		[Test]
		public async Task SaveSaltAsync_ReleasesSemaphoreAfterDatabaseExceptionOccursInSavingSaltMethod()
		{
			// Arrange
			int userId = Any.Integer();
			Guid salt = Guid.NewGuid();
			IEnumerable<PasswordSaltEntity> saltSource = Enumerable.Empty<PasswordSaltEntity>();
			DbSet<PasswordSaltEntity> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_guidWrapper.CreateGuid().Returns(salt);
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);
			_passwordSaltContext.SaveChangesAsync(Arg.Any<CancellationToken>())
				.ThrowsAsync(new DbUpdateException());

			Func<Task<int?>> func = async () => await _uut.SaveSaltAsync(salt.ToString(), userId);

			// Act & Assert
			await func.Should().ThrowAsync<DbUpdateException>();
			_semaphoreWrapper.Received(1).Release();
		}

		[Test]
		public async Task GetSaltAsync_ShouldReleaseSemaphoreAfterExceptionOccursInGettingSaltMethod()
		{
			// Arrange
			int userId = Any.Integer();
			string salt = Any.String();
			PasswordSaltEntity passwordSalt = new PasswordSaltEntity(salt, userId);
			IEnumerable<PasswordSaltEntity> saltSource = new List<PasswordSaltEntity>(1) { passwordSalt };
			DbSet<PasswordSaltEntity> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Throws<Exception>();
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);

			await _uut.GetSaltAsync(userId);
				
			// Act & Assert
			_logger.Received(1).Error(Arg.Any<Exception>(), Arg.Any<string>());
			_semaphoreWrapper.Received(1).Release();
		}

		[Test]
		public async Task SaveSaltAsync_IfExceptionOccurs_RetriesSavingChanges()
		{
			// Arrange
			int userId = Any.Integer();
			Guid salt = Guid.NewGuid();
			IEnumerable<PasswordSaltEntity> saltSource = Enumerable.Empty<PasswordSaltEntity>();
			DbSet<PasswordSaltEntity> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_guidWrapper.CreateGuid().Returns(salt);
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);
			_passwordSaltContext.SaveChangesAsync(Arg.Any<CancellationToken>())
				.Returns(x => throw new DbUpdateException(), x => throw new DbUpdateException(), x => 1);
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(2) { TimeSpan.Zero, TimeSpan.Zero });
			
			// Act
			await _uut.SaveSaltAsync(salt.ToString(), userId);

			// Assert
			await _passwordSaltContext.ReceivedWithAnyArgs(3).SaveChangesAsync(Arg.Any<CancellationToken>());
			_semaphoreWrapper.Received(1).Release();
		}

		[Test]
		public async Task SaveSaltAsync_IfExceptionStillOccursAfterRetries_ShouldThrowDbUpdateException()
		{
			// Arrange
			int userId = Any.Integer();
			Guid salt = Guid.NewGuid();
			IEnumerable<PasswordSaltEntity> saltSource = Enumerable.Empty<PasswordSaltEntity>();
			DbSet<PasswordSaltEntity> saltDbSet = saltSource.AsQueryable().BuildMockDbSet();
			_guidWrapper.CreateGuid().Returns(salt);
			_passwordSaltContext.PasswordSalt.Returns(saltDbSet);
			_passwordSaltContext.SaveChangesAsync(Arg.Any<CancellationToken>())
				.Returns(x => throw new DbUpdateException(), x => throw new DbUpdateException(),
					x => throw new DbUpdateException(), x => 1);
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(2) { TimeSpan.Zero, TimeSpan.Zero });

			Func<Task<int?>> function = async () => await _uut.SaveSaltAsync(salt.ToString(),userId);

			// Act & Assert
			await function.Should().ThrowAsync<DbUpdateException>();
			await _passwordSaltContext.ReceivedWithAnyArgs(3).SaveChangesAsync(Arg.Any<CancellationToken>());
			_semaphoreWrapper.Received(1).Release();
		}

		[Test]
		public void GenerateSalt_GeneratesSalt()
		{
			// Act
			var actual = _uut.GenerateSalt();
			// Assert
			actual.Should().NotBeNull();
			actual.Length.Should().BeGreaterThan(0);
		}
	}
}