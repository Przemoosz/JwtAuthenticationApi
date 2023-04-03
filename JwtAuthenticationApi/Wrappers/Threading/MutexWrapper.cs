namespace JwtAuthenticationApi.Wrappers.Threading
{
	/// <summary>
	/// <see cref="Mutex"/> wrapper.
	/// </summary>
	public class MutexWrapper: IMutexWrapper
	{
		private readonly Mutex _mutex;

		public MutexWrapper(bool initiallyOwned, string name)
		{
			_mutex = new Mutex(initiallyOwned, name);
		}

		public void WaitOne()
		{
			_mutex.WaitOne();
		}

		public void ReleaseMutex()
		{
			_mutex.ReleaseMutex();
		}

		public void Dispose()
		{
			_mutex?.Dispose();
		}
	}
}