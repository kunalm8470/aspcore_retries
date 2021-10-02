using System;

namespace WeatherClient
{
    public interface IRetryDelayCalculator
    {
        public TimeSpan Calculate(int attemptNumber);
    }
}
