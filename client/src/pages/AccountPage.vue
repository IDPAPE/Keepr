<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';

const account = computed(() => AppState.account)
const myVaults = computed(() => AppState.myVaults)
const myKeeps = computed(() => AppState.activeKeeps)

async function getMyKeeps() {
  try {
    keepsService.getProfileKeeps(account.value.id)
  }
  catch (error) {
    Pop.error(error);
    console.error(error)
  }
}

onMounted(() => {
  getMyKeeps()
})
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
        <div class="col-12 mb-3">
          <h2 class="fs-1">Vaults</h2>
          <hr />
        </div>
        <div v-for="vault in myVaults" :key="vault.id" class="col-md-3 col-6">
          <VaultCard :vault="vault" />
        </div>

      </section>

      <section v-if="myKeeps" class="row">
        <div class="col-12 mb-3">
          <h2 class="fs-1">Keeps</h2>
          <hr />
        </div>
        <div class="col-12 masonry p-0">
          <div v-for="keep in myKeeps" :key="keep.id">
            <KeepCard class="card-margin drop-shadow" :keep="keep" />
          </div>
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

@media screen and (max-width: 992px) {
  .masonry {
    columns: 2;
    column-gap: 1rem;
  }

  .card-margin {
    margin-bottom: 1rem;
  }
}

//classes for large screens
@media screen and (min-width: 992px) {
  .masonry {
    columns: 4;
    column-gap: 2rem;
  }

  .card-margin {
    margin-bottom: 2rem;
  }
}
</style>
