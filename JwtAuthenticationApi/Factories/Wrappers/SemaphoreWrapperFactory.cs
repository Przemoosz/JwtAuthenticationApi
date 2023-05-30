using JwtAuthenticationApi.Wrappers.Threading;

namespace JwtAuthenticationApi.Factories.Wrappers
{
	/// <summary>
	/// Implementation of <see cref="ISemaphoreWrapperFactory"/> interface. Creates and returns <see cref="SemaphoreWrapper"/>.
	/// </summary>
	public sealed class SemaphoreWrapperFactory: ISemaphoreWrapperFactory
	{
		public ISemaphoreWrapper Create(int initialCount, int maximalCount, string name)
		{
			return new SemaphoreWrapper(initialCount, maximalCount, name);
		}
	}
}