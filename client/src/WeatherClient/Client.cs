using Polly;
using Polly.Retry;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherClient
{
	public class Client
	{
		private const int _maxRetries = 3;

		private readonly HttpClient _httpClient;
		private readonly AsyncRetryPolicy _retryPolicy;
		public Client(IRetryDelayCalculator retryDelayCalculator)
		{
			_httpClient = new HttpClient();
			_retryPolicy = Policy.Handle<HttpRequestException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
				.WaitAndRetryAsync(
				   retryCount: _maxRetries,
				   sleepDurationProvider: retryDelayCalculator.Calculate,
				   onRetry: (exception, sleepDuration, attemptNumber, context) =>
				   {
					   Log($"Too many requests. Retrying in {sleepDuration}. {attemptNumber} / {_maxRetries}");
				   });
		}

		public void Log(string message)
		{
			Console.WriteLine($"{DateTime.Now:hh:mm:ss.ffff} {message}");
		}

		public async Task<string> GetWeather()
		{
			return await _retryPolicy.ExecuteAsync(async () =>
			{
				var response = await _httpClient.GetAsync("https://localhost:44370/weatherforecast");
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadAsStringAsync();
			});
		}
	}
}

