import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '../../store/store';
import { setShoppingItems, clearShoppingItems, setLoading, setError } from '../../store/slices/shoppingSlice';
import { shoppingService } from '../../services/shoppingService';
import { userService } from '../../services/userService';
import ShoppingList from './ShoppingList';
import AddItemForm from './AddItemForm';
import { User } from '../../store/slices/authSlice';

const ShoppingTab: React.FC = () => {
  const dispatch = useDispatch();
  const { items, loading, error } = useSelector((state: RootState) => state.shopping);
  const [users, setUsers] = useState<User[]>([]);
  const [showEmailModal, setShowEmailModal] = useState(false);
  const [selectedUserId, setSelectedUserId] = useState('');
  const [sending, setSending] = useState(false);

  useEffect(() => {
    loadShoppingList();
    loadUsers();
  }, []);

  const loadShoppingList = async () => {
    try {
      dispatch(setLoading(true));
      const data = await shoppingService.getAll();
      dispatch(setShoppingItems(data));
    } catch (err: any) {
      dispatch(setError(err.message || 'Failed to load shopping list'));
    }
  };

  const loadUsers = async () => {
    try {
      const data = await userService.getAll();
      setUsers(data.filter(u => u.email));
    } catch (err) {
      console.error('Failed to load users');
    }
  };

  const handleClearAll = async () => {
    if (!confirm('Are you sure you want to clear all items?')) return;
    
    try {
      await shoppingService.clear();
      dispatch(clearShoppingItems());
    } catch (err) {
      alert('Failed to clear shopping list');
    }
  };

  const handleSendEmail = async () => {
    if (!selectedUserId) {
      alert('Please select a user');
      return;
    }

    try {
      setSending(true);
      await shoppingService.sendEmail(selectedUserId);
      alert('Shopping list sent successfully!');
      setShowEmailModal(false);
      setSelectedUserId('');
    } catch (err: any) {
      alert(err.response?.data?.message || 'Failed to send email');
    } finally {
      setSending(false);
    }
  };

  if (loading && items.length === 0) {
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
          <h2 className="text-2xl font-bold text-gray-900">Shopping List</h2>
          <p className="text-gray-600">Family shopping items</p>
        </div>
        <div className="flex space-x-2">
          {items.length > 0 && (
            <>
              <button
                onClick={() => setShowEmailModal(true)}
                className="px-4 py-2 bg-green-600 text-white rounded-lg hover:bg-green-700 transition"
              >
                📧 Go Shop
              </button>
              <button
                onClick={handleClearAll}
                className="px-4 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700 transition"
              >
                Clear All
              </button>
            </>
          )}
        </div>
      </div>

      {error && (
        <div className="mb-4 p-3 bg-red-50 border border-red-200 rounded-lg text-red-700 text-sm">
          {error}
        </div>
      )}

      <div className="space-y-6">
        <AddItemForm onSuccess={loadShoppingList} />
        
        {items.length === 0 && !loading ? (
          <div className="text-center py-12 bg-white rounded-lg shadow">
            <p className="text-gray-500 text-lg">Shopping list is empty</p>
            <p className="text-gray-400 mt-2">Add items using the form above</p>
          </div>
        ) : (
          <ShoppingList items={items} onRefresh={loadShoppingList} />
        )}
      </div>

      {/* Email Modal */}
      {showEmailModal && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4 z-50">
          <div className="bg-white rounded-lg max-w-md w-full p-6">
            <h2 className="text-2xl font-bold mb-4">Send Shopping List</h2>
            <p className="text-gray-600 mb-4">Select a user to send the shopping list via email</p>

            <select
              value={selectedUserId}
              onChange={(e) => setSelectedUserId(e.target.value)}
              className="w-full px-3 py-2 border rounded-lg focus:ring-2 focus:ring-blue-500 mb-4"
            >
              <option value="">Select a user</option>
              {users.map((user) => (
                <option key={user.id} value={user.id}>
                  {user.userName} ({user.email})
                </option>
              ))}
            </select>

            <div className="flex space-x-3">
              <button
                onClick={handleSendEmail}
                disabled={sending || !selectedUserId}
                className="flex-1 bg-green-600 text-white py-2 rounded-lg hover:bg-green-700 transition disabled:opacity-50"
              >
                {sending ? 'Sending...' : 'Send Email'}
              </button>
              <button
                onClick={() => {
                  setShowEmailModal(false);
                  setSelectedUserId('');
                }}
                className="flex-1 bg-gray-300 text-gray-700 py-2 rounded-lg hover:bg-gray-400 transition"
              >
                Cancel
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default ShoppingTab;
