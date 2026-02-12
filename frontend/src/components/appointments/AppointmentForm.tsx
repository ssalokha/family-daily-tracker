import React, { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import { RootState } from '../../store/store';
import { appointmentService } from '../../services/appointmentService';
import { userService } from '../../services/userService';
import { User } from '../../store/slices/authSlice';

interface AppointmentFormProps {
  appointmentId: string | null;
  onClose: () => void;
  onSuccess: () => void;
}

const AppointmentForm: React.FC<AppointmentFormProps> = ({ appointmentId, onClose, onSuccess }) => {
  const { user: currentUser } = useSelector((state: RootState) => state.auth);
  const { appointments } = useSelector((state: RootState) => state.appointments);
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(false);
  const [formData, setFormData] = useState({
    appointmentForUserId: '',
    appointmentDate: '',
    appointmentTime: '',
    street: '',
    buildingNumber: '',
  });

  useEffect(() => {
    loadUsers();
    if (appointmentId) {
      const appointment = appointments.find(a => a.id === appointmentId);
      if (appointment) {
        const [street, buildingNumber] = appointment.location.split(' ');
        
        // Convert UTC datetime to local for date and time inputs
        const utcDate = new Date(appointment.appointmentDateTime);
        const localDateTime = new Date(utcDate.getTime() - utcDate.getTimezoneOffset() * 60000)
          .toISOString();
        const appointmentDate = localDateTime.slice(0, 10); // YYYY-MM-DD
        const appointmentTime = localDateTime.slice(11, 16); // HH:mm
        
        setFormData({
          appointmentForUserId: appointment.appointmentForUserId,
          appointmentDate,
          appointmentTime,
          street: street || '',
          buildingNumber: buildingNumber || '',
        });
      }
    }
  }, [appointmentId]);

  const loadUsers = async () => {
    try {
      const data = await userService.getAll();
      setUsers(data);
    } catch (err) {
      console.error('Failed to load users');
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Form submitted!', formData);
    console.log('Current user:', currentUser);
    
    if (!currentUser) {
      alert('You must be logged in to create an appointment');
      return;
    }
    
    setLoading(true);

    try {
      if (appointmentId) {
        const appointment = appointments.find(a => a.id === appointmentId)!;
        
        // Combine date and time inputs to ISO 8601 format for backend
        const isoDateTime = `${formData.appointmentDate}T${formData.appointmentTime}:00.000Z`;
        
        const updateData = {
          id: appointmentId,
          appointmentForUserId: formData.appointmentForUserId,
          appointmentDateTime: isoDateTime,
          street: formData.street,
          buildingNumber: formData.buildingNumber,
          isCompleted: appointment.isCompleted,
        };
        console.log('Updating appointment:', updateData);
        await appointmentService.update(updateData);
      } else {
        // Combine date and time inputs to ISO 8601 format for backend
        const isoDateTime = `${formData.appointmentDate}T${formData.appointmentTime}:00.000Z`;
        
        const createData = {
          createdByUserId: currentUser.id,
          appointmentForUserId: formData.appointmentForUserId,
          appointmentDateTime: isoDateTime,
          street: formData.street,
          buildingNumber: formData.buildingNumber,
        };
        console.log('Creating appointment:', createData);
        await appointmentService.create(createData);
      }
      console.log('Success!');
      onSuccess();
    } catch (err: any) {
      console.error('Error saving appointment:', err);
      alert(err.response?.data?.message || 'Failed to save appointment');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4 z-50">
      <div className="bg-white rounded-lg max-w-md w-full p-6">
        <h2 className="text-2xl font-bold mb-4">
          {appointmentId ? 'Edit Appointment' : 'New Appointment'}
        </h2>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Appointment For
            </label>
            <select
              value={formData.appointmentForUserId}
              onChange={(e) => setFormData({ ...formData, appointmentForUserId: e.target.value })}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-gray-900 bg-white"
              required
            >
              <option value="" className="text-gray-900">Select a user</option>
              {users.map((user) => (
                <option key={user.id} value={user.id} className="text-gray-900">
                  {user.userName}
                </option>
              ))}
            </select>
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Date
              </label>
              <input
                type="date"
                value={formData.appointmentDate}
                onChange={(e) => setFormData({ ...formData, appointmentDate: e.target.value })}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-gray-900 bg-white"
                required
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Time (24h)
              </label>
              <input
                type="time"
                value={formData.appointmentTime}
                onChange={(e) => setFormData({ ...formData, appointmentTime: e.target.value })}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-gray-900 bg-white"
                required
              />
            </div>
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Street
            </label>
            <input
              type="text"
              value={formData.street}
              onChange={(e) => setFormData({ ...formData, street: e.target.value })}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-gray-900 bg-white"
              placeholder="e.g., Main Street"
              required
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Building Number
            </label>
            <input
              type="text"
              value={formData.buildingNumber}
              onChange={(e) => setFormData({ ...formData, buildingNumber: e.target.value })}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-gray-900 bg-white"
              placeholder="e.g., 123"
              required
            />
          </div>

          <div className="flex space-x-3 pt-4">
            <button
              type="submit"
              disabled={loading}
              className="flex-1 bg-blue-600 text-white py-2 rounded-lg hover:bg-blue-700 transition disabled:opacity-50"
            >
              {loading ? 'Saving...' : 'Save'}
            </button>
            <button
              type="button"
              onClick={onClose}
              className="flex-1 bg-gray-300 text-gray-700 py-2 rounded-lg hover:bg-gray-400 transition"
            >
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default AppointmentForm;
