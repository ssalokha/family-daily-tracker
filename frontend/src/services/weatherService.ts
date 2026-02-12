import api from './api';
import { WeatherForecast } from '../store/slices/weatherSlice';

export const weatherService = {
  getForecast: async (city: string = 'Poznan,PL'): Promise<WeatherForecast[]> => {
    const response = await api.get<WeatherForecast[]>(`/weather?city=${encodeURIComponent(city)}`);
    return response.data;
  },
};
