import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface ShoppingItem {
  id: string;
  name: string;
  quantity: number;
  createdByUserId: string;
  createdByUserName: string;
  createdAt: string;
  updatedAt: string;
}

interface ShoppingState {
  items: ShoppingItem[];
  loading: boolean;
  error: string | null;
}

const initialState: ShoppingState = {
  items: [],
  loading: false,
  error: null,
};

const shoppingSlice = createSlice({
  name: 'shopping',
  initialState,
  reducers: {
    setShoppingItems: (state, action: PayloadAction<ShoppingItem[]>) => {
      state.items = action.payload;
      state.loading = false;
      state.error = null;
    },
    addShoppingItem: (state, action: PayloadAction<ShoppingItem>) => {
      state.items.push(action.payload);
    },
    updateShoppingItem: (state, action: PayloadAction<ShoppingItem>) => {
      const index = state.items.findIndex(i => i.id === action.payload.id);
      if (index !== -1) {
        state.items[index] = action.payload;
      }
    },
    removeShoppingItem: (state, action: PayloadAction<string>) => {
      state.items = state.items.filter(i => i.id !== action.payload);
    },
    clearShoppingItems: (state) => {
      state.items = [];
    },
    setLoading: (state, action: PayloadAction<boolean>) => {
      state.loading = action.payload;
    },
    setError: (state, action: PayloadAction<string | null>) => {
      state.error = action.payload;
      state.loading = false;
    },
  },
});

export const {
  setShoppingItems,
  addShoppingItem,
  updateShoppingItem,
  removeShoppingItem,
  clearShoppingItems,
  setLoading,
  setError,
} = shoppingSlice.actions;

export default shoppingSlice.reducer;
