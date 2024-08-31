namespace Common.Models
{
    public class WeatherForecast
    {
        public string? City { get; set; }
        public DateOnly Date { get; set; }
        public Temperature? Temperature { get; set; }
        public WeatherSummary? Summary { get; set; }
    }
}
