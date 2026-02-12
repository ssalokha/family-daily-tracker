import api from './api';
import { ShoppingItem } from '../store/slices/shoppingSlice';

export interface CreateShoppingItemRequest {
  name: string;
  quantity: number;
  createdByUserId: string;
}

export interface UpdateShoppingItemRequest {
  id: string;
  name: string;
  quantity: number;
}

export const shoppingService = {
  getAll: async (): Promise<ShoppingItem[]> => {
    const response = await api.get<ShoppingItem[]>('/shopping');
    return response.data;
  },

  create: async (data: CreateShoppingItemRequest): Promise<ShoppingItem> => {
    const response = await api.post<ShoppingItem>('/shopping', data);
    return response.data;
  },

  update: async (data: UpdateShoppingItemRequest): Promise<ShoppingItem> => {
    const response = await api.put<ShoppingItem>(`/shopping/${data.id}`, data);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/shopping/${id}`);
  },

  clear: async (): Promise<void> => {
    await api.post('/shopping/clear');
  },

  sendEmail: async (userId: string): Promise<void> => {
    await api.post('/shopping/send-email', { userId });
  },
};
