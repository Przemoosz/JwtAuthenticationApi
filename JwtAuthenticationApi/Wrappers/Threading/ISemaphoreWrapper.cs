namespace JwtAuthenticationApi.Wrappers.Threading;

/// <summary>
/// Defines methods for <see cref="Semaphore"/> wrapper.
/// </summary>
public interface ISemaphoreWrapper: IDisposable
{
	/// <summary>
	/// Blocks current thread until the current <see cref="WaitHandle"/> receives a signal.
	/// </summary>
	/// <returns>
	/// <see langword="true"/> if the current instance receives a signal. If the current instance is never signaled, WaitOne() never returns.
	/// </returns>
	bool WaitOne();
	/// <inheritdoc cref="Semaphore.Release"/>
	int Release();
}