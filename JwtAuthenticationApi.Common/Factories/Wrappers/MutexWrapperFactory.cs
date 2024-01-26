namespace JwtAuthenticationApi.Common.Factories.Wrappers
{
    using JwtAuthenticationApi.Common.Abstraction.Factories.Wrappers;
    using JwtAuthenticationApi.Common.Abstraction.Wrappers.Threading;
    using JwtAuthenticationApi.Common.Wrappers.Threading;


    /// <summary>
    /// Implementation of <see cref="IMutexWrapperFactory"/> interface. Factory class for <see cref="IMutexWrapper"/>.
    /// </summary>
    internal sealed class MutexWrapperFactory : IMutexWrapperFactory
    {
	    /// <inheritdoc/>
		public IMutexWrapper Create(bool initiallyOwned, string name)
        {
            return new MutexWrapper(initiallyOwned, name);
        }
    }
}