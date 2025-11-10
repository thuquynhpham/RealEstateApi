const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'https://localhost:7064/api';
const TOKEN_KEY = 'realestate_token';

export const getToken = () => localStorage.getItem(TOKEN_KEY);

export const setToken = (token) => {
  if (token) {
    localStorage.setItem(TOKEN_KEY, token);
  } else {
    localStorage.removeItem(TOKEN_KEY);
  }
};

export const login = async ({ email, password }) => {
  const response = await fetch(`${API_BASE_URL}/users/login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ email, password }),
  });

  if (!response.ok) {
    throw new Error('Invalid email or password.');
  }

  const data = await response.json();
  const token = data?.jwtToken;
  if (!token) {
    throw new Error('Login response did not include a token.');
  }
  setToken(token);
  return token;
};

export const logout = () => {
  setToken(null);
};
