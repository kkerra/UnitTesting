using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingLib.Weather;

namespace UnitTesting.kerra
{
    public class WeatherForecastTest
    {
        private readonly Mock<IWeatherForecastSource> _mockWeatherForecastSource;
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastTest()
        {
            _mockWeatherForecastSource = new Mock<IWeatherForecastSource>();
            _weatherForecastService = new WeatherForecastService(_mockWeatherForecastSource.Object);
        }

        [Fact]
        public void GetWeatherForecast_ShouldReturnForecastWhenDateIsValid()
        {
            var date = new DateTime(2023, 10, 1);
            var expectedForecast = new WeatherForecast { TemperatureC = 20 }; // Пример ожидаемого значения
            _mockWeatherForecastSource.Setup(s => s.GetForecast(date)).Returns(expectedForecast);

            var result = _weatherForecastService.GetWeatherForecast(date);

            Assert.NotNull(result);
            Assert.Equal(expectedForecast, result);
            _mockWeatherForecastSource.Verify(s => s.GetForecast(date), Times.Once);
        }

        [Fact]
        public void GetWeatherForecast_ShouldReturnNullWhenNoForecastAvailable()
        {
            var date = new DateTime(2023, 10, 1);
            _mockWeatherForecastSource.Setup(s => s.GetForecast(date)).Returns((WeatherForecast)null);

            var result = _weatherForecastService.GetWeatherForecast(date);

            Assert.Null(result);
            _mockWeatherForecastSource.Verify(s => s.GetForecast(date), Times.Once);
        }
    }
}
