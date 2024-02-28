namespace JwtAuthenticationApi.Common.Abstraction.Factories.Wrappers
{
    using JwtAuthenticationApi.Common.Abstraction.Wrappers.Threading;

    /// <summary>
    /// Defines method for <see cref="IMutexWrapper"/> factory class.
    /// </summary>
    public interface IMutexWrapperFactory
    {
        /// <summary>
        /// Creates <see cref="IMutexWrapper"/>.
        /// </summary>
        /// <param name="initiallyOwned">Defines if calling thread is mutex owner.</param>
        /// <param name="name">Mutex name.</param>
        /// <returns>New instance of <see cref="IMutexWrapper"/> with provided name.</returns>
        IMutexWrapper Create(bool initiallyOwned, string name);
    }
}