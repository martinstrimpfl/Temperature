using System.IO;

using WeatherService;

namespace Weather
{
    public class TemperatureWriter
    {
        private readonly TemperatureService temperatureService = new TemperatureService();

        public void WriteTemperature(TextWriter writer, City city)
        {
            if(writer == null)
            {
                return;
            }

            writer.WriteLine("{0,-10}: {1} °C", city, temperatureService.GetTemperature(city));
        }
    }
}
