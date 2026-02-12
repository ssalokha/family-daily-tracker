import api from './api';

export interface AppointmentHistoryFilters {
  userId?: string;
  startDate?: string;
  endDate?: string;
  isCompleted?: boolean;
}

export const appointmentHistoryService = {
  async getHistory(filters: AppointmentHistoryFilters = {}) {
    const params = new URLSearchParams();
    if (filters.userId) params.append('userId', filters.userId);
    if (filters.startDate) params.append('startDate', filters.startDate);
    if (filters.endDate) params.append('endDate', filters.endDate);
    if (filters.isCompleted !== undefined) params.append('isCompleted', filters.isCompleted.toString());

    const response = await api.get(`/appointments/history?${params.toString()}`);
    return response.data;
  },
};
