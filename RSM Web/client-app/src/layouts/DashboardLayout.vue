<template>
    <div class="dashboard-layout d-flex">
        <DashSidebar :show-sidebar="showSidebar" @toggle-sidebar="toggleSidebar" />

        <div class="main-content flex-grow-1">
            <DashHeader />

            <main class="page-content">
                <router-view />
            </main>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import DashSidebar from '@/components/dashboard/DashSidebar.vue';
import DashHeader from '@/components/dashboard/DashHeader.vue';

const showSidebar = ref(true);
const toggleSidebar = () => {
    showSidebar.value = !showSidebar.value;
};
</script>

<style scoped>
.dashboard-layout {
    min-height: 100vh;
    background-color: #f1f3f5;
}

.sidebar {
    width: 250px;
    background-color: #212529;
    color: #f8f9fa;
}

.main-content {
    display: flex;
    flex-direction: column;
    width: 100%;
}

.page-content {
    flex-grow: 1;
    padding: 2rem;
    background-color: #ffffff;
    border-top: 1px solid #dee2e6;
    border-radius: 8px;
    margin: 1rem;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
}
</style>
