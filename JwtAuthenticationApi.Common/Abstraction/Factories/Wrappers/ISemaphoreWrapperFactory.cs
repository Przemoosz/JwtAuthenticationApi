namespace JwtAuthenticationApi.Common.Abstraction.Factories.Wrappers
{
    using JwtAuthenticationApi.Common.Abstraction.Wrappers.Threading;
    using JwtAuthenticationApi.Common.Wrappers.Threading;

    /// <summary>
    /// Defines method for <see cref="ISemaphoreWrapper"/> factory class.
    /// </summary>
    public interface ISemaphoreWrapperFactory
    {
        /// <summary>
        /// Creates and returns <see cref="ISemaphoreWrapper"/>.
        /// </summary>
        /// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently</param>
        /// <param name="maximalCount">The maximum number of requests for the semaphore that can be granted concurrently.</param>
        /// <param name="name">
        /// The name, if the synchronization object is to be shared with other processes; otherwise, <see langword="null"/> or an empty string. The name is case-sensitive.
        /// </param>
        /// <returns>New instance of <see cref="SemaphoreWrapper"/>.</returns>
        ISemaphoreWrapper Create(int initialCount, int maximalCount, string name);
    }
}

