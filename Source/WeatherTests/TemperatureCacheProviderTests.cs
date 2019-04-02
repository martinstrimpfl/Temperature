using System;
using System.Runtime.Caching;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using WeatherService;

namespace Weather.Tests
{
    [TestClass()]
    public class TemperatureCacheProviderTests
    {
        [TestMethod()]
        public void CanGetValueFromCache()
        {
            // Arrange
            var mockCache = new Mock<ObjectCache>(MockBehavior.Strict);
            mockCache.Setup(m => m.Contains(It.IsAny<string>(), null)).Returns(true);
            mockCache.Setup(m => m.Get(It.IsAny<string>(), null)).Returns(20);

            var provider = new TemperatureCacheProvider(mockCache.Object, null);

            // Act
            var temperature = provider.GetTemperature(City.Brno);

            // Assert
            mockCache.Verify(m => m.Get(It.IsAny<string>(), null), Times.Once);
            Assert.AreEqual(20, temperature);
        }

        [TestMethod()]
        public void CanGetValueIfNotInCache()
        {
            // Arrange
            var mockCache = new Mock<ObjectCache>(MockBehavior.Strict);
            mockCache.Setup(m => m.Contains(It.IsAny<string>(), null)).Returns(false).Verifiable();

            mockCache
                .Setup(m => 
                    m.Add(
                        "Brno",
                        It.IsAny<object>(),
                        It.IsAny<DateTimeOffset>(),
                        null))
                .Returns(true)
                .Verifiable();

            var mockDirectProvider = new Mock<ITemperatureProvider>(MockBehavior.Strict);
            mockDirectProvider.Setup(m => m.GetTemperature(It.IsAny<City>())).Returns(30);

            var provider = new TemperatureCacheProvider(mockCache.Object, mockDirectProvider.Object);

            // Act
            var temperature = provider.GetTemperature(City.Brno);

            // Assert
            mockCache.Verify();
            Assert.AreEqual(30, temperature);
        }
    }
}