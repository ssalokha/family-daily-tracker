import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface UIState {
  activeTab: number;
  carouselPlaying: boolean;
  lastInteraction: number;
}

const initialState: UIState = {
  activeTab: 0,
  carouselPlaying: true,
  lastInteraction: Date.now(),
};

const uiSlice = createSlice({
  name: 'ui',
  initialState,
  reducers: {
    setActiveTab: (state, action: PayloadAction<number>) => {
      state.activeTab = action.payload;
      state.lastInteraction = Date.now();
    },
    setCarouselPlaying: (state, action: PayloadAction<boolean>) => {
      state.carouselPlaying = action.payload;
    },
    updateInteraction: (state) => {
      state.lastInteraction = Date.now();
      state.carouselPlaying = false;
    },
    resumeCarousel: (state) => {
      state.carouselPlaying = true;
    },
  },
});

export const { setActiveTab, setCarouselPlaying, updateInteraction, resumeCarousel } = uiSlice.actions;
export default uiSlice.reducer;
