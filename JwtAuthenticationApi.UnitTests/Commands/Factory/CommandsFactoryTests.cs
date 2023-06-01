namespace JwtAuthenticationApi.UnitTests.Commands.Factory
{
	using JwtAuthenticationApi.Commands.Factory;
	using JwtAuthenticationApi.Abstraction.Commands;
	using JwtAuthenticationApi.Commands;

	[TestFixture, Parallelizable]
	public class CommandsFactoryTests
	{
		private CommandsFactory _uut;

		[SetUp]
		public void SetUp()
		{
			_uut = new CommandsFactory();
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
	}
}