<script setup>
import { useRoute } from 'vue-router';
import { router } from '../router.js';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';


const route = useRoute()
const vault = computed(() => AppState.activeVault)

async function getActiveVault() {
    try {
        vaultsService.getActiveVault(route.params.vaultId)
    }
    catch (error) {
        Pop.error(error);
        console.error(error)
    }
}

onMounted(() => {
    getActiveVault()
})
</script>


<template>
    <div class="container">
        <div class="row">
            <div class="col rounded">
                <div class="row">

                </div>
                <div v-if="AppState.activeVault" class="row">
                    <h1>{{ vault.name }}</h1>
                    <h3>by {{ vault.creator }}</h3>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped></style>