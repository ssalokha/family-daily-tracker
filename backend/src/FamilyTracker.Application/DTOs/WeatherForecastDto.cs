namespace FamilyTracker.Application.DTOs;

public class WeatherForecastDto
{
    public DateTime Date { get; set; }
    public double TemperatureMin { get; set; }
    public double TemperatureMax { get; set; }
    public string Condition { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int Humidity { get; set; }
    public double WindSpeed { get; set; }
}
