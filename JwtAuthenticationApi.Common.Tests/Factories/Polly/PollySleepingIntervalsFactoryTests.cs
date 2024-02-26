namespace JwtAuthenticationApi.Common.Tests.Factories.Polly
{
	using Common.Factories.Polly;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture, Parallelizable]
	public class PollySleepingIntervalsFactoryTests
	{
		private PollySleepingIntervalsFactory _uut;

		[SetUp]
		public void SetUp()
		{
			_uut = new PollySleepingIntervalsFactory();
		}

		[TestCase(1u,6u)]
		[TestCase(6u,9u)]
		public void ShouldCreateConstantSleepingInterval(uint sleepTime, uint totalRetries)
		{
			// Act
			var actual = _uut.CreateConstantInterval(sleepTime, totalRetries).ToList();

			// Assert
			actual.Should().NotBeNull();
			actual.Count.Should().Be((int) totalRetries);
			foreach (var timeSpan in actual)
			{
				timeSpan.Seconds.Should().Be((int) sleepTime);
			}
		}

		[TestCase(2u,1u, 6u)]
		[TestCase(3u,4u, 9u)]
		public void ShouldCreateLinearSleepingInterval(uint baseSleepTime, uint sleepIncreaseTime, uint totalRetries)
		{
			// Act
			var actual = _uut.CreateLinearInterval(baseSleepTime,sleepIncreaseTime, totalRetries).ToList();
			   
			// Assert
			actual.Should().NotBeNull();
			actual.Count.Should().Be((int) totalRetries);
			for (int i = 0; i < totalRetries; i++)
			{
				actual[i].Seconds.Should().Be((int)(baseSleepTime + i * sleepIncreaseTime));
			}
		}
	}
}