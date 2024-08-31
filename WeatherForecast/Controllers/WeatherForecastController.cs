using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly string _temperatureServiceBaseUrl = "http://temperatureservice/Temperature";
        private readonly string _weatherSummaryServiceBaseUrl = "http://weathersummaryservice/WeatherSummary";

        public WeatherForecastController(
            IHttpClientFactory httpClientFactory,
            ILogger<WeatherForecastController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet("{city}", Name = "GetWeatherForecast")]
        public async Task<Common.Models.WeatherForecast> GetAsync(string city)
        {
            _logger.LogInformation("Fetching temperature data");
            var temperature = await GetTemperatureAsync(city);

            _logger.LogInformation("Fetching weather summary data");
            var weatherSummary = await GetSymmaryAsync(city);

            return new Common.Models.WeatherForecast
            {
                City = city,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Temperature = temperature,
                Summary = weatherSummary
            };
        }

        private async Task<Temperature?> GetTemperatureAsync(string city)
        {

            var temperatureClient = _httpClientFactory.CreateClient();
            var teamperatureResponse = await temperatureClient.GetAsync($"{_temperatureServiceBaseUrl}/{city}");
            try
            {
                return await teamperatureResponse.Content.ReadFromJsonAsync<Temperature>();
            }
            catch (JsonException)
            {
                return null;
            }
        }

        private async Task<WeatherSummary?> GetSymmaryAsync(string city)
        {
            var weatherSummaryClient = _httpClientFactory.CreateClient();
            var weatherSummaryResponse = await weatherSummaryClient.GetAsync($"{_weatherSummaryServiceBaseUrl}/{city}");
            try
            {
                return await weatherSummaryResponse.Content.ReadFromJsonAsync<WeatherSummary>();
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}
