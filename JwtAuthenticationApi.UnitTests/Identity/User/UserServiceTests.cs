﻿namespace JwtAuthenticationApi.UnitTests.Identity.User
{
    using DatabaseContext;
    using JwtAuthenticationApi.Factories.Polly;
    using JwtAuthenticationApi.Identity.User;
    using Serilog;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using TddXt.AnyRoot.Numbers;
    using static TddXt.AnyRoot.Root;
    using MockQueryable.NSubstitute;
    using JwtAuthenticationApi.Abstraction.DatabaseContext;

    [TestFixture, Parallelizable]
	public sealed class UserServiceTests
	{
		private IUserContext _userContext;
		private IPollySleepingIntervalsFactory _pollySleepingIntervalsFactory;
		private ILogger _logger;
		private UserService _sut;

		[SetUp]
		public void SetUp()
		{
			_userContext = Substitute.For<IUserContext>();
			_pollySleepingIntervalsFactory = Substitute.For<IPollySleepingIntervalsFactory>();
			_logger = Substitute.For<ILogger>();
			_sut = new UserService(_userContext, _pollySleepingIntervalsFactory, _logger);
		}

		[Test]
		public async Task ShouldSaveUserInDatabase()
		{
			// Arrange
			UserEntity userEntity = Any.Instance<UserEntity>();
			var userDbSet = new List<UserEntity>().AsQueryable().BuildMockDbSet();
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(1)
				{
					TimeSpan.Zero
				});
			_userContext.Users.Returns(userDbSet);
			_userContext.SaveChangesAsync(CancellationToken.None).Returns(1);

			// Act
			int? acutal = await _sut.SaveUserAsync(userEntity, CancellationToken.None);

			// Assert
			acutal.HasValue.Should().BeTrue();
			await _userContext.Received(1).SaveChangesAsync(CancellationToken.None);
		}

		[Test]
		public async Task ShouldRetrySavingUserIfDbUpdateExceptionOccurs()
		{
			// Arrange
			UserEntity userEntity = Any.Instance<UserEntity>();
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(1)
				{
					TimeSpan.Zero,
					TimeSpan.Zero
				});
			_userContext.SaveChangesAsync(CancellationToken.None).Returns(_ => throw new DbUpdateException(), _ => 1);

			// Act
			var acutal = await _sut.SaveUserAsync(userEntity, CancellationToken.None);

			// Assert
			acutal.HasValue.Should().BeTrue();
			await _userContext.Received(2).SaveChangesAsync(CancellationToken.None);
		}

		[Test]
		public async Task ShouldThrowDbUpdateExceptionIfExceptionIsReceivedAfterMaxRetryCount()
		{
			// Arrange
			int id = Any.Integer();
			UserEntity userEntity = Any.Instance<UserEntity>();
			_pollySleepingIntervalsFactory.CreateLinearInterval(Arg.Any<uint>(), Arg.Any<uint>(), Arg.Any<uint>())
				.Returns(new List<TimeSpan>(1)
				{
					TimeSpan.Zero,
					TimeSpan.Zero,
					TimeSpan.Zero
				});
			_userContext.SaveChangesAsync(CancellationToken.None).Returns(_ => throw new DbUpdateException(),
				_ => throw new DbUpdateException(),
				_ => throw new DbUpdateException(),
				_ => throw new DbUpdateException(),
				_ => id);
			Func<Task<int?>> func = async () => await _sut.SaveUserAsync(userEntity, CancellationToken.None);

			// Act & Assert
			await func.Should().ThrowAsync<DbUpdateException>();
			await _userContext.Received(4).SaveChangesAsync(CancellationToken.None);
		}

		[Test]
		public async Task ShouldReturnTrueIfUserExistsBasedOnUserName()
		{
			// Arrange
			UserEntity userEntity = Any.Instance<UserEntity>();
			var userEntitiesDbSet = new List<UserEntity>(1){userEntity}.AsQueryable().BuildMockDbSet();
			_userContext.Users.Returns(userEntitiesDbSet);

			// Act
			var actual = await _sut.UserExistsAsync(userEntity.Username);

			// Assert
			actual.Should().BeTrue();
		}

		[Test]
		public async Task ShouldReturnFalseIfUserDoesNotExistsBasedOnUserName()
		{
			// Arrange
			UserEntity userEntity = Any.Instance<UserEntity>();
			var userEntitiesDbSet = new List<UserEntity>(1) { userEntity }.AsQueryable().BuildMockDbSet();
			_userContext.Users.Returns(userEntitiesDbSet);

			// Act
			var actual = await _sut.UserExistsAsync(string.Empty);

			// Assert
			actual.Should().BeFalse();
		}

		[Test]
		public async Task ShouldReturnTrueIfUserExistsBasedOnUserId()
		{
			// Arrange
			UserEntity userEntity = Any.Instance<UserEntity>();
			var userEntitiesDbSet = new List<UserEntity>(1) { userEntity }.AsQueryable().BuildMockDbSet();
			_userContext.Users.Returns(userEntitiesDbSet);

			// Act
			var actual = await _sut.UserExistsAsync(userEntity.Id);

			// Assert
			actual.Should().BeTrue();
		}

		[Test]
		public async Task ShouldReturnFalseIfUserDoesNotExistsBasedOnUserID()
		{
			// Arrange
			UserEntity userEntity = Any.Instance<UserEntity>();
			var userEntitiesDbSet = new List<UserEntity>(1) { userEntity }.AsQueryable().BuildMockDbSet();
			_userContext.Users.Returns(userEntitiesDbSet);

			// Act
			var actual = await _sut.UserExistsAsync(-1);

			// Assert
			actual.Should().BeFalse();
		}

	}
}
