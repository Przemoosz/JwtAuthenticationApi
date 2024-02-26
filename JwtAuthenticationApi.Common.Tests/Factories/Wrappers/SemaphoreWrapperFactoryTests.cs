namespace JwtAuthenticationApi.Common.Tests.Factories.Wrappers
{
	using Abstraction.Wrappers.Threading;
	using Common.Factories.Wrappers;
	using Common.Wrappers.Threading;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture, Parallelizable]
	public class SemaphoreWrapperFactoryTests
	{
		private SemaphoreWrapperFactory _uut;

		[SetUp]
		public void SetUp()
		{
			_uut = new SemaphoreWrapperFactory();
		}

		[TestCase(0,2,"yyy")]
		[TestCase(1, 9, "zzz")]

		public void ShouldCreateSemaphoreWrapper(int initial, int max, string name)
		{
			// Act
			var actual = _uut.Create(initial, max, name);

			// Assert
			actual.Should().BeAssignableTo<ISemaphoreWrapper>();
			actual.Should().BeOfType<SemaphoreWrapper>();
		}
	}
}