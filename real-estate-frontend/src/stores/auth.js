import { defineStore } from 'pinia';
import { login as apiLogin, logout as apiLogout, getToken } from '../services/auth';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: getToken(),
    loading: false,
    error: '',
  }),
  getters: {
    isAuthenticated: (state) => Boolean(state.token),
  },
  actions: {
    async login(credentials) {
      this.loading = true;
      this.error = '';
      try {
        const token = await apiLogin(credentials);
        this.token = token;
        return true;
      } catch (err) {
        this.error = err.message || 'Unable to sign in. Please try again.';
        this.token = null;
        return false;
      } finally {
        this.loading = false;
      }
    },
    logout() {
      apiLogout();
      this.token = null;
    },
    hydrate() {
      this.token = getToken();
    },
  },
});
