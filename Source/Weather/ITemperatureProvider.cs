using WeatherService;

namespace Weather
{
    public interface ITemperatureProvider
    {
        int GetTemperature(City city);
    }
}
