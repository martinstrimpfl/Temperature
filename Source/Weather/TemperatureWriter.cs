using System.IO;

using WeatherService;

namespace Weather
{
    public class TemperatureWriter : ITemperatureWriter
    {
        private readonly TextWriter writer;
        private readonly ITemperatureProvider temperatureProvider;

        public TemperatureWriter(TextWriter writer, ITemperatureProvider temperatureProvider)
        {
            this.writer = writer;
            this.temperatureProvider = temperatureProvider;
        }

        public void WriteTemperature(City city)
        {
            writer.WriteLine("{0,-10}: {1}°C", city, temperatureProvider.GetTemperature(city));
        }
    }
}
