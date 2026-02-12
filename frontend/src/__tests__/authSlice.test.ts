import { describe, it, expect } from 'vitest';
import authReducer, { setCredentials, logout, restoreAuth } from '../store/slices/authSlice';
import type { User } from '../store/slices/authSlice';

export interface AuthState {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
}

describe('authSlice', () => {
  const initialState: AuthState = {
    user: null,
    token: null,
    isAuthenticated: false,
  };

  it('should return the initial state', () => {
    expect(authReducer(undefined, { type: 'unknown' })).toEqual(initialState);
  });

  it('should handle setCredentials', () => {
    const user: User = {
      id: '123',
      userName: 'TestUser',
      email: 'test@test.com',
      birthDate: '1990-01-01',
      userAge: 34,
      role: 0,
    };
    const token = 'test-token';

    const actual = authReducer(initialState, setCredentials({ user, token }));
    
    expect(actual.user).toEqual(user);
    expect(actual.token).toEqual(token);
    expect(actual.isAuthenticated).toBe(true);
  });

  it('should handle logout', () => {
    const loggedInState: AuthState = {
      user: {
        id: '123',
        userName: 'TestUser',
        email: 'test@test.com',
        birthDate: '1990-01-01',
        userAge: 34,
        role: 0,
      },
      token: 'test-token',
      isAuthenticated: true,
    };

    const actual = authReducer(loggedInState, logout());
    
    expect(actual.user).toBeNull();
    expect(actual.token).toBeNull();
    expect(actual.isAuthenticated).toBe(false);
  });

  it('should handle restoreAuth when data exists in localStorage', () => {
    const user: User = {
      id: '123',
      userName: 'TestUser',
      email: 'test@test.com',
      birthDate: '1990-01-01',
      userAge: 34,
      role: 0,
    };
    const token = 'stored-token';

    // Mock localStorage
    global.localStorage.setItem('user', JSON.stringify(user));
    global.localStorage.setItem('token', token);

    const actual = authReducer(initialState, restoreAuth());
    
    expect(actual.user).toEqual(user);
    expect(actual.token).toEqual(token);
    expect(actual.isAuthenticated).toBe(true);

    // Cleanup
    global.localStorage.clear();
  });

  it('should not restore auth when localStorage is empty', () => {
    global.localStorage.clear();

    const actual = authReducer(initialState, restoreAuth());
    
    expect(actual.user).toBeNull();
    expect(actual.token).toBeNull();
    expect(actual.isAuthenticated).toBe(false);
  });
});
