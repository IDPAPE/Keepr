<script setup>
import { Vault } from '../models/Vault.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';


const props = defineProps({ vault: { type: Vault, required: true } })
const imgUrl = `url(${props.vault.img})`


</script>


<template>
    <RouterLink :to="{ name: 'Vault', params: { vaultId: vault.id } }">

        <div class="card container-fluid d-flex text-light selectable mb-4 drop-shadow">
            <div class="row flex-grow-1">

            </div>
            <div class="row ">
                <div
                    class="col m-1 rounded p-2 d-flex justify-content-between align-items-center text-shadow kalam-regular">
                    <p class="fw-bold m-0 fs-5">
                        {{ vault.name }}
                    </p>
                    <i v-if="vault.isPrivate == true" class="text-warning fs-5 mdi mdi-lock"
                        title="vault is private"></i>
                </div>
            </div>
        </div>
    </RouterLink>
</template>


<style lang="scss" scoped>
.card {
    height: 20dvh;
    background-image: v-bind(imgUrl);
    background-size: cover;
    background-position: center;
}

.bg-glass {
    background-color: rgba(68, 68, 68, 0.9);
}

.text-shadow {
    text-shadow: 2px 2px 4px black;
}

.drop-shadow {
    filter: drop-shadow(3px 3px 6px rgb(135, 134, 134));
}
</style>