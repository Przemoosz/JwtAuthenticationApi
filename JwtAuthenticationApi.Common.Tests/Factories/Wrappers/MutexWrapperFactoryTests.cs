namespace JwtAuthenticationApi.Common.Tests.Factories.Wrappers
{
	using Abstraction.Wrappers.Threading;
	using Common.Factories.Wrappers;
	using Common.Wrappers.Threading;
	using FluentAssertions;
	using NUnit.Framework;

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
        public void Create_CreatesMutexWrapper(bool initiallyOwned, string name)
        {
            // Act
            using IMutexWrapper actual = _uut.Create(initiallyOwned, name);

            // Assert
            actual.Should().BeAssignableTo<IMutexWrapper>();
            actual.Should().BeOfType<MutexWrapper>();
        }
    }
}