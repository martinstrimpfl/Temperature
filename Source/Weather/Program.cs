using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Caching;
using WeatherService;

namespace Weather
{
    internal static class Program
    {
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "Demo")]
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String,System.Object,System.Object)", Justification = "Demo")]
        public static void Main()
        {
            var cities = Enum.GetValues(typeof(City)).Cast<City>().ToList();
            var temperatureProvider = 
                new TemperatureCacheProvider(
                    MemoryCache.Default, 
                    new TemperatureDirectProvider(
                        new TemperatureService()));

            var temperatureWriter = new TemperatureWriter(Console.Out, temperatureProvider);

            do
            {
                Console.WriteLine("Actual temperature in selected cities:");
                foreach (var city in cities)
                {
                    temperatureWriter.WriteTemperature(city);
                }
                Console.WriteLine("Press Enter to refresh, any other key to exit.");
            }
            while (Console.ReadKey(true).Key == ConsoleKey.Enter);    
        }
    }
}
