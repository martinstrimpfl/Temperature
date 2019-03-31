using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

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
            var template = string.Empty;
            var temperature = 0;

            var temperatureProvider = new Mock<ITemperatureProvider>(MockBehavior.Strict);
            var writer = new Mock<TextWriter>(MockBehavior.Strict);
            writer
                .Setup(m => 
                    m.WriteLine(
                        It.IsAny<string>(), 
                        It.IsAny<City>(),  
                        It.IsAny<int>()))
                .Callback<string, object, object>((f,c,t) => { template = f; temperature = (int)t; });

            temperatureProvider
                .Setup(m => m.GetTemperature(It.IsAny<City>())).Returns(25);

            var writeTemperature = new TemperatureWriter(writer.Object, temperatureProvider.Object);

            // Act
            writeTemperature.WriteTemperature(City.Brno);

            // Assert
            Assert.IsTrue(template.Contains("°C"));
            Assert.AreEqual(25, temperature);
        }
    }
}