<script setup>
import { onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';


const vaultData = ref({
    name: '',
    description: '',
    isPrivate: false,
    img: ''
})

async function createVault() {
    try {
        await vaultsService.createVault(vaultData.value)
        clearVaultData()
        Pop.success("Vault Successfully Created")
    }
    catch (error) {
        Pop.error(error);
    }

}
function clearVaultData() {
    vaultData.value = {
        name: '',
        description: '',
        isPrivate: false,
        img: ''
    }
}

</script>


<template>
    <!-- Button trigger modal -->
    <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#createVaultModal">
        Launch demo modal
    </button> -->

    <!-- Modal -->
    <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createVaultModalLabel">Create A Vault</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createVault()">
                        <div class="form-floating mb-3 text-grey">
                            <input v-model="vaultData.name" type="text" class="form-control " id="name" placeholder=""
                                required minlength="1" maxlength="255">
                            <label class="" for="name">Vault Name</label>
                        </div>
                        <div class="form-floating mb-3 text-grey">
                            <input v-model="vaultData.img" type="url" class="form-control" id="img" placeholder=""
                                required>
                            <label for="img">Vault Image Url</label>
                        </div>
                        <div class="form-floating mb-3 text-grey">
                            <textarea v-model="vaultData.description" type="url" class="form-control textarea-height"
                                id="name" placeholder="" required minlength="1" maxlength="1000"></textarea>
                            <label for="name">Vault Description</label>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-6">
                                <div class="form-check fs-5 ms-2">
                                    <label class="form-check-label" for="flexCheckDefault">
                                        Private Vault
                                    </label>
                                    <input v-model="vaultData.isPrivate" class="form-check-input" type="checkbox"
                                        value="" id="flexCheckDefault" title="Set Vault as Private">
                                </div>
                            </div>
                            <div class="col-6 text-end">
                                <button type="submit" class="btn btn-rounded rounded-pill btn-primary">Create
                                    Vault</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.textarea-height {
    height: 20dvh;
}

.text-grey {
    color: #696969;
}
</style>