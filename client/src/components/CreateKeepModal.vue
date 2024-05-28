<script setup>
import { ref } from 'vue';


const keepData = ref({
    name: '',
    img: '',
    description: '',
    creatorId: ''
})

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
                    <form @submit.prevent="">
                        <div class="form-floating mb-3 text-grey">
                            <input v-model="keepData.name" type="text" class="form-control " id="name" placeholder="">
                            <label class="" for="name">Keep Name</label>
                        </div>
                        <div class="form-floating mb-3 text-grey">
                            <input v-model="keepData.img" type="url" class="form-control" id="img" placeholder="">
                            <label for="img">Keep Image Url</label>
                        </div>
                        <div class="form-floating mb-3 text-grey">
                            <textarea v-model="keepData.description" type="url" class="form-control textarea-height"
                                id="name" placeholder=""></textarea>
                            <label for="name">Keep Description</label>
                        </div>
                        <div class="text-end">
                            <button type="submit" class="btn btn-rounded rounded-pill btn-success">Create Keep</button>
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