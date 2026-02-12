import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '../../store/store';
import { setWeatherForecast, setLoading, setError } from '../../store/slices/weatherSlice';
import { weatherService } from '../../services/weatherService';
import WeatherCard from './WeatherCard';

const WeatherTab: React.FC = () => {
  const dispatch = useDispatch();
  const { forecast, loading, error } = useSelector((state: RootState) => state.weather);

  useEffect(() => {
    loadWeather();
    
    // Auto-refresh every hour
    const interval = setInterval(loadWeather, 3600000);
    return () => clearInterval(interval);
  }, []);

  const loadWeather = async () => {
    try {
      dispatch(setLoading(true));
      const data = await weatherService.getForecast();
      dispatch(setWeatherForecast(data));
    } catch (err: any) {
      dispatch(setError(err.message || 'Failed to load weather'));
    }
  };

  if (loading && forecast.length === 0) {
    return (
      <div className="flex justify-center items-center h-64">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="bg-red-50 border border-red-200 rounded-lg p-4 text-red-700">
        <p className="font-semibold">Error loading weather</p>
        <p className="text-sm">{error}</p>
        <button
          onClick={loadWeather}
          className="mt-2 px-4 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700 transition"
        >
          Retry
        </button>
      </div>
    );
  }

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <div>
          <h2 className="text-2xl font-bold text-gray-900">Weather Forecast</h2>
          <p className="text-gray-600">7-day forecast for Poznan, Poland</p>
        </div>
        <button
          onClick={loadWeather}
          className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition flex items-center space-x-2"
        >
          <span>🔄</span>
          <span>Refresh</span>
        </button>
      </div>

      <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-7 gap-4">
        {forecast.map((day, index) => (
          <WeatherCard key={index} forecast={day} />
        ))}
      </div>
    </div>
  );
};

export default WeatherTab;
