<script setup>
import { ref } from 'vue';
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';
import { Modal } from 'bootstrap';


const accountInfoData = ref({
    coverImg: '',
    picture: '',
    name: '',
    email: ''
})

async function updateAccountInfo() {
    try {
        accountService.updateAccountInfo(accountInfoData.value)
        accountInfoData.value = {
            coverImg: '',
            picture: '',
            name: '',
            email: ''
        }
        Modal.getOrCreateInstance('#EditAccountModal').hide()
        Pop.success('Account Info Updated Successfully')
    }
    catch (error) {
        Pop.toast('could not update user info', 'error');
        console.error(error)
    }
}



</script>


<template>
    <!-- Button trigger modal -->
    <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#EditAccountModal">
  Launch demo modal
</button> -->

    <!-- Modal -->
    <div class="modal fade" id="EditAccountModal" tabindex="-1" aria-labelledby="EditAccountModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="EditAccountModalLabel">Edit Account</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="updateAccountInfo()">
                        <div class="form-floating mb-3">
                            <input v-model="accountInfoData.coverImg" type="url" class="form-control" id="coverImg"
                                placeholder="">
                            <label for="coverImg">Cover Image Url</label>
                        </div>
                        <div class="form-floating mb-3">
                            <input v-model="accountInfoData.picture" type="url" class="form-control" id="profilePicture"
                                placeholder="">
                            <label for="profilePicture">Profile Picture Url</label>
                        </div>
                        <div class="form-floating">
                            <input v-model="accountInfoData.name" type="text" class="form-control" id="userName"
                                placeholder="">
                            <label for="userName">Username</label>
                        </div>
                        <hr />
                        <button type="submit" class="btn btn-primary">Save changes</button>
                    </form>
                </div>

            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped></style>