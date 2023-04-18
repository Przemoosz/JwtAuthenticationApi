namespace JwtAuthenticationApi.UnitTests.Handlers
{
	using JwtAuthenticationApi.Abstraction.Commands;
	using JwtAuthenticationApi.Commands.Models;
	using JwtAuthenticationApi.Handlers;
	using TddXt.AnyRoot.Strings;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
	public class CommandHandlerTests
	{
		private CommandHandler _uut;

		[SetUp]
		public void SetUp()
		{
			_uut = new CommandHandler();
		}

		[Test]
		public async Task ShouldHandleCommandExecutionAndReturnResult()
		{
			// Arrange
			string stringResult = Any.String();
			ICommand<string> command = Substitute.For<ICommand<string>>();
			command.ExecuteAsync(CancellationToken.None).Returns(new Result<string>(stringResult, true));

			// Act
			var result = await _uut.HandleAsync(command, CancellationToken.None);

			// Assert
			result.Value.Should().Be(stringResult);
		}
	}
}
