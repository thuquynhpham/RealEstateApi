import { getToken, logout } from './auth';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'https://localhost:7064/api';

export const fetchCategories = async () => {
  const token = getToken();
  if (!token) {
    throw new Error('Missing authentication token.');
  }

  const response = await fetch(`${API_BASE_URL}/categories/getAll`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (response.status === 401 || response.status === 403) {
    logout();
    throw new Error('Session expired. Please sign in again.');
  }

  if (!response.ok) {
    throw new Error('Failed to load categories.');
  }

  const data = await response.json();
  return data?.categories ?? [];
};
