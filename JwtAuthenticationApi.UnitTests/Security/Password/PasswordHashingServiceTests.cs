namespace JwtAuthenticationApi.UnitTests.Security.Password
{
	using JwtAuthenticationApi.Commands.Factory;
	using JwtAuthenticationApi.Handlers;
	using Models.Options;
	using JwtAuthenticationApi.Security.Password;
	using Microsoft.Extensions.Options;
	using JwtAuthenticationApi.Abstraction.Commands;
	using JwtAuthenticationApi.Commands.Models;

	[TestFixture, Parallelizable]
	public class PasswordHashingServiceTests
	{
		private IOptions<PasswordPepper> _passwordOptions;
		private ICommandHandler _commandHandler;
		private ICommandFactory _commandFactory;
		private PasswordHashingService _uut;

		[SetUp]
		public void SetUp()
		{
			_passwordOptions = Substitute.For<IOptions<PasswordPepper>>();
			_commandHandler = Substitute.For<ICommandHandler>();
			_commandFactory = Substitute.For<ICommandFactory>();
			_uut = new PasswordHashingService(_passwordOptions, _commandHandler, _commandFactory);
		}

		[Test]
		public async Task ShouldHashPassword()
		{
			// Arrange
			const string salt = "SALT";
			const string pepper = "PEPPER";
			const string password = "PASSWORD";
			const string expectedHash = "FsjT2moPUhUXzwImF0vUbj+Rd4QFgfYvOFcKbqSL4rY=";
			const string mixedPassword = $"{password}{pepper}{salt}";
			ICommand<string> command = Substitute.For<ICommand<string>>();
			_passwordOptions.Value.Returns(new PasswordPepper() { Pepper = pepper });
			_commandFactory.CreatePasswordMixCommand(password, salt, pepper).Returns(command);
			_commandHandler.HandleAsync(command, Arg.Any<CancellationToken>())
				.Returns(new Result<string>(mixedPassword, true));
			
			// Act
			var actual = await _uut.HashPasswordAsync(password, salt, CancellationToken.None);

			// Arrange
			actual.Should().Be(expectedHash);
		}
	}
}
