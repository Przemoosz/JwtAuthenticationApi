namespace JwtAuthenticationApi.UnitTests.Factories.Wrappers
{
    using JwtAuthenticationApi.Factories.Wrappers;
    using JwtAuthenticationApi.Wrappers.Threading;

    [TestFixture, Parallelizable]
    public class MutexWrapperFactoryTests
    {
        private MutexWrapperFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new MutexWrapperFactory();
        }

        [TestCase(true, "Name")]
        [TestCase(false, "")]
        public void ShouldCreateMutexWrapper(bool initiallyOwned, string name)
        {
            // Act
            using IMutexWrapper actual = _uut.Create(initiallyOwned, name);

            // Assert
            actual.Should().BeAssignableTo<IMutexWrapper>();
            actual.Should().BeOfType<MutexWrapper>();
        }
    }
}