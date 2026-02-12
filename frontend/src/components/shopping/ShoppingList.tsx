import React, { useState } from 'react';
import { ShoppingItem } from '../../store/slices/shoppingSlice';
import { shoppingService } from '../../services/shoppingService';

interface ShoppingListProps {
  items: ShoppingItem[];
  onRefresh: () => void;
}

const ShoppingList: React.FC<ShoppingListProps> = ({ items, onRefresh }) => {
  const [editingId, setEditingId] = useState<string | null>(null);
  const [editForm, setEditForm] = useState({ name: '', quantity: 1 });
  const [deletingId, setDeletingId] = useState<string | null>(null);

  const handleEdit = (item: ShoppingItem) => {
    setEditingId(item.id);
    setEditForm({ name: item.name, quantity: item.quantity });
  };

  const handleSaveEdit = async (id: string) => {
    try {
      await shoppingService.update({ id, ...editForm });
      setEditingId(null);
      onRefresh();
    } catch (err) {
      alert('Failed to update item');
    }
  };

  const handleDelete = async (id: string) => {
    try {
      setDeletingId(id);
      await shoppingService.delete(id);
      onRefresh();
    } catch (err) {
      alert('Failed to delete item');
    } finally {
      setDeletingId(null);
    }
  };

  return (
    <div className="bg-white rounded-lg shadow-md overflow-hidden">
      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Item
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Quantity
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Added By
              </th>
              <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                Actions
              </th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {items.map((item) => (
              <tr key={item.id} className="hover:bg-gray-50">
                {editingId === item.id ? (
                  <>
                    <td className="px-6 py-4">
                      <input
                        type="text"
                        value={editForm.name}
                        onChange={(e) => setEditForm({ ...editForm, name: e.target.value })}
                        className="w-full px-2 py-1 border rounded"
                      />
                    </td>
                    <td className="px-6 py-4">
                      <input
                        type="number"
                        min="1"
                        value={editForm.quantity}
                        onChange={(e) => setEditForm({ ...editForm, quantity: parseInt(e.target.value) || 1 })}
                        className="w-20 px-2 py-1 border rounded"
                      />
                    </td>
                    <td className="px-6 py-4 text-sm text-gray-600">
                      {item.createdByUserName}
                    </td>
                    <td className="px-6 py-4 text-right space-x-2">
                      <button
                        onClick={() => handleSaveEdit(item.id)}
                        className="text-green-600 hover:text-green-700 font-medium"
                      >
                        Save
                      </button>
                      <button
                        onClick={() => setEditingId(null)}
                        className="text-gray-600 hover:text-gray-700 font-medium"
                      >
                        Cancel
                      </button>
                    </td>
                  </>
                ) : (
                  <>
                    <td className="px-6 py-4 text-sm font-medium text-gray-900">
                      {item.name}
                    </td>
                    <td className="px-6 py-4 text-sm text-gray-600">
                      x{item.quantity}
                    </td>
                    <td className="px-6 py-4 text-sm text-gray-600">
                      {item.createdByUserName}
                    </td>
                    <td className="px-6 py-4 text-right space-x-2">
                      <button
                        onClick={() => handleEdit(item)}
                        className="text-blue-600 hover:text-blue-700 font-medium"
                      >
                        Edit
                      </button>
                      <button
                        onClick={() => handleDelete(item.id)}
                        disabled={deletingId === item.id}
                        className="text-red-600 hover:text-red-700 font-medium disabled:opacity-50"
                      >
                        {deletingId === item.id ? '...' : 'Delete'}
                      </button>
                    </td>
                  </>
                )}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ShoppingList;
