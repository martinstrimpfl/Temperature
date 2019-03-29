using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherService;

namespace Weather.Tests
{
    [TestClass()]
    public class TemperatureWriterTests
    {
        [TestMethod()]
        public void WriteTemperatureTest()
        {
            // Arrange
            var writeTemperature = new TemperatureWriter();
            var stringBuilder = new StringBuilder();

            // Act
            using (var writer = new StringWriter(stringBuilder))
            {
                writeTemperature.WriteTemperature(writer, City.Brno);
            }

            // Assert
            Assert.IsTrue(stringBuilder.ToString().Contains("°C"));
        }
    }
}