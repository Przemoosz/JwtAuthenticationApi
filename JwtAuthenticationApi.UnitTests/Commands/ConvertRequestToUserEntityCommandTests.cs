﻿namespace JwtAuthenticationApi.UnitTests.Commands
{
    using JwtAuthenticationApi.Commands;
    using JwtAuthenticationApi.Models.Registration.Requests;
    using TddXt.AnyRoot.Strings;
    using static TddXt.AnyRoot.Root;


    [TestFixture, Parallelizable]
	public class ConvertRequestToUserEntityCommandTests
	{
		[Test]
		public async Task ShouldCreateUserModelFromRequest()
		{
			// Arrange
			RegisterUserRequest request = Any.Instance<RegisterUserRequest>();
			string hashedPassword = Any.String();
			var sut = new ConvertRequestToUserEntityCommand(request, hashedPassword);

			// Act
			var actual = await sut.ExecuteAsync(CancellationToken.None);

			// Assert
			actual.IsSuccessful.Should().BeTrue();
			actual.Value.Username.Should().Be(request.Username);
			actual.Value.HashedPassword.Should().Be(hashedPassword);
			actual.Value.Email.Should().Be(request.Email);
		}
	}
}
