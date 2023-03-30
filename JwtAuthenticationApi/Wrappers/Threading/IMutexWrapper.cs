namespace JwtAuthenticationApi.Wrappers.Threading
{
	public interface IMutexWrapper : IDisposable
	{
		void WaitOne();
		void ReleaseMutex();
	}
}