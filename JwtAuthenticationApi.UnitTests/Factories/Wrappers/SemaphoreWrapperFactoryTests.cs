using JwtAuthenticationApi.Factories.Wrappers;
using JwtAuthenticationApi.Wrappers.Threading;

namespace JwtAuthenticationApi.UnitTests.Factories.Wrappers
{
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