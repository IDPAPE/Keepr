<script setup>
import { computed } from 'vue';
import { Keep } from '../models/Keep.js';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';
import { Modal } from 'bootstrap';


const keep = computed(() => AppState.activeKeep)

async function deleteKeep(keepId) {
    try {
        const confirmation = await Pop.confirm('Are you sure you want to delete this Keep?')
        if (confirmation == false) { return }
        keepsService.deleteKeep(keepId)
        Modal.getOrCreateInstance('#keepModal').hide()
    }
    catch (error) {
        Pop.error(error);
    }
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
                        <div class="col-6">
                            <img class="w-100 rounded rounded-end-0" :src="keep.img" alt="">
                        </div>
                        <div class="col-6 d-flex flex-column justify-content-between p-3 px-5 text-center">

                            <div class="d-flex align-items-center justify-content-between">
                                <p @click="deleteKeep(keep.id)" v-if="keep.creatorId == AppState.account?.id"
                                    role="button" title="Delete this Keep"
                                    class="fs-2 text-danger mdi mdi-delete-circle-outline"></p>
                                <p v-else></p>
                                <p>
                                    <i title="times viewed" class="mdi mdi-eye-outline"></i> {{ keep.views }}
                                    <i title="times kept" class="ms-2 mdi mdi-alpha-k-box-outline"></i> {{ keep.kept }}
                                </p>
                                <button type="button" class="btn-close" data-bs-dismiss="modal"
                                    aria-label="Close"></button>
                            </div>

                            <div class="px-5">
                                <h2 class="fw-bold">{{ keep.name }}</h2>
                                <p class="text-start">{{ keep.description }}</p>
                            </div>

                            <div class="d-flex align-items-center justify-content-between">
                                <h4>save to my valuts</h4>
                                <h5>{{ keep.creator.name }}</h5>
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


<style lang="scss" scoped></style>