import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '../../store/store';
import { setAppointments, setLoading, setError } from '../../store/slices/appointmentsSlice';
import { appointmentService } from '../../services/appointmentService';
import AppointmentList from './AppointmentList';
import AppointmentForm from './AppointmentForm';

const AppointmentsTab: React.FC = () => {
  const dispatch = useDispatch();
  const { appointments, loading, error } = useSelector((state: RootState) => state.appointments);
  const [showForm, setShowForm] = useState(false);
  const [editingId, setEditingId] = useState<string | null>(null);

  useEffect(() => {
    loadAppointments();
  }, []);

  const loadAppointments = async () => {
    try {
      dispatch(setLoading(true));
      const data = await appointmentService.getUpcoming(14);
      dispatch(setAppointments(data));
    } catch (err: any) {
      dispatch(setError(err.message || 'Failed to load appointments'));
    }
  };

  const handleEdit = (id: string) => {
    setEditingId(id);
    setShowForm(true);
  };

  const handleFormClose = () => {
    setShowForm(false);
    setEditingId(null);
  };

  const handleFormSuccess = () => {
    setShowForm(false);
    setEditingId(null);
    loadAppointments();
  };

  if (loading && appointments.length === 0) {
    return (
      <div className="flex justify-center items-center h-64">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
      </div>
    );
  }

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <div>
          <h2 className="text-2xl font-bold text-gray-900">Doctor Appointments</h2>
          <p className="text-gray-600">Upcoming appointments for the next 2 weeks</p>
        </div>
        <button
          onClick={() => setShowForm(true)}
          className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition flex items-center space-x-2"
        >
          <span>+</span>
          <span>New Appointment</span>
        </button>
      </div>

      {error && (
        <div className="mb-4 p-3 bg-red-50 border border-red-200 rounded-lg text-red-700 text-sm">
          {error}
        </div>
      )}

      {appointments.length === 0 && !loading ? (
        <div className="text-center py-12 bg-white rounded-lg shadow">
          <p className="text-gray-500 text-lg">No upcoming appointments</p>
          <button
            onClick={() => setShowForm(true)}
            className="mt-4 px-6 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition"
          >
            Create First Appointment
          </button>
        </div>
      ) : (
        <AppointmentList
          appointments={appointments}
          onEdit={handleEdit}
          onRefresh={loadAppointments}
        />
      )}

      {showForm && (
        <AppointmentForm
          appointmentId={editingId}
          onClose={handleFormClose}
          onSuccess={handleFormSuccess}
        />
      )}
    </div>
  );
};

export default AppointmentsTab;
