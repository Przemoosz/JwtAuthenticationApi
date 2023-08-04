using System.Diagnostics.CodeAnalysis;

namespace JwtAuthenticationApi.Wrappers.Threading
{
	[ExcludeFromCodeCoverage]
	/// <inheritdoc/>
	public sealed class SemaphoreWrapper: ISemaphoreWrapper
	{
		private readonly Semaphore _semaphore;

		/// <summary>
		/// Initializes new instance of <see cref="SemaphoreWrapper"/>.
		/// </summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently</param>
		/// <param name="maximalCount">The maximum number of requests for the semaphore that can be granted concurrently.</param>
		/// <param name="name">
		/// The name, if the synchronization object is to be shared with other processes; otherwise, <see langword="null"/> or an empty string. The name is case-sensitive.
		/// </param>
		public SemaphoreWrapper(int initialCount, int maximalCount, string name)
		{
			_semaphore = new Semaphore(initialCount, maximalCount, name);
		}
		public bool WaitOne()
		{
			return _semaphore.WaitOne();
		}
		public int Release()
		{
			return _semaphore.Release();
		}
		/// <inheritdoc cref="IDisposable.Dispose"/>
		public void Dispose()
		{
			_semaphore?.Dispose();
		}
	}
}