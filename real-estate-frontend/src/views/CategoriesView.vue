<template>
  <div class="page">
    <header>
      <h1>Property Categories</h1>
      <button @click="handleLogout">Log out</button>
    </header>

    <section v-if="loading" class="state">Loading categories…</section>
    <section v-else-if="error" class="state error">{{ error }}</section>

    <ul v-else class="categories">
      <li v-for="category in categories" :key="category.categoryId">
        <article>
          <img v-if="category.imageUrl" :src="category.imageUrl" :alt="category.name" />
          <div>
            <h2>{{ category.name }}</h2>
            <p v-if="category.description">{{ category.description }}</p>
          </div>
        </article>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { onMounted, computed } from 'vue';
import { useRouter } from 'vue-router';
import { storeToRefs } from 'pinia';
import { useCategoriesStore } from '../stores/categories';
import { useAuthStore } from '../stores/auth';

const router = useRouter();
const authStore = useAuthStore();
const categoriesStore = useCategoriesStore();
const { items, loading, error } = storeToRefs(categoriesStore);
const categories = computed(() => items.value ?? []);

const loadCategories = async () => {
  try {
    await categoriesStore.load();
  } catch (err) {
    if (err.message?.includes('Session expired')) {
      authStore.logout();
      categoriesStore.reset();
      router.replace({ name: 'login', query: { redirect: '/categories' } });
    }
  }
};

const handleLogout = () => {
  authStore.logout();
  categoriesStore.reset();
  router.push({ name: 'login' });
};

onMounted(loadCategories);
</script>

<style scoped>
.page {
  min-height: 100vh;
  background: #f8fafc;
  padding: 2rem clamp(1rem, 4vw, 3rem);
}

header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 2rem;
}

h1 {
  font-size: clamp(1.75rem, 2.5vw, 2.25rem);
  color: #0f172a;
}

button {
  background: #1d4ed8;
  color: white;
  border: none;
  padding: 0.75rem 1.5rem;
  border-radius: 999px;
  cursor: pointer;
  font-weight: 600;
  transition: background 0.2s ease;
}

button:hover {
  background: #1e40af;
}

button:focus {
  outline: none;
  box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.35);
}

.state {
  text-align: center;
  color: #475569;
  font-size: 1.1rem;
}

.state.error {
  color: #b91c1c;
}

.categories {
  list-style: none;
  display: grid;
  gap: 1.5rem;
  padding: 0;
  margin: 0;
}

li article {
  display: flex;
  gap: 1.5rem;
  padding: 1.25rem;
  border-radius: 16px;
  background: white;
  box-shadow: 0 10px 25px rgba(15, 23, 42, 0.08);
  align-items: center;
}

img {
  width: 96px;
  height: 96px;
  object-fit: cover;
  border-radius: 12px;
}

h2 {
  margin: 0;
  font-size: 1.25rem;
  color: #0f172a;
}

p {
  margin-top: 0.35rem;
  color: #475569;
}
</style>
