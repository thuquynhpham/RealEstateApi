<template>
  <div class="auth-layout">
    <section class="card">
      <h1>RealEstate Admin Login</h1>
      <form @submit.prevent="handleSubmit">
        <label>
          Email
          <input
            v-model="form.email"
            type="email"
            required
            autocomplete="email"
            placeholder="you@example.com"
          />
        </label>
        <label>
          Password
          <input
            v-model="form.password"
            type="password"
            required
            autocomplete="current-password"
            placeholder="••••••••"
          />
        </label>
        <button type="submit" :disabled="loading">
          <span v-if="loading">Signing in...</span>
          <span v-else>Sign In</span>
        </button>
      </form>
      <p v-if="error" class="error">{{ error }}</p>
    </section>
  </div>
</template>

<script setup>
import { reactive } from 'vue';
import { storeToRefs } from 'pinia';
import { useRouter, useRoute } from 'vue-router';
import { useAuthStore } from '../stores/auth';

const router = useRouter();
const route = useRoute();
const authStore = useAuthStore();
const { loading, error } = storeToRefs(authStore);

const form = reactive({
  email: '',
  password: '',
});

const handleSubmit = async () => {
  const success = await authStore.login({ ...form });
  if (success) {
    const redirectTarget = route.query.redirect;
    if (typeof redirectTarget === 'string') {
      router.push(redirectTarget);
    } else {
      router.push({ name: 'categories' });
    }
  }
};
</script>

<style scoped>
.auth-layout {
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
  background: linear-gradient(135deg, #1f2937, #111827);
  padding: 2rem;
}

.card {
  width: min(400px, 100%);
  background: white;
  border-radius: 16px;
  padding: 2rem;
  box-shadow: 0 20px 45px rgba(15, 23, 42, 0.2);
}

h1 {
  margin-bottom: 1.5rem;
  text-align: center;
  color: #111827;
}

form {
  display: grid;
  gap: 1rem;
}

label {
  display: grid;
  gap: 0.5rem;
  font-weight: 600;
  color: #374151;
}

input {
  border: 1px solid #d1d5db;
  border-radius: 8px;
  padding: 0.75rem;
  font-size: 1rem;
  transition: border-color 0.2s ease;
}

input:focus {
  outline: none;
  border-color: #2563eb;
  box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.2);
}

button {
  margin-top: 1rem;
  background: #2563eb;
  color: white;
  font-weight: 600;
  border: none;
  border-radius: 8px;
  padding: 0.75rem;
  cursor: pointer;
  transition: background 0.2s ease;
}

button:disabled {
  background: #93c5fd;
  cursor: not-allowed;
}

.error {
  margin-top: 1rem;
  color: #b91c1c;
  text-align: center;
}
</style>
