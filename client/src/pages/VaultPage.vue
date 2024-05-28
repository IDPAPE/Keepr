<script setup>
import { useRoute } from 'vue-router';
import { router } from '../router.js';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import { computed, onBeforeMount, onMounted, ref } from 'vue';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';


const route = useRoute()
const vault = computed(() => AppState.activeVault)
const account = computed(() => AppState.account)
// const imgUrl = `url(${vault.value.img})`
const keeps = computed(() => AppState.activeKeeps)

async function getActiveVault() {
    try {
        await vaultsService.getActiveVault(route.params.vaultId)
    }
    catch (error) {
        Pop.error(error);
        console.error(error)
    }
}

async function getVaultKeeps() {
    try {
        await keepsService.getVaultKeeps(route.params.vaultId)
    }
    catch (error) {
        Pop.error(error);
        console.error(error)
    }
}

onMounted(() => {
    getActiveVault()
    getVaultKeeps()
})
</script>


<template>
    <div v-if="vault" class="container">

        <div class="row justify-content-center">
            <div class="col-4 rounded card d-flex drop-shadow mt-4">
                <div class="row flex-grow-1"></div>
                <div class="row text-light text-shadow text-center">
                    <h1>{{ vault.name }}</h1>
                    <p class="mb-1">by {{ vault.creator.name }}</p>
                </div>
            </div>
        </div>
        <div class="row justify-content-center mt-2">
            <div class="col-4">
                <div class="row align-items-center">
                    <div class="col-4">
                    </div>
                    <div class="col-4">
                        <h5 class="rounded rounded-pill bg-primary-subtle p-2 text-center">Keeps: {{ keeps.length }}
                        </h5>
                    </div>
                    <div class="col-4 text-end">
                        <p v-if="account.id == vault.creatorId" role="button" title="Delete this Keep"
                            class="fs-2 text-danger mdi mdi-delete-circle-outline"></p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12 masonry p-0">
                <div v-for="keep in keeps" :key="keep.id">
                    <KeepCard class="card-margin drop-shadow" :keep="keep" />
                </div>
            </div>
        </div>


    </div>
</template>


<style lang="scss" scoped>
.card {
    height: 20dvh;
    background-image: v-bind(imgUrl);
    background-size: cover;
    background-position: center;
}

.drop-shadow {
    filter: drop-shadow(3px 3px 6px rgb(135, 134, 134));
}

.text-shadow {
    text-shadow: 2px 2px 4px black;
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