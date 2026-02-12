using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace FamilyTracker.Infrastructure.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IEnumerable<WeatherForecastDto>> GetWeeklyForecastAsync(string city, CancellationToken cancellationToken = default)
    {
        try
        {
            // For Poznan, Poland - use Open-Meteo API (free, no key required)
            // Coordinates for Poznan: 52.4064° N, 16.9252° E
            var latitude = 52.4064;
            var longitude = 16.9252;
            
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,weathercode,windspeed_10m_max&timezone=Europe/Warsaw&forecast_days=7";
            
            var response = await _httpClient.GetAsync(url, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Weather API request failed with status {StatusCode}. Returning mock data.", response.StatusCode);
                return GetMockWeatherData();
            }

            var data = await response.Content.ReadFromJsonAsync<OpenMeteoResponse>(cancellationToken: cancellationToken);
            
            if (data?.Daily == null)
            {
                _logger.LogWarning("Weather API returned invalid data. Returning mock data.");
                return GetMockWeatherData();
            }

            // Map Open-Meteo weather codes to conditions and icons
            var forecasts = new List<WeatherForecastDto>();
            for (int i = 0; i < Math.Min(7, data.Daily.Time.Count); i++)
            {
                var weatherCode = data.Daily.Weathercode[i];
                var (condition, icon) = MapWeatherCode(weatherCode);
                
                forecasts.Add(new WeatherForecastDto
                {
                    Date = DateTime.Parse(data.Daily.Time[i]),
                    TemperatureMin = (int)Math.Round(data.Daily.Temperature_2m_min[i]),
                    TemperatureMax = (int)Math.Round(data.Daily.Temperature_2m_max[i]),
                    Condition = condition,
                    Icon = icon,
                    Humidity = 60, // Open-Meteo free tier doesn't include humidity
                    WindSpeed = (int)Math.Round(data.Daily.Windspeed_10m_max[i])
                });
            }

            _logger.LogInformation("Successfully fetched weather data for Poznan");
            return forecasts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather data for {City}", city);
            return GetMockWeatherData();
        }
    }

    private (string condition, string icon) MapWeatherCode(int code)
    {
        // WMO Weather interpretation codes
        return code switch
        {
            0 => ("Clear sky", "01d"),
            1 or 2 => ("Partly cloudy", "02d"),
            3 => ("Overcast", "03d"),
            45 or 48 => ("Foggy", "50d"),
            51 or 53 or 55 => ("Drizzle", "09d"),
            61 or 63 or 65 => ("Rain", "10d"),
            71 or 73 or 75 => ("Snow", "13d"),
            77 => ("Snow grains", "13d"),
            80 or 81 or 82 => ("Rain showers", "09d"),
            85 or 86 => ("Snow showers", "13d"),
            95 => ("Thunderstorm", "11d"),
            96 or 99 => ("Thunderstorm with hail", "11d"),
            _ => ("Unknown", "01d")
        };
    }

    private IEnumerable<WeatherForecastDto> GetMockWeatherData()
    {
        // Use today's date as seed to ensure consistent mock data throughout the day
        var seed = DateTime.Today.Year * 10000 + DateTime.Today.Month * 100 + DateTime.Today.Day;
        var random = new Random(seed);
        
        var conditions = new[] { "Sunny", "Cloudy", "Rainy", "Partly Cloudy", "Windy", "Foggy" };
        var icons = new[] { "01d", "02d", "03d", "04d", "09d", "10d", "11d", "13d", "50d" };
        
        return Enumerable.Range(0, 7).Select(i =>
        {
            var tempMin = random.Next(-5, 15);
            var tempMax = random.Next(tempMin + 5, 30);
            var conditionIndex = random.Next(conditions.Length);
            
            return new WeatherForecastDto
            {
                Date = DateTime.Today.AddDays(i),
                TemperatureMin = tempMin,
                TemperatureMax = tempMax,
                Condition = conditions[conditionIndex],
                Icon = icons[random.Next(icons.Length)],
                Humidity = random.Next(40, 80),
                WindSpeed = random.Next(5, 20)
            };
        }).ToList();
    }

    // Open-Meteo API response models
    private class OpenMeteoResponse
    {
        public DailyData? Daily { get; set; }
    }

    private class DailyData
    {
        public List<string> Time { get; set; } = new();
        public List<double> Temperature_2m_max { get; set; } = new();
        public List<double> Temperature_2m_min { get; set; } = new();
        public List<int> Weathercode { get; set; } = new();
        public List<double> Windspeed_10m_max { get; set; } = new();
    }
}
