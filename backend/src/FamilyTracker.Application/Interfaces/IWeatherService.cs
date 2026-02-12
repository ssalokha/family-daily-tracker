using FamilyTracker.Application.DTOs;

namespace FamilyTracker.Application.Interfaces;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecastDto>> GetWeeklyForecastAsync(string city, CancellationToken cancellationToken = default);
}
