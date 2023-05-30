using JwtAuthenticationApi.Wrappers.Threading;

namespace JwtAuthenticationApi.Factories.Wrappers
{
    /// <summary>
    /// Implementation of <see cref="IMutexWrapperFactory"/> interface. Factory class for <see cref="IMutexWrapper"/>.
    /// </summary>
	public sealed class MutexWrapperFactory : IMutexWrapperFactory
    {
	    /// <inheritdoc/>
		public IMutexWrapper Create(bool initiallyOwned, string name)
        {
            return new MutexWrapper(initiallyOwned, name);
        }
    }
}