using System;

namespace WeatherClient
{
    public class ExponentialBackoffWithJitterCalculator : IRetryDelayCalculator
	{
		private readonly Random _random;
		private readonly object _randomLock;

		public ExponentialBackoffWithJitterCalculator()
		{
			_random = new Random();
			_randomLock = new object();
		}

		public TimeSpan Calculate(int attemptNumber)
		{
			int jitter = 0;

			// Random is not thread-safe
			lock (_randomLock)
            {
				jitter = _random.Next(10, 500);
            }

			return TimeSpan.FromSeconds(Math.Pow(2, attemptNumber - 1)) + TimeSpan.FromMilliseconds(jitter);
		}
	}
}
