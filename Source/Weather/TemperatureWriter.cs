using System;
using System.IO;
using System.Runtime.Caching;

using WeatherService;

namespace Weather
{
    public class TemperatureWriter
    {
        private readonly TemperatureService temperatureService = new TemperatureService();
        private MemoryCache cache = MemoryCache.Default;

        public void WriteTemperature(TextWriter writer, City city)
        {
            if(writer == null)
            {
                return;
            }
            var key = city.ToString();
            int temperature = 0;

            if(cache.Contains(key))
            {
                temperature = (int)cache.Get(key);
            }
            else
            {
                temperature = temperatureService.GetTemperature(city);
                cache.Add(key, temperature, DateTimeOffset.UtcNow.AddSeconds(5));
            }
            
            writer.WriteLine("{0,-10}: {1} °C", city, temperature);
        }
    }
}
