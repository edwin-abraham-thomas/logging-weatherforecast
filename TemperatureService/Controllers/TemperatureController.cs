using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace TemperatureService.Controllers;

[ApiController]
[Route("[controller]")]
public class TemperatureController : ControllerBase
{

    private readonly ILogger<TemperatureController> _logger;

    public TemperatureController(ILogger<TemperatureController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{city}", Name = "GetWeatherForecast")]
    public Temperature? Get(string city)
    {
        _logger.LogInformation("Finding temperature data for {city}", city);
        if (Temperatures.TryGetValue(city, out var temperature))
        {
            _logger.LogInformation("Found temperature data for {city}", city);
            return temperature;
        }

        _logger.LogError("Could not find temperature data for {city}", city);
        return null;
    }

    private readonly Dictionary<string, Temperature> Temperatures = new Dictionary<string, Temperature>()
    {
        { "Springfield", new Temperature { High = 30.5, Low = 15.2 } },
        { "Rivertown", new Temperature { High = 28.3, Low = 14.8 } },
        { "Lakeside", new Temperature { High = 25.0, Low = 10.5 } },
        { "Hilltop", new Temperature { High = 32.1, Low = 18.4 } },
        { "Greenfield", new Temperature { High = 29.7, Low = 16.3 } }
    };
}
