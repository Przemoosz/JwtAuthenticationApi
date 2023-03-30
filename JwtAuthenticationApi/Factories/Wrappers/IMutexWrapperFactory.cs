using JwtAuthenticationApi.Wrappers.Threading;

namespace JwtAuthenticationApi.Factories.Wrappers
{
	public interface IMutexWrapperFactory
    {
        IMutexWrapper Create(bool initiallyOwned, string name);
    }
}