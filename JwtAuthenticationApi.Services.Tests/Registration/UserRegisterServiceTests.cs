using static TddXt.AnyRoot.Root;

namespace JwtAuthenticationApi.Services.Tests.Registration
{
	using Abstraction.Identity.User;
	using Abstraction.Validators.Password;
	using Common.Abstraction.Commands;
	using Common.Abstraction.Handlers;
	using Common.Models;
	using FluentAssertions;
	using Infrastructure.Entities;
	using Microsoft.EntityFrameworkCore;
	using Models.Enums;
	using Models.Registration.Requests;
	using NSubstitute;
	using NSubstitute.ExceptionExtensions;
	using NSubstitute.ReturnsExtensions;
	using NUnit.Framework;
	using Security.Abstraction.Cryptography;
	using Security.Abstraction.Salt;
	using Serilog;
	using Services.Registration;
	using TddXt.AnyRoot.Numbers;
	using TddXt.AnyRoot.Strings;

	[TestFixture, Parallelizable]
	public sealed class UserRegisterServiceTests
	{
		private ISaltService _saltService;
		private IPasswordValidator _passwordValidator;
		private IPasswordHashingService _passwordHashingService;
		private ICommandHandler _commandHandler;
		private IUserService _userService;
		private ILogger _logger;
		private UserRegisterService _sut;
		private ICommand<UserEntity> _convertRequestToUserEntityCommand;

		[SetUp]
		public void Setup()
		{
			_saltService = Substitute.For<ISaltService>();
			_passwordValidator = Substitute.For<IPasswordValidator>();
			_passwordHashingService = Substitute.For<IPasswordHashingService>();
			_commandHandler = Substitute.For<ICommandHandler>();
			_userService = Substitute.For<IUserService>();
			_logger = Substitute.For<ILogger>();
			_convertRequestToUserEntityCommand = Substitute.For<ICommand<UserEntity>>();

			_sut = new UserRegisterService(_saltService, _passwordValidator, _passwordHashingService, _commandHandler, _userService, _logger);
		}

		[Test]
		public async Task ShouldReturnValidationErrorIfPasswordValidationFails()
		{
			// Arrange
			var request = Any.Instance<RegisterUserRequest>();
			_passwordValidator.Validate(request.Password, request.PasswordConfirmation).Returns(false);
			
			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);

			// Assert
			actual.ErrorType.Should().Be(ErrorType.PasswordValidationError);
			actual.ErrorMessage.Should().NotBeNull();
			actual.IsSuccessful.Should().BeFalse();
			actual.UserId.Should().Be(0);
		}

		[Test]
		public async Task ShouldReturnInternalErrorIfErrorOccurredDuringEntityCreation()
		{
			// Arrange
			var request = Any.Instance<RegisterUserRequest>();
			var salt = Any.String();
			_passwordValidator.Validate(request.Password, request.PasswordConfirmation).Returns(true);
			_saltService.GenerateSalt().Returns(salt);
			_commandHandler.HandleAsync(_convertRequestToUserEntityCommand, CancellationToken.None)
				.Returns(new Result<UserEntity>(null, false));

			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);

			// Assert
			actual.ErrorType.Should().Be(ErrorType.InternalError);
			actual.ErrorMessage.Should().Be("Error occurred during entity creation");
			actual.IsSuccessful.Should().BeFalse();
			actual.UserId.Should().Be(0);
		}

		[Test]
		public async Task ShouldReturnInternalErrorIfErrorOccurredDuringSavingUserInDatabase()
		{
			// Arrange
			var request = Any.Instance<RegisterUserRequest>();
			var salt = Any.String();
			var userEntity = Any.Instance<UserEntity>();
			_passwordValidator.Validate(request.Password, request.PasswordConfirmation).Returns(true);
			_saltService.GenerateSalt().Returns(salt);
			_commandHandler.HandleAsync(_convertRequestToUserEntityCommand, CancellationToken.None)
				.Returns(new Result<UserEntity>(userEntity, true));
			_userService.SaveUserAsync(userEntity, CancellationToken.None).ReturnsNull();

			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);

			// Assert
			actual.ErrorType.Should().Be(ErrorType.DbError);
			actual.ErrorMessage.Should().Be("Error occurred during saving user in database - No Id retrieved for new user.");
			actual.IsSuccessful.Should().BeFalse();
			actual.UserId.Should().Be(0);
		}

		[Test]
		public async Task ShouldReturnUserIdIfEveryStepIsSuccessful()
		{
			// Arrange
			var request = Any.Instance<RegisterUserRequest>();
			var salt = Any.String();
			var userEntity = Any.Instance<UserEntity>();
			var id = Any.Integer();
			_passwordValidator.Validate(request.Password, request.PasswordConfirmation).Returns(true);
			_saltService.GenerateSalt().Returns(salt);
			_commandHandler.HandleAsync(_convertRequestToUserEntityCommand, CancellationToken.None)
				.Returns(new Result<UserEntity>(userEntity, true));
			_userService.SaveUserAsync(userEntity, CancellationToken.None).Returns(id);

			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);

			// Assert
			await _saltService.Received(1).SaveSaltAsync(salt, id);
			actual.ErrorMessage.Should().BeNull();
			actual.IsSuccessful.Should().BeTrue();
			actual.UserId.Should().Be(id);
		}

		[Test]
		public async Task ShouldReturnUserExistsIfDbUpdateExceptionOccurred()
		{
			// Arrange
			var request = Any.Instance<RegisterUserRequest>();
			var salt = Any.String();
			var userEntity = Any.Instance<UserEntity>();
			_passwordValidator.Validate(request.Password, request.PasswordConfirmation).Returns(true);
			_saltService.GenerateSalt().Returns(salt);
			_commandHandler.HandleAsync(_convertRequestToUserEntityCommand, CancellationToken.None)
				.Returns(new Result<UserEntity>(userEntity, true));
			_userService.SaveUserAsync(userEntity, CancellationToken.None).ThrowsAsync<DbUpdateException>();
			_userService.UserExistsAsync(request.Username).Returns(true);

			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);

			// Assert
			actual.ErrorType.Should().Be(ErrorType.DbErrorEntityExists);
			actual.ErrorMessage.Should().NotBeNull();
			actual.IsSuccessful.Should().BeFalse();
			actual.UserId.Should().Be(0);
		}


		[Test]
		public async Task ShouldReturnDbErrorIfDbUpdateExceptionOccurredAndUserDoesNotExists()
		{
			// Arrange
			var request = Any.Instance<RegisterUserRequest>();
			var salt = Any.String();
			var userEntity = Any.Instance<UserEntity>();
			_passwordValidator.Validate(request.Password, request.PasswordConfirmation).Returns(true);
			_saltService.GenerateSalt().Returns(salt);
			_commandHandler.HandleAsync(_convertRequestToUserEntityCommand, CancellationToken.None)
				.Returns(new Result<UserEntity>(userEntity, true));
			_userService.SaveUserAsync(userEntity, CancellationToken.None).ThrowsAsync<DbUpdateException>();
			_userService.UserExistsAsync(request.Username).Returns(false);

			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);

			// Assert
			actual.ErrorType.Should().Be(ErrorType.DbError);
			actual.ErrorMessage.Should().NotBeNull();
			actual.IsSuccessful.Should().BeFalse();
			actual.UserId.Should().Be(0);
		}

		[Test]
		public async Task ShouldReturnInternalErrorIfAnyErrorThatIsNotHandledOccurs()
		{
			// Arrange
			var request = Any.Instance<RegisterUserRequest>();
			_passwordValidator.Validate(request.Password, request.PasswordConfirmation).Throws<Exception>();

			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);

			// Assert
			actual.ErrorType.Should().Be(ErrorType.InternalError);
			actual.ErrorMessage.Should().Be("Unhandled Exception occurred during user registration process");
			actual.IsSuccessful.Should().BeFalse();
			actual.UserId.Should().Be(0);
		}

	}
}
