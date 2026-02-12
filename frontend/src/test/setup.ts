import { expect, afterEach, vi } from 'vitest';
import { cleanup } from '@testing-library/react';
import * as matchers from '@testing-library/jest-dom/matchers';

// Extend Vitest's expect with jest-dom matchers
expect.extend(matchers);

// Cleanup after each test
afterEach(() => {
  cleanup();
});

// Mock localStorage
const localStorageMock: Storage & { store: Record<string, string> } = {
  getItem: vi.fn((key: string): string | null => {
    return localStorageMock.store[key] || null;
  }),
  setItem: vi.fn((key: string, value: string): void => {
    localStorageMock.store[key] = value;
  }),
  removeItem: vi.fn((key: string): void => {
    delete localStorageMock.store[key];
  }),
  clear: vi.fn((): void => {
    localStorageMock.store = {};
  }),
  key: vi.fn((index: number): string | null => {
    const keys = Object.keys(localStorageMock.store);
    return keys[index] || null;
  }),
  get length(): number {
    return Object.keys(localStorageMock.store).length;
  },
  store: {} as Record<string, string>,
};

global.localStorage = localStorageMock as any;
