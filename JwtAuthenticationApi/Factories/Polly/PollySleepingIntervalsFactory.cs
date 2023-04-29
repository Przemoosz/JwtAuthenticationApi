namespace JwtAuthenticationApi.Factories.Polly
{

	/// <summary>
	/// Implementation of <see cref="IPollySleepingIntervalsFactory"/> interface.
	/// </summary>
	public class PollySleepingIntervalsFactory : IPollySleepingIntervalsFactory
    {
        public IEnumerable<TimeSpan> CreateConstantInterval(uint sleepTime, uint retryCount)
        {
            TimeSpan[] sleepingIntervals = new TimeSpan[retryCount];
            for (int i = 0; i < retryCount; i++)
            {
                sleepingIntervals[i] = TimeSpan.FromSeconds(sleepTime);
            }
            return sleepingIntervals;
        }

        public IEnumerable<TimeSpan> CreateLinearInterval(uint baseSleepTime, uint sleepTimeIncrease, uint retryCount)
        {
            TimeSpan[] sleepingInterval = new TimeSpan[retryCount];
            for (int i = 0; i < retryCount; i++)
            {
                sleepingInterval[i] = TimeSpan.FromSeconds(baseSleepTime + sleepTimeIncrease * i);
            }

            return sleepingInterval;
        }
    }
}
