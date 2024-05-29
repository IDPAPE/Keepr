<script setup>
import { onMounted } from 'vue';
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import { vaultsService } from './services/VaultsService.js';
import Pop from './utils/Pop.js';

async function getMyVaults() {
  try {
    const myVaults = await vaultsService.getMyVaults()
  }
  catch (error) {
    Pop.error(error);
  }
}

// onMounted(() => {
//   getMyVaults()
// })
</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <KeepModal />
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
