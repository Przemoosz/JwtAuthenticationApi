using System.Net;
using JwtAuthenticationApi.Controllers;
using JwtAuthenticationApi.Models.Registration.Requests;
using JwtAuthenticationApi.Models.Registration.Responses;
using JwtAuthenticationApi.Registration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TddXt.AnyRoot.Numbers;
using static TddXt.AnyRoot.Root;

namespace JwtAuthenticationApi.UnitTests.Controllers
{
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
		public async Task ShouldReturn201IfUserIsCreated()
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
		public async Task ShouldReturn400IfPasswordValidationFails()
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
		public async Task ShouldReturn409IfUserWithThatUserNameExists()
		{
			// Arrange
			var userId = Any.Integer();
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
		public async Task ShouldReturn500IfDbErrorOccurred()
		{
			// Arrange
			var userId = Any.Integer();
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
		public async Task ShouldReturn500IfInternalErrorOccurred()
		{
			// Arrange
			var userId = Any.Integer();
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
