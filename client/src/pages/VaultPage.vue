<script setup>
import { useRoute } from 'vue-router';
import { router } from '../router.js';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import { computed, onBeforeMount, onMounted, onUnmounted, ref } from 'vue';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';


const route = useRoute()
const vault = computed(() => AppState.activeVault)
const account = computed(() => AppState.account)
const imgUrl = computed(() => `url(${vault.value?.img})`)
const keeps = computed(() => AppState.vaultKeeps)

async function getActiveVault() {
    try {
        await vaultsService.getActiveVault(route.params.vaultId)
    }
    catch (error) {
        if (error.response.data.includes('Could not find vault bad Id')) {
            router.push({ name: 'Home' })
        }
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

async function deleteVault(vaultId) {
    try {
        const confirmation = await Pop.confirm('are you sure you want to delete this vault?')
        if (confirmation == false) { return }
        await vaultsService.deleteVault(vaultId)
        router.push({ name: 'Profile', params: { profileId: AppState.account?.id } })
    }
    catch (error) {
        Pop.error(error);
        console.error(error)
    }
}

async function removeKeepFromVault(vaultKeepId) {
    try {
        const confirmation = await Pop.confirm('Remove From Vault?', "Are you sure you want to remove this keep from the vault?", "remove", "question")
        if (confirmation == false) { return }
        await keepsService.removeKeepFromVault(vaultKeepId)
    }
    catch (error) {
        Pop.error(error);
        console.error(error)
    }
}

onBeforeMount(() => {
    getActiveVault()
    getVaultKeeps()
})

</script>


<template>
    <div v-if="vault" class="container">
        <div class="row justify-content-center px-2 px-lg-0">
            <div class="col-md-4 rounded card d-flex drop-shadow mt-4">
                <div class="row flex-grow-1"></div>
                <div class="row text-light text-shadow text-center">
                    <h1>{{ vault.name }}</h1>
                    <p class="mb-1">by {{ vault.creator.name }}</p>
                </div>
            </div>
        </div>
        <div class="row justify-content-center mt-2">
            <div class="col-md-4 col-12">
                <div class="row align-items-center justify-content-center">
                    <div class="col-lg-4 col-2">
                        <p v-if="vault.isPrivate == true" class="mb-0 fs-3 text-warning mdi mdi-lock"
                            title="vault is private">
                        </p>
                    </div>
                    <div class="col-lg-4 col-6">
                        <p class="rounded rounded-pill bg-primary text-light text-shadow p-2 mb-0 fs-5s text-center">
                            Keeps: {{
                                keeps.length }}
                        </p>
                    </div>
                    <div class="col-lg-4 col-2 text-end">
                        <p @click="deleteVault(vault.id)" v-if="account && account.id == vault.creatorId" role="button"
                            title="Delete this Vault" class="mb-0 fs-2 text-danger mdi mdi-delete-circle-outline"></p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <h2>Keeps</h2>
                <hr />
            </div>
            <div class="col-12 masonry p-lg-0">
                <div class="parent" v-for="keep in keeps" :key="keep.id">
                    <KeepCard class="card-margin drop-shadow" :keep="keep" />
                    <p role="button" v-if="account?.id == vault.creatorId"
                        @click="removeKeepFromVault(keep.vaultKeepId)" class="btn btn-danger child p-0 px-1"><i
                            class="mdi mdi-close" name="remove keep from vault" title="remove keep from vault"></i></p>
                </div>
            </div>
        </div>


    </div>
</template>


<style lang="scss" scoped>
.parent {
    position: relative;
}

.child {
    position: absolute;
    top: 0rem;
    right: 0;
}

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