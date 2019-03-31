using WeatherService;

namespace Weather
{
    public class TemperatureDirectProvider : ITemperatureProvider
    {
        private readonly TemperatureService temperatureService = new TemperatureService();

        public int GetTemperature(City city)
        {
            return temperatureService.GetTemperature(city);
        }
    }
}
