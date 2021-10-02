using System;
using System.Threading.Tasks;

namespace WeatherClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Client weatherClient = new(new ExponentialBackoffWithJitterCalculator());
                Console.WriteLine($"Weather = {await weatherClient.GetWeather()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request failed. {ex.Message}");
            }
        }
    }
}
