namespace JwtAuthenticationApi.Wrappers.Threading
{
	/// <summary>
	/// Defines method for wrapping <see cref="Mutex"/> class.
	/// </summary>
	public interface IMutexWrapper : IDisposable
	{
		/// <inheritdoc cref="Mutex.WaitOne"/>
		void WaitOne();
		/// <inheritdoc cref="Mutex.ReleaseMutex"/>
		void ReleaseMutex();
	}
}