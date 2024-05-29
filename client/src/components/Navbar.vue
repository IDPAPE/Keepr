<script setup>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from '../AppState.js';

const theme = ref(loadState('theme') || 'light')
const account = computed(() => AppState.account)

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

</script>

<template>
  <nav class="navbar navbar-expand-sm navbar-light bg-color px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <h2 class="fs-1"><i class="mdi mdi-camera-iris"></i> Keepr</h2>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <p v-if="account" class="text-light mb-0 me-4 selectable" data-bs-toggle="modal"
            data-bs-target="#createKeepModal"><i class="mdi mdi-camera" title="keep"></i> Create
            Keep</p>
        </li>
        <li>
          <p v-if="account" class="text-light mb-0 selectable" data-bs-toggle="modal"
            data-bs-target="#createVaultModal"><i class="mdi mdi-safe" title="vault"></i> Create Vault
          </p>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}

.bg-color {
  background-color: #7D938A;
}
</style>
