namespace JwtAuthenticationApi.Controllers.Tests.Controllers
{
	using System.Net;
	using FluentAssertions;
	using JwtAuthenticationApi.Controllers.Controllers;
	using Microsoft.AspNetCore.Mvc;
	using NSubstitute;
	using NUnit.Framework;
	using Services.Abstraction.Registration;
	using Services.Models.Enums;
	using Services.Models.Registration.Requests;
	using Services.Models.Registration.Responses;
	using TddXt.AnyRoot.Numbers;
	using static TddXt.AnyRoot.Root;

	[TestFixture, Parallelizable]
	public sealed class UserRegisterControllerTests
	{
		private IUserRegisterService _userRegisterService;
		private UserRegisterController _sut;

		[SetUp]
		public void SetUp()
		{
			_userRegisterService = Substitute.For<IUserRegisterService>();
			_sut = new UserRegisterController(_userRegisterService);
		}

		[Test]
		public async Task RegisterUserAsync_IfUserIsCreated_Returns201Created()
		{
			// Arrange
			var userId = Any.Integer();
			var serviceResponse = new RegisterUserResponse()
			{
				IsSuccessful = true,
				UserId = userId
			};

			var request = Any.Instance<RegisterUserRequest>();
			_userRegisterService.RegisterUserAsync(request, CancellationToken.None)
				.Returns(serviceResponse);
			
			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);
			var convertedActual = (CreatedResult) actual;

			// Assert
			convertedActual.Should().NotBeNull();
			convertedActual.StatusCode.Should().Be(201);
		}

		[Test]
		public async Task RegisterUserAsync_IfPasswordValidationFails_Return400BadRequest()
		{
			// Arrange
			var userId = Any.Integer();
			var serviceResponse = new RegisterUserResponse()
			{
				IsSuccessful = false,
				ErrorType = ErrorType.PasswordValidationError
			};

			var request = Any.Instance<RegisterUserRequest>();
			_userRegisterService.RegisterUserAsync(request, CancellationToken.None)
				.Returns(serviceResponse);

			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);
			var convertedActual = (BadRequestObjectResult)actual;

			// Assert
			convertedActual.Should().NotBeNull();
			convertedActual.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
		}

		[Test]
		public async Task RegisterUserAsync_IfUserNameExists_Return409Conflict()
		{
			// Arrange
			var serviceResponse = new RegisterUserResponse()
			{
				IsSuccessful = false,
				ErrorType = ErrorType.DbErrorEntityExists
			};

			var request = Any.Instance<RegisterUserRequest>();
			_userRegisterService.RegisterUserAsync(request, CancellationToken.None)
				.Returns(serviceResponse);

			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);
			var convertedActual = (ConflictObjectResult)actual;

			// Assert
			convertedActual.Should().NotBeNull();
			convertedActual.StatusCode.Should().Be((int)HttpStatusCode.Conflict);
		}

		[Test]
		public async Task RegisterUserAsync_IfDbErrorOccurred_Returns500InternalServerError()
		{
			// Arrange
			var serviceResponse = new RegisterUserResponse()
			{
				IsSuccessful = false,
				ErrorType = ErrorType.DbError
			};

			var request = Any.Instance<RegisterUserRequest>();
			_userRegisterService.RegisterUserAsync(request, CancellationToken.None)
				.Returns(serviceResponse);

			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);
			var convertedActual = (ObjectResult)actual;

			// Assert
			convertedActual.Should().NotBeNull();
			convertedActual.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
		}

		[Test]
		public async Task RegisterUserAsync_IfUnexpectedErrorOccured_Return500InternalServerError()
		{
			// Arrange
			var serviceResponse = new RegisterUserResponse()
			{
				IsSuccessful = false,
				ErrorType = ErrorType.InternalError
			};

			var request = Any.Instance<RegisterUserRequest>();
			_userRegisterService.RegisterUserAsync(request, CancellationToken.None)
				.Returns(serviceResponse);

			// Act
			var actual = await _sut.RegisterUserAsync(request, CancellationToken.None);
			var convertedActual = (ObjectResult)actual;

			// Assert
			convertedActual.Should().NotBeNull();
			convertedActual.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
		}
	}
}
