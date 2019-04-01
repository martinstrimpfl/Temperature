using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using Autofac;
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

            var builder = new ContainerBuilder();

            builder.RegisterType<TemperatureDirectProvider>().As<ITemperatureProvider>();
            builder.RegisterType<TemperatureService>();
            builder.RegisterType<TemperatureCacheProvider>().As<ITemperatureProvider>();
            builder.RegisterType<TemperatureWriter>().As<ITemperatureWriter>();
            builder.RegisterInstance(MemoryCache.Default).As<MemoryCache>();
            builder.RegisterInstance(Console.Out).As<TextWriter>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var temperatureWriter = scope.Resolve<ITemperatureWriter>();
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
}
