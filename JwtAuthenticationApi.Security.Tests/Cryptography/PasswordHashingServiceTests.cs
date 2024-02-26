namespace JwtAuthenticationApi.Security.Tests.Cryptography
{
	using Common.Abstraction.Commands;
	using Common.Abstraction.Handlers;
	using Common.Models;
	using Common.Options;
	using FluentAssertions;
	using Microsoft.Extensions.Options;
	using NSubstitute;
	using NUnit.Framework;
	using Security.Cryptography;

	[TestFixture, Parallelizable]
	public class PasswordHashingServiceTests
	{
		private IOptions<PasswordPepper> _passwordOptions;
		private ICommandHandler _commandHandler;
		private PasswordHashingService _uut;

		[SetUp]
		public void SetUp()
		{
			_passwordOptions = Substitute.For<IOptions<PasswordPepper>>();
			_commandHandler = Substitute.For<ICommandHandler>();
			_uut = new PasswordHashingService(_passwordOptions, _commandHandler);
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
			_commandHandler.HandleAsync(command, Arg.Any<CancellationToken>())
				.Returns(new Result<string>(mixedPassword, true));
			
			// Act
			var actual = await _uut.HashPasswordAsync(password, salt, CancellationToken.None);

			// Arrange
			actual.Should().Be(expectedHash);
		}
	}
}
