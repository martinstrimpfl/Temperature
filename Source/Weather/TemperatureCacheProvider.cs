using System;
using System.Runtime.Caching;

using WeatherService;

namespace Weather
{
    public class TemperatureCacheProvider : ITemperatureProvider
    {
        private MemoryCache cache = MemoryCache.Default;
        private ITemperatureProvider temperatureProvider = new TemperatureDirectProvider();

        public int GetTemperature(City city)
        {
            var key = city.ToString();
            int temperature = 0;

            if (cache.Contains(key))
            {
                temperature = (int)cache.Get(key);
            }
            else
            {
                temperature = temperatureProvider.GetTemperature(city);
                cache.Add(key, temperature, DateTimeOffset.UtcNow.AddSeconds(5));
            }

            return temperature;
        }
    }
}
