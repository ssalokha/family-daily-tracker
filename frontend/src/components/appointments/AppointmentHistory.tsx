import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { RootState } from '../../store/store';
import { appointmentHistoryService } from '../../services/appointmentHistoryService';
import { userService } from '../../services/userService';
import { format } from 'date-fns';
import { User } from '../../store/slices/authSlice';

interface Appointment {
  id: string;
  appointmentForUserName: string;
  appointmentDateTime: string;
  location: string;
  isCompleted: boolean;
  createdByUserName: string;
}

const AppointmentHistory: React.FC = () => {
  const { user } = useSelector((state: RootState) => state.auth);
  const [appointments, setAppointments] = useState<Appointment[]>([]);
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);
  const [filters, setFilters] = useState({
    userId: '',
    startDate: '',
    endDate: '',
    isCompleted: undefined as boolean | undefined,
  });

  useEffect(() => {
    if (user && user.userRole.includes('AdminUser')) {
      loadUsers();
      loadHistory();
    }
  }, [user]);

  const loadUsers = async () => {
    try {
      const data = await userService.getAll();
      setUsers(data);
    } catch (err) {
      console.error('Failed to load users');
    }
  };

  const loadHistory = async () => {
    try {
      setLoading(true);
      const data = await appointmentHistoryService.getHistory(filters);
      setAppointments(data);
    } catch (err) {
      console.error('Failed to load appointment history');
    } finally {
      setLoading(false);
    }
  };

  const handleFilterChange = (e: React.ChangeEvent<HTMLSelectElement | HTMLInputElement>) => {
    const { name, value } = e.target;
    setFilters((prev) => ({
      ...prev,
      [name]: value === '' ? undefined : value,
    }));
  };

  const applyFilters = () => {
    loadHistory();
  };

  const clearFilters = () => {
    setFilters({
      userId: '',
      startDate: '',
      endDate: '',
      isCompleted: undefined,
    });
    setTimeout(() => loadHistory(), 100);
  };

  if (!user || !user.userRole.includes('AdminUser')) {
    return (
      <div className="p-6 bg-white rounded-lg shadow">
        <p className="text-gray-600">Access denied. Admin privileges required.</p>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <div className="bg-white rounded-lg shadow p-6">
        <h2 className="text-2xl font-bold mb-4">Appointment History</h2>

        {/* Filters */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4 mb-6">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">User</label>
            <select
              name="userId"
              value={filters.userId}
              onChange={handleFilterChange}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-gray-900 bg-white"
            >
              <option value="">All Users</option>
              {users.map((u) => (
                <option key={u.id} value={u.id} className="text-gray-900">
                  {u.userName}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Start Date</label>
            <input
              type="date"
              name="startDate"
              value={filters.startDate}
              onChange={handleFilterChange}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-gray-900 bg-white"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">End Date</label>
            <input
              type="date"
              name="endDate"
              value={filters.endDate}
              onChange={handleFilterChange}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-gray-900 bg-white"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Status</label>
            <select
              name="isCompleted"
              value={filters.isCompleted === undefined ? '' : filters.isCompleted.toString()}
              onChange={(e) =>
                setFilters((prev) => ({
                  ...prev,
                  isCompleted: e.target.value === '' ? undefined : e.target.value === 'true',
                }))
              }
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-gray-900 bg-white"
            >
              <option value="">All</option>
              <option value="false">Pending</option>
              <option value="true">Completed</option>
            </select>
          </div>
        </div>

        <div className="flex gap-2 mb-6">
          <button
            onClick={applyFilters}
            className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition"
          >
            Apply Filters
          </button>
          <button
            onClick={clearFilters}
            className="px-4 py-2 bg-gray-300 text-gray-700 rounded-lg hover:bg-gray-400 transition"
          >
            Clear
          </button>
        </div>

        {/* Appointments List */}
        {loading ? (
          <div className="text-center py-8">
            <div className="inline-block h-8 w-8 animate-spin rounded-full border-4 border-solid border-blue-600 border-r-transparent"></div>
            <p className="mt-2 text-gray-600">Loading history...</p>
          </div>
        ) : appointments.length === 0 ? (
          <div className="text-center py-8">
            <p className="text-gray-600">No appointments found with current filters.</p>
          </div>
        ) : (
          <div className="space-y-3">
            {appointments.map((appointment) => (
              <div
                key={appointment.id}
                className={`p-4 border rounded-lg ${
                  appointment.isCompleted ? 'bg-green-50 border-green-200' : 'bg-white border-gray-200'
                }`}
              >
                <div className="flex justify-between items-start">
                  <div>
                    <p className="font-semibold text-gray-900">
                      Doc: {appointment.appointmentForUserName}
                    </p>
                    <p className="text-sm text-gray-600">
                      📅 {format(new Date(appointment.appointmentDateTime), 'PPP HH:mm')}
                    </p>
                    <p className="text-sm text-gray-600">📍 {appointment.location}</p>
                    <p className="text-xs text-gray-500 mt-1">
                      Created by: {appointment.createdByUserName}
                    </p>
                  </div>
                  <div>
                    {appointment.isCompleted ? (
                      <span className="inline-flex items-center px-3 py-1 rounded-full text-sm font-medium bg-green-100 text-green-800">
                        ✓ Completed
                      </span>
                    ) : (
                      <span className="inline-flex items-center px-3 py-1 rounded-full text-sm font-medium bg-yellow-100 text-yellow-800">
                        ⏳ Pending
                      </span>
                    )}
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}

        <div className="mt-4 text-sm text-gray-600">
          Total: {appointments.length} appointment(s)
        </div>
      </div>
    </div>
  );
};

export default AppointmentHistory;
