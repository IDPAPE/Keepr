<script setup>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';
import { AppState } from '../AppState.js';
import KeepCard from '../components/KeepCard.vue';

const keeps = computed(() => AppState.keeps)

async function getAllKeeps() {
  try {
    await keepsService.getAllKeeps()
  }
  catch (error) {
    Pop.toast('could not get all keeps', 'error');
    console.error(error)
  }
}

onMounted(() => {
  getAllKeeps()
})

</script>

<template>
  <div class="container-fluid p-md-5 pt-4">
    <section class="row">
      <div class="col-12 masonry">
        <div v-for="keep in keeps" :key="keep.id">
          <KeepCard class="card-margin" :keep="keep" />
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss">
@media screen and (max-width: 992px) {
  .masonry {
    columns: 2;
    column-gap: 0rem;
  }

  .card-margin {
    margin-bottom: 1rem;
  }
}

@media screen and (min-width: 992px) {
  .masonry {
    columns: 4;
    column-gap: 1rem;
  }

  .card-margin {
    margin-bottom: 2rem;
  }

}
</style>
