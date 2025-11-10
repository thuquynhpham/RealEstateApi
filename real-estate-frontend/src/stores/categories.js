import { defineStore } from 'pinia';
import { fetchCategories } from '../services/categories';

export const useCategoriesStore = defineStore('categories', {
  state: () => ({
    items: [],
    loading: false,
    error: '',
    hasLoaded: false,
  }),
  actions: {
    async load(force = false) {
      if (this.loading) {
        return;
      }
      if (this.hasLoaded && !force) {
        return;
      }
      this.loading = true;
      this.error = '';
      try {
        const categories = await fetchCategories();
        this.items = categories;
        this.hasLoaded = true;
      } catch (err) {
        this.error = err.message || 'Unable to load categories.';
        this.items = [];
        throw err;
      } finally {
        this.loading = false;
      }
    },
    reset() {
      this.items = [];
      this.hasLoaded = false;
      this.error = '';
      this.loading = false;
    },
  },
});
