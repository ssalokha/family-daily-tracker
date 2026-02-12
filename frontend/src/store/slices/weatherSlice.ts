import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface WeatherForecast {
  date: string;
  temperatureMin: number;
  temperatureMax: number;
  condition: string;
  icon: string;
  humidity: number;
  windSpeed: number;
}

interface WeatherState {
  forecast: WeatherForecast[];
  loading: boolean;
  error: string | null;
  lastUpdated: string | null;
}

const initialState: WeatherState = {
  forecast: [],
  loading: false,
  error: null,
  lastUpdated: null,
};

const weatherSlice = createSlice({
  name: 'weather',
  initialState,
  reducers: {
    setWeatherForecast: (state, action: PayloadAction<WeatherForecast[]>) => {
      state.forecast = action.payload;
      state.loading = false;
      state.error = null;
      state.lastUpdated = new Date().toISOString();
    },
    setLoading: (state, action: PayloadAction<boolean>) => {
      state.loading = action.payload;
    },
    setError: (state, action: PayloadAction<string | null>) => {
      state.error = action.payload;
      state.loading = false;
    },
  },
});

export const { setWeatherForecast, setLoading, setError } = weatherSlice.actions;
export default weatherSlice.reducer;
