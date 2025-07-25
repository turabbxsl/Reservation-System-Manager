<template>
    <header class="dashboard-header d-flex bg-dark justify-content-between align-items-center px-4 py-2">
        <div class="flex-grow-1">
            <router-link to="/" class="btn btn-sm btn-outline-light me-2">
                <i class="bi bi-arrow-left"></i>
                Sayta keç
            </router-link>
        </div>
        <div class="d-flex align-items-center gap-3">
            <span class="me-3 text-light fw-semibold">
                <i class="bi bi-person-circle me-1"></i>
                {{ fullName }}
            </span>
            <button class="btn btn-sm btn-outline-danger d-flex align-items-center" @click="logout">
                <i class="bi bi-box-arrow-right me-1"></i>
                Çıxış
            </button>
        </div>
    </header>
</template>

<script>
import { useAuthStore } from '@/stores/authStore';

export default {
    name: "DashHeader",
    data() {
        return {
            fullName: ''
        };
    },
    created() {
        const user = JSON.parse(localStorage.getItem('user'));
        if (user && user.fullName) {
            this.fullName = user.fullName;
        }
    },
    methods: {
        logout() {
            const authStore = useAuthStore();
            authStore.logout();
            this.$router.push('/');
        }
    }
};
</script>

<style scoped>
.dashboard-header {
    background-color: #1f1f1f;
    color: #f8f9fa;
    border-bottom: 1px solid #343a40;
    height: 60px;
    width: 100%;
    position: sticky;
    top: 0;
    z-index: 1020;
}

.btn-outline-light {
    border-color: #adb5bd;
    color: #f8f9fa;
}

.btn-outline-light:hover {
    background-color: #495057;
    color: #ffffff;
}

@media (max-width: 767.98px) {
    .dashboard-header {
        padding: 0.5rem 1rem;
        height: auto;
        flex-wrap: wrap;
        gap: 0.5rem;
    }

    .dashboard-header .btn {
        font-size: 12px;
        padding: 4px 8px;
    }

    .dashboard-header .text-light {
        font-size: 13px;
        white-space: nowrap;
    }

    .dashboard-header .gap-3 {
        gap: 0.5rem !important;
    }

    .dashboard-header .me-3 {
        margin-right: 0 !important;
    }

    .dashboard-header {
        overflow-x: auto;
    }

    .dashboard-header .btn-outline-light:first-child {
        margin-left: 3rem;
    }
}
</style>
