import api from './api';
import { User } from '../store/slices/authSlice';

export interface CreateUserRequest {
  userName: string;
  birthDate?: string | null;
  email?: string | null;
  password: string;
  role: number;
}

export interface UpdateUserRequest {
  id: string;
  userName: string;
  birthDate?: string | null;
  email?: string | null;
  password?: string | null;
  role: number;
}

export const userService = {
  getAll: async (): Promise<User[]> => {
    const response = await api.get<User[]>('/users');
    return response.data;
  },

  create: async (data: CreateUserRequest): Promise<User> => {
    const response = await api.post<User>('/users', data);
    return response.data;
  },

  update: async (data: UpdateUserRequest): Promise<User> => {
    const response = await api.put<User>(`/users/${data.id}`, data);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/users/${id}`);
  },
};
