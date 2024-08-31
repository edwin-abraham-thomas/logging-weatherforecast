using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace WeatherSummaryService.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherSummaryController : ControllerBase
{

    private readonly ILogger<WeatherSummaryController> _logger;

    public WeatherSummaryController(ILogger<WeatherSummaryController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{city}", Name = "GetWeatherForecast")]
    public WeatherSummary? Get(string city)
    {
        _logger.LogInformation("Finding weather summary data for {city}", city);
        if (Summaries.TryGetValue(city, out var summary))
        {
            _logger.LogInformation("Found weather summary data for {city}", city);
            return summary;
        }
        _logger.LogError("Could not find summary data for {city}", city);
        return null;
    }

    private readonly Dictionary<string, WeatherSummary> Summaries = new Dictionary<string, WeatherSummary>()
    {
        { "Springfield", new WeatherSummary { Description = "Sunny", Precipitation = 0.0, Humidity = 45.0, Wind = 10.5 } },
        { "Rivertown", new WeatherSummary { Description = "Cloudy", Precipitation = 1.2, Humidity = 60.0, Wind = 5.3 } },
        { "Lakeside", new WeatherSummary { Description = "Rainy", Precipitation = 12.5, Humidity = 85.0, Wind = 7.8 } },
        { "Hilltop", new WeatherSummary { Description = "Windy", Precipitation = 0.0, Humidity = 50.0, Wind = 20.0 } },
        { "Greenfield", new WeatherSummary { Description = "Snowy", Precipitation = 8.0, Humidity = 70.0, Wind = 15.0 } }
    };
}
