using System;
using System.Threading.Tasks;

namespace WeatherService
{
    public class TemperatureService
    {
        private Random random = new Random();

        private int currentTemperature = 20;

        public int GetTemperature(City city)
        {
            int delay = random.Next(500, 2000);

            Task.Delay(delay).GetAwaiter().GetResult();

            if ((currentTemperature + city.ToString().Length) % 2 == 0)
            {
                currentTemperature = random.Next(currentTemperature - 1, currentTemperature + 3);
            }
            else
            {
                currentTemperature = random.Next(currentTemperature - 2, currentTemperature + 1);
            }

            return currentTemperature;
        }
    }
}
