using WeatherService;

namespace Weather
{
    public interface ITemperatureWriter
    {
        void WriteTemperature(City city);
    }
}
