import React, { useState } from 'react';
import { Appointment } from '../../store/slices/appointmentsSlice';
import { appointmentService } from '../../services/appointmentService';
import { format } from 'date-fns';

interface AppointmentListProps {
  appointments: Appointment[];
  onEdit: (id: string) => void;
  onRefresh: () => void;
}

const AppointmentList: React.FC<AppointmentListProps> = ({ appointments, onEdit, onRefresh }) => {
  const [deletingId, setDeletingId] = useState<string | null>(null);

  const handleDelete = async (id: string) => {
    if (!confirm('Are you sure you want to delete this appointment?')) return;
    
    try {
      setDeletingId(id);
      await appointmentService.delete(id);
      onRefresh();
    } catch (err) {
      alert('Failed to delete appointment');
    } finally {
      setDeletingId(null);
    }
  };

  const handleToggleComplete = async (appointment: Appointment) => {
    try {
      const [street, buildingNumber] = appointment.location.split(' ');
      await appointmentService.update({
        id: appointment.id,
        appointmentForUserId: appointment.appointmentForUserId,
        appointmentDateTime: appointment.appointmentDateTime,
        street: street || '',
        buildingNumber: buildingNumber || '',
        isCompleted: !appointment.isCompleted,
      });
      onRefresh();
    } catch (err) {
      alert('Failed to update appointment');
    }
  };

  return (
    <div className="space-y-4">
      {appointments.map((appointment) => (
        <div
          key={appointment.id}
          className={`bg-white rounded-lg shadow-md p-4 ${
            appointment.isCompleted ? 'opacity-60' : ''
          }`}
        >
          <div className="flex items-start justify-between">
            <div className="flex-1">
              <div className="flex items-center space-x-2 mb-2">
                <h3 className="text-lg font-semibold text-gray-900">
                  Doc: {appointment.appointmentForUserName}
                </h3>
                {appointment.isCompleted && (
                  <span className="px-2 py-1 bg-green-100 text-green-700 text-xs font-medium rounded">
                    ✓ Completed
                  </span>
                )}
              </div>
              <div className="space-y-1 text-sm text-gray-600">
                <p className="flex items-center">
                  <span className="font-medium w-24">Date:</span>
                  {format(new Date(appointment.appointmentDateTime), 'PPP HH:mm')}
                </p>
                <p className="flex items-center">
                  <span className="font-medium w-24">Location:</span>
                  {appointment.location}
                </p>
                <p className="flex items-center">
                  <span className="font-medium w-24">Created by:</span>
                  {appointment.createdByUserName}
                </p>
              </div>
            </div>
            <div className="flex flex-col space-y-2 ml-4">
              <button
                onClick={() => handleToggleComplete(appointment)}
                className="px-3 py-1 text-sm bg-green-600 text-white rounded hover:bg-green-700 transition"
              >
                {appointment.isCompleted ? 'Undo' : 'Complete'}
              </button>
              <button
                onClick={() => onEdit(appointment.id)}
                className="px-3 py-1 text-sm bg-blue-600 text-white rounded hover:bg-blue-700 transition"
              >
                Edit
              </button>
              <button
                onClick={() => handleDelete(appointment.id)}
                disabled={deletingId === appointment.id}
                className="px-3 py-1 text-sm bg-red-600 text-white rounded hover:bg-red-700 transition disabled:opacity-50"
              >
                {deletingId === appointment.id ? '...' : 'Delete'}
              </button>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default AppointmentList;
