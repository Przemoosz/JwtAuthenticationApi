namespace JwtAuthenticationApi.Common.Factories.Wrappers
{
    using JwtAuthenticationApi.Common.Abstraction.Factories.Wrappers;
    using JwtAuthenticationApi.Common.Abstraction.Wrappers.Threading;
    using JwtAuthenticationApi.Common.Wrappers.Threading;

	/// <summary>
	/// Implementation of <see cref="ISemaphoreWrapperFactory"/> interface. Creates and returns <see cref="SemaphoreWrapper"/>.
	/// </summary>
	internal sealed class SemaphoreWrapperFactory: ISemaphoreWrapperFactory
	{
		public ISemaphoreWrapper Create(int initialCount, int maximalCount, string name)
		{
			return new SemaphoreWrapper(initialCount, maximalCount, name);
		}
	}
}