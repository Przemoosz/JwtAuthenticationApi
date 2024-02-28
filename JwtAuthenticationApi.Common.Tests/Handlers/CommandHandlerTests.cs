namespace JwtAuthenticationApi.Common.Tests.Handlers
{
	using Abstraction.Commands;
	using Common.Handlers;
	using FluentAssertions;
	using Models;
	using NSubstitute;
	using NUnit.Framework;
	using TddXt.AnyRoot.Strings;
	using static TddXt.AnyRoot.Root;

	[TestFixture, Parallelizable]
	public class CommandHandlerTests
	{
		private CommandHandler _uut;

		[SetUp]
		public void SetUp()
		{
			_uut = new CommandHandler();
		}

		[Test]
		public async Task HandleAsync_HandlesCommandExecutionAndReturnResult()
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
