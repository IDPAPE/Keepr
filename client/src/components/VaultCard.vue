<script setup>
import { Vault } from '../models/Vault.js';


const props = defineProps({ vault: { type: Vault, required: true } })
const imgUrl = `url(${props.vault.img})`


</script>


<template>
    <RouterLink :to="{ name: 'Vault', params: { vaultId: vault.id } }">

        <div class="card container-fluid d-flex text-light selectable mb-4 drop-shadow">
            <div class="row flex-grow-1">

            </div>
            <div class="row ">
                <div class="col m-1 rounded p-2 d-flex justify-content-between">
                    <h5 class="fw-bold m-0 text-shadow">
                        {{ vault.name }}
                    </h5>
                    <h5>
                        <i v-if="vault.isPrivate == true" class="text-warning mdi mdi-lock"
                            title="vault is private"></i>
                    </h5>
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
</style>