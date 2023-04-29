namespace JwtAuthenticationApi.Factories.Polly
{

	/// <summary>
	/// Defines method for polly sleeping intervals factory class.
	/// </summary>
	public interface IPollySleepingIntervalsFactory
	{
		/// <summary>
		/// Creates constant time polly sleep interval and returns it as collection of <see cref="TimeSpan"/>.
		/// </summary>
		/// <param name="sleepTime">Constant value of sleep time per retry.</param>
		/// <param name="retryCount">Total amount of retries.</param>
		/// <returns><see cref="IEnumerable{T}"/> that contains sleep intervals as a <see cref="TimeSpan"/></returns>
		IEnumerable<TimeSpan> CreateConstantInterval(uint sleepTime, uint retryCount);

		/// <summary>
		/// Creates time polly sleep interval that increases linear and returns it as collection of <see cref="TimeSpan"/>.
		/// </summary>
		/// <param name="baseSleepTime">Starting sleep time.</param>
		/// <param name="sleepTimeIncrease">Defines increase of time interval between retries.</param>
		/// <param name="retryCount">Total amount of retires.</param>
		/// <returns><see cref="IEnumerable{T}"/> that contains sleep intervals as a <see cref="TimeSpan"/></returns>
		IEnumerable<TimeSpan> CreateLinearInterval(uint baseSleepTime, uint sleepTimeIncrease, uint retryCount);
	}
}