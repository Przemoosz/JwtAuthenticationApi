using JwtAuthenticationApi.Entities;

namespace JwtAuthenticationApi.UnitTests.Commands.Factory
{
    using JwtAuthenticationApi.Commands.Factory;
    using JwtAuthenticationApi.Abstraction.Commands;
    using JwtAuthenticationApi.Commands;
    using Models;

    [TestFixture, Parallelizable]
	public class CommandsFactoryTests
	{
		private CommandFactory _uut;

		[SetUp]
		public void SetUp()
		{
			_uut = new CommandFactory();
		}

		[Test]
		public void ShouldCreatePasswordMixCommand()
		{
			// Act
			var actual = _uut.CreatePasswordMixCommand(null, null, null);

			// Assert
			actual.Should().NotBeNull();
			actual.Should().BeOfType<PasswordMixCommand>();
			actual.Should().BeAssignableTo<ICommand<string>>();
		}

		[Test]
		public void ShouldCreateUserModelFromRequestCommand()
		{
			// Act
			var actual = _uut.CreateUserEntityFromRequestCommand(null, null);

			// Assert
			actual.Should().NotBeNull();
			actual.Should().BeOfType<CreateUserModelFromRequestCommand>();
			actual.Should().BeAssignableTo<ICommand<UserEntity>>();
		}
	}
}