import React from 'react';
import { WeatherForecast } from '../../store/slices/weatherSlice';
import { format } from 'date-fns';

interface WeatherCardProps {
  forecast: WeatherForecast;
}

const WeatherCard: React.FC<WeatherCardProps> = ({ forecast }) => {
  const getWeatherIcon = (condition: string) => {
    const lower = condition.toLowerCase();
    if (lower.includes('sun') || lower.includes('clear')) return '☀️';
    if (lower.includes('cloud')) return '☁️';
    if (lower.includes('rain')) return '🌧️';
    if (lower.includes('snow')) return '❄️';
    if (lower.includes('thunder') || lower.includes('storm')) return '⛈️';
    return '🌤️';
  };

  return (
    <div className="bg-white rounded-lg shadow-md p-4 hover:shadow-lg transition-shadow">
      <div className="text-center">
        <p className="text-sm font-semibold text-gray-600 mb-2">
          {format(new Date(forecast.date), 'EEE, MMM d')}
        </p>
        <div className="text-5xl my-3">
          {getWeatherIcon(forecast.condition)}
        </div>
        <p className="text-sm text-gray-700 mb-3 capitalize">
          {forecast.condition}
        </p>
        <div className="flex justify-center items-center space-x-2 mb-3">
          <span className="text-2xl font-bold text-orange-500">
            {Math.round(forecast.temperatureMax)}°
          </span>
          <span className="text-lg text-gray-500">
            {Math.round(forecast.temperatureMin)}°
          </span>
        </div>
        <div className="text-xs text-gray-500 space-y-1">
          <p>💧 {forecast.humidity}%</p>
          <p>💨 {Math.round(forecast.windSpeed)} km/h</p>
        </div>
      </div>
    </div>
  );
};

export default WeatherCard;
