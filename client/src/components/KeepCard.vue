<script setup>
import { computed } from 'vue';
import { Keep } from '../models/Keep.js';


const props = defineProps({ keep: { type: Keep, required: true } })
const imgUrl = computed(() => `url(${props.keep.img})`)

</script>


<template>
    <div class="container-fluid d-flex flex-column">
        <div class="row parent">
            <img :src="keep.img" alt="">
            <div class="child">
                <div class="col px-4 text-light d-flex justify-content-between">
                    <h3 class="fw-bold text-shadow">{{ keep.name }}</h3>
                    <img class="pfp d-none d-md-block" :src="keep.creator.picture" alt="">
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
    height: 5dvh;
    aspect-ratio: 1/1;
    border-radius: 50%;
    filter: drop-shadow(0 0 0.25rem black);
}
</style>