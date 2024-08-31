using System;

namespace Common.Models;

public class WeatherSummary
{
    public string? Description { get; set; }
    public double Precipitation { get; set; }
    public double Humidity { get; set; }
    public double Wind { get; set; }
}
