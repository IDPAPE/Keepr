<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';

const account = computed(() => AppState.account)
const myVaults = computed(() => AppState.myVaults)

</script>

<template>
  <div class="about container">
    <div v-if="account">
      <section class="row text-center parent">
        <img class="hero-img" :src="account.coverImg" alt="">
        <div class="child">
          <img class="pfp" :src="account.picture" alt="" />
          <h1>{{ account.name }}</h1>
          <p>{{ myVaults.length }} Vaults | 0 Keeps</p>
          <button class="btn btn-success" data-bs-toggle="modal" data-bs-target="#EditAccountModal">Edit
            Account</button>
        </div>
        <div class="col">
          <!-- <img class="pfp" :src="account.picture" alt="" />
          <h1>{{ account.name }}</h1>
          <p>{{ myVaults.length }} Vaults | 0 Keeps</p>
          <button class="btn btn-success" data-bs-toggle="modal" data-bs-target="#EditAccountModal">Edit
            Account</button> -->
        </div>
      </section>
      <section class="row">
        <div class="col-12">
          <h2>Vaults</h2>
        </div>
        <div v-for="vault in myVaults" :key="vault.id" class="col-md-3 col-6">
          <VaultCard :vault="vault" />
        </div>
      </section>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
  <EditAccountModal />
</template>

<style scoped lang="scss">
.parent {
  position: relative;
  margin-bottom: 15rem;
}

.child {
  position: absolute;
  top: 17rem;
  right: 0;
}

.pfp {
  height: 20dvh;
  aspect-ratio: 1/1;
  border-radius: 50%;
}

.drop-shadow {
  filter: drop-shadow(3px 3px 6px rgb(135, 134, 134));
}

.text-shadow {
  text-shadow: 2px 2px 4px black;
}

.hero-img {
  object-fit: cover;
  object-position: center;
  height: 40dvh;
  width: 100%;
}
</style>
