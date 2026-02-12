import api from './api';
import { Appointment } from '../store/slices/appointmentsSlice';

export interface CreateAppointmentRequest {
  createdByUserId: string;
  appointmentForUserId: string;
  appointmentDateTime: string;
  street: string;
  buildingNumber: string;
}

export interface UpdateAppointmentRequest {
  id: string;
  appointmentForUserId: string;
  appointmentDateTime: string;
  street: string;
  buildingNumber: string;
  isCompleted: boolean;
}

export const appointmentService = {
  getUpcoming: async (daysAhead: number = 14): Promise<Appointment[]> => {
    const response = await api.get<Appointment[]>(`/appointments/upcoming?daysAhead=${daysAhead}`);
    return response.data;
  },

  create: async (data: CreateAppointmentRequest): Promise<Appointment> => {
    const response = await api.post<Appointment>('/appointments', data);
    return response.data;
  },

  update: async (data: UpdateAppointmentRequest): Promise<Appointment> => {
    const response = await api.put<Appointment>(`/appointments/${data.id}`, data);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/appointments/${id}`);
  },
};
