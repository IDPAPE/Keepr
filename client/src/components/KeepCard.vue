<script setup>
import { computed, onUnmounted } from 'vue';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import { Modal } from 'bootstrap';

const props = defineProps({ keep: { type: Keep, required: true } })
const titleString = computed(() => `navigate to ${props.keep.creator.name}'s page`)

function setActiveKeep(keepId) {
    keepsService.setActiveKeep(keepId)
    Modal.getOrCreateInstance('#keepModal').show()
}

function closeModal() {
    Modal.getOrCreateInstance('#keepModal').hide()
}

onUnmounted(() => {
    closeModal()
})

</script>


<template>
    <div @click="setActiveKeep(keep.id)" class="container-fluid d-flex flex-column selectable">
        <div class="row parent">
            <img class="rounded p-0" :src="keep.img" alt="">
            <div class="child">
                <div class="col px-1 text-light d-flex justify-content-between align-items-center">
                    <p class="kalam-bold text-shadow fs-3 mb-0 me-1">{{ keep.name }}</p>
                    <RouterLink @click.stop :to="{ name: 'Profile', params: { profileId: keep.creatorId } }"
                        :title="titleString" name="link to user's page">
                        <img @click="closeModal()" class="pfp d-none d-md-block" :src="keep.creator.picture" alt="">
                    </RouterLink>
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
    bottom: 1rem;
    right: 0;
}

.text-shadow {
    text-shadow: 2px 2px 4px black;
}

.pfp {
    height: 4dvh;
    aspect-ratio: 1/1;
    border-radius: 50%;
    filter: drop-shadow(0 0 0.25rem rgb(58, 58, 58));
    object-fit: cover;
    object-position: top;
}

.rounded {
    border-radius: 1rem;
}
</style>