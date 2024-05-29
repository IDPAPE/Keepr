<script setup>
import { computed, ref } from 'vue';
import { Keep } from '../models/Keep.js';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';
import { Modal } from 'bootstrap';
import { vaultsService } from '../services/VaultsService.js';

const myVaults = computed(() => AppState.myVaults)
const keep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const titleString = computed(() => `navigate to ${keep.value.creator.name}'s page`)


const selectedVaultData = ref({
    vaultId: 0,
    keepId: 0
})

async function deleteKeep(keepId) {
    try {
        const confirmation = await Pop.confirm('Are you sure you want to delete this Keep?')
        if (confirmation == false) { return }
        keepsService.deleteKeep(keepId)
        Modal.getOrCreateInstance('#keepModal').hide()
    }
    catch (error) {
        Pop.error(error);
        console.error(error)
    }
}

async function saveToVault() {
    try {
        // console.log('asdf')
        const confirmation = await Pop.confirm('Add this keep to vault?', "", "Add", "question")
        if (confirmation == false) { return }
        vaultsService.saveToVault(selectedVaultData.value)
        selectedVaultData.value = {
            vaultId: 0,
            keepId: 0
        }
    }
    catch (error) {
        Pop.error(error);
        console.error(error)
    }
}

function closeModal() {
    Modal.getOrCreateInstance('#keepModal').hide()
}
</script>


<template>
    <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#keepModal">
  Launch demo modal
</button> -->

    <!-- Modal -->
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
                <div v-if="AppState.activeKeep" class="modal-body container-fluid p-0">
                    <section class="row">
                        <div class="col-lg-6 col-12">
                            <img class=" keep-img img-rounded" :src="keep.img" alt="keep image">
                        </div>
                        <div class="col-lg-6 col-12 d-flex flex-column justify-content-between p-3 px-5 text-center">

                            <div class="d-flex align-items-center justify-content-between">
                                <p @click="deleteKeep(keep.id)" v-if="keep.creatorId == AppState.account?.id"
                                    role="button" title="Delete this Keep"
                                    class="fs-2 text-danger mdi mdi-delete-circle-outline"></p>
                                <p v-else></p>
                                <p>
                                    <i title="times viewed" class="mdi mdi-eye-outline"></i> {{ keep.views }}
                                    <i title="times kept" class="ms-2 mdi mdi-alpha-k-box-outline"></i> {{ keep.kept }}
                                </p>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                                    name="close modal" title="close modal"></button>
                            </div>

                            <div class="px-lg-5 my-4 my-lg-0">
                                <h2 class="fw-bold">{{ keep.name }}</h2>
                                <p class="text-start">{{ keep.description }}</p>
                            </div>

                            <div class="d-flex align-items-center justify-content-between">
                                <form @submit.prevent="saveToVault()" class="d-flex w-50">
                                    <select v-if="account" v-model="selectedVaultData.vaultId" class="form-select me-2"
                                        aria-label="Default select example" default="Add To Vault" required>
                                        <option v-for="vault in AppState.myVaults" :key="vault.id" :value="vault.id">
                                            {{ vault.name }}<small v-if="vault.isPrivate == true">: Private</small>
                                        </option>
                                    </select>
                                    <button v-if="account" type="submit" class="btn btn-primary rounded-pill"
                                        name="save to vault" title="save to vault">Save</button>
                                </form>
                                <div>
                                    <RouterLink @click="closeModal()" class="d-flex align-items-center text-dark"
                                        :to="{ name: 'Profile', params: { profileId: keep.creatorId } }"
                                        :title="titleString" name="navigate to user's page">
                                        <h5 class="mb-0 me-2">{{ keep.creator.name }}</h5>
                                        <img class="pfp" :src="keep.creator.picture" alt="">
                                    </RouterLink>
                                </div>
                            </div>

                        </div>
                    </section>
                </div>
                <!-- <div class="modal-footer">
      </div> -->
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.pfp {
    height: 5dvh;
    aspect-ratio: 1/1;
    border-radius: 50%;
    filter: drop-shadow(0 0 0.25rem rgb(58, 58, 58));
    object-fit: cover;
    object-position: top;
}

.keep-img {
    object-fit: cover;
    object-position: center;
    max-width: 100%;
    height: 100%;
}

//classes for small screens
@media screen and (max-width: 992px) {
    .img-rounded {
        border-radius: 0.375rem 0.375rem 0rem 0rem;
    }
}

//classes for large screens
@media screen and (min-width: 992px) {
    .img-rounded {
        border-radius: 0.375rem 0rem 0rem 0.375rem
    }
}
</style>