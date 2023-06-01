namespace JwtAuthenticationApi.UnitTests.Wrappers
{
	using JwtAuthenticationApi.Wrappers;

	[TestFixture, Parallelizable]
	public class GuidWrapperTests
	{
		private GuidWrapper _uut;

		[SetUp]
		public void SetUp()
		{
			_uut = new GuidWrapper();
		}

		[Test]
		public void ShouldCreateGuid()
		{
			// Act
			Guid actual = _uut.CreateGuid();

			// Assert
			actual.Should().NotBeEmpty();
		}
	}
}