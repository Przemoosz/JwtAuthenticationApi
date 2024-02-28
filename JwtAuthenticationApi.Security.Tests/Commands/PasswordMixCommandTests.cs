namespace JwtAuthenticationApi.Security.Tests.Commands
{
	using Common.Exceptions;
	using Common.Models;
	using FluentAssertions;
	using NUnit.Framework;
	using Security.Commands;
	using TddXt.AnyRoot.Strings;
	using static TddXt.AnyRoot.Root;

	[TestFixture, Parallelizable]
	public class PasswordMixCommandTests
	{
		[Test]
		public async Task ExecuteAsync_MixesPasswordWithPepperAndSalt()
		{
			// Arrange
			var password = Any.String();
			var salt = Any.String();
			var pepper = Any.String();
			var uut = new PasswordMixCommand(password, salt, pepper);

			// Act
			var result = await uut.ExecuteAsync(CancellationToken.None);

			// Assert
			result.Value.Should().Be($"{password}{pepper}{salt}");
			result.IsSuccessful.Should().BeTrue();
		}

		[TestCase(null)]
		[TestCase("")]
		public async Task ExecuteAsync_IfProvidedPasswordIsNullOrEmpty_ThrowsCommandExecutionException(string password)
		{
			// Arrange
			var salt = Any.String();
			var pepper = Any.String();
			var uut = new PasswordMixCommand(password, salt, pepper);
			Func<Task<Result<string>>> function = async () => await uut.ExecuteAsync(CancellationToken.None);

			// Act and Assert
			await function.Should()
				.ThrowAsync<CommandExecutionException>()
				.WithMessage($"Cannot execute {nameof(PasswordMixCommand)} command.")
				.WithInnerException(typeof(ArgumentException))
				.WithMessage("Provided password can not be null or empty value.");
		}

		[TestCase(null)]
		[TestCase("")]
		public async Task ExecuteAsync_IfProvidedSaltIsNullOrEmpty_ThrowsCommandExecutionException(string salt)
		{
			// Arrange
			var password = Any.String();
			var pepper = Any.String();
			var uut = new PasswordMixCommand(password, salt, pepper);
			Func<Task<Result<string>>> function = async () => await uut.ExecuteAsync(CancellationToken.None);

			// Act and Assert
			await function.Should()
				.ThrowAsync<CommandExecutionException>()
				.WithMessage($"Cannot execute {nameof(PasswordMixCommand)} command.")
				.WithInnerException(typeof(ArgumentException))
				.WithMessage("Provided salt can not be null or empty value.");
		}

		[TestCase(null)]
		[TestCase("")]
		public async Task ExecuteAsync_IfProvidedPepperIsNullOrEmpty_ThrowsCommandExecutionException(string pepper)
		{
			// Arrange
			var password = Any.String();
			var salt = Any.String();
			var uut = new PasswordMixCommand(password, salt, pepper);
			Func<Task<Result<string>>> function = async () => await uut.ExecuteAsync(CancellationToken.None);

			// Act and Assert
			await function.Should()
				.ThrowAsync<CommandExecutionException>()
				.WithMessage($"Cannot execute {nameof(PasswordMixCommand)} command.")
				.WithInnerException(typeof(ArgumentException))
				.WithMessage("Provided pepper can not be null or empty value.");
		}
	}
}