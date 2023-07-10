namespace JwtAuthenticationApi.UnitTests.Commands
{
	using JwtAuthenticationApi.Commands;
	using Models.Requests;
	using TddXt.AnyRoot.Strings;
	using static TddXt.AnyRoot.Root;


	[TestFixture, Parallelizable]
	public class CreateUserModelFromRequestCommandTests
	{
		[Test]
		public async Task ShouldCreateUserModelFromRequest()
		{
			// Arrange
			RegisterUserRequest request = Any.Instance<RegisterUserRequest>();
			string hashedPassword = Any.String();
			var sut = new CreateUserModelFromRequestCommand(request, hashedPassword);

			// Act
			var actual = await sut.ExecuteAsync(CancellationToken.None);

			// Assert
			actual.IsSuccessful.Should().BeTrue();
			actual.Value.UserName.Should().Be(request.UserName);
			actual.Value.HashedPassword.Should().Be(hashedPassword);
			actual.Value.Email.Should().Be(request.Email);
		}
	}
}
