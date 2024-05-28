<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';

const account = computed(() => AppState.account)
const myVaults = computed(() => AppState.myVaults)

</script>

<template>
  <div class="about container">
    <div v-if="account">
      <section class="row">
        <img :src="account.coverImg" alt="">
      </section>
      <section class="row text-center mt-4">
        <div class="col">
          <img class="rounded" :src="account.picture" alt="" />
          <h1>{{ account.name }}</h1>
          <p>{{ myVaults.length }} Vaults | 0 Keeps</p>
          <button class="btn btn-success">Edit Account</button>
        </div>
      </section>
      <section class="row">
        <div class="col-12">
          <h2>Vaults</h2>
        </div>
        <div v-for="vault in myVaults" :key="vault.id" class="col-3">
          <VaultCard :vault="vault" />
        </div>
      </section>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}

.drop-shadow {
  filter: drop-shadow(3px 3px 6px rgb(135, 134, 134));
}

.text-shadow {
  text-shadow: 2px 2px 4px black;
}
</style>
