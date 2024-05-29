<script setup>
import { ref } from 'vue';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';


const keepData = ref({
    name: '',
    img: '',
    description: '',
    // creatorId: ''
})

async function createKeep() {
    try {
        keepsService.createKeep(keepData.value)
        keepData.value = {
            name: '',
            img: '',
            description: '',
            // creatorId: ''
        }
        Pop.success('Keep Successfully Created')
    }
    catch (error) {
        Pop.error(error);
    }
}

</script>


<template>
    <!-- Button trigger modal -->
    <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#createKeepModal">
        Launch demo modal
    </button> -->

    <!-- Modal -->
    <div class="modal fade" id="createKeepModal" tabindex="-1" aria-labelledby="createKeepModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createKeepModalLabel">Create A Keep</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createKeep()">
                        <div class="form-floating mb-3 text-grey">
                            <input v-model="keepData.name" type="text" class="form-control " id="name" placeholder=""
                                required minlength="1" maxlength="255">
                            <label class="" for="name">Keep Name</label>
                        </div>
                        <div class="form-floating mb-3 text-grey">
                            <input v-model="keepData.img" type="url" class="form-control" id="img" placeholder=""
                                required>
                            <label for="img">Keep Image Url</label>
                        </div>
                        <div class="form-floating mb-3 text-grey">
                            <textarea v-model="keepData.description" type="url" class="form-control textarea-height"
                                id="name" placeholder="" required></textarea>
                            <label for="name">Keep Description</label>
                        </div>
                        <div class="text-end">
                            <button type="submit" class="btn btn-rounded rounded-pill btn-primary">Create Keep</button>
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