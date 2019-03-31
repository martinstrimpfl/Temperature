using WeatherService;

namespace Weather
{
    public class TemperatureDirectProvider : ITemperatureProvider
    {
        private readonly TemperatureService temperatureService;

        public TemperatureDirectProvider(TemperatureService temperatureService)
        {
            this.temperatureService = temperatureService;
        }

        public int GetTemperature(City city)
        {
            return temperatureService.GetTemperature(city);
        }
    }
}
