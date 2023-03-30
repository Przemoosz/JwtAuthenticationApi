using JwtAuthenticationApi.Wrappers.Threading;

namespace JwtAuthenticationApi.Factories.Wrappers
{
	public class MutexWrapperFactory : IMutexWrapperFactory
    {
        public IMutexWrapper Create(bool initiallyOwned, string name)
        {
            return new MutexWrapper(initiallyOwned, name);
        }
    }
}