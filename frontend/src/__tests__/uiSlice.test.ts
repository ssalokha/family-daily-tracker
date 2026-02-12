import { describe, it, expect, beforeEach } from 'vitest';
import { configureStore, EnhancedStore } from '@reduxjs/toolkit';
import uiReducer, { setActiveTab, updateInteraction, resumeCarousel, UIState } from '../store/slices/uiSlice';

interface RootState {
  ui: UIState;
}

describe('uiSlice', () => {
  let store: EnhancedStore<RootState>;

  beforeEach(() => {
    store = configureStore({
      reducer: {
        ui: uiReducer,
      },
    });
  });

  it('should have initial state with carousel playing', () => {
    const state = store.getState().ui;
    expect(state.activeTab).toBe(0);
    expect(state.carouselPlaying).toBe(true);
    expect(state.lastInteraction).toBeGreaterThan(0);
  });

  it('should change active tab', () => {
    store.dispatch(setActiveTab(1));
    expect(store.getState().ui.activeTab).toBe(1);

    store.dispatch(setActiveTab(2));
    expect(store.getState().ui.activeTab).toBe(2);
  });

  it('should stop carousel on user interaction', () => {
    const beforeInteraction = store.getState().ui.lastInteraction;
    
    // Wait a tiny bit
    setTimeout(() => {
      store.dispatch(updateInteraction());
      const afterInteraction = store.getState().ui.lastInteraction;
      
      expect(store.getState().ui.carouselPlaying).toBe(false);
      expect(afterInteraction).toBeGreaterThan(beforeInteraction);
    }, 10);
  });

  it('should resume carousel', () => {
    // Stop carousel first
    store.dispatch(updateInteraction());
    expect(store.getState().ui.carouselPlaying).toBe(false);

    // Resume carousel
    store.dispatch(resumeCarousel());
    expect(store.getState().ui.carouselPlaying).toBe(true);
  });
});
