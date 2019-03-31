using System.IO;

using WeatherService;

namespace Weather
{
    public class TemperatureWriter : ITemperatureWriter
    {
        private readonly TextWriter writer;
        private readonly ITemperatureProvider temperatureProvider = new TemperatureCacheProvider();

        public TemperatureWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void WriteTemperature(City city)
        {
            writer.WriteLine("{0,-10}: {1}°C", city, temperatureProvider.GetTemperature(city));
        }
    }
}
