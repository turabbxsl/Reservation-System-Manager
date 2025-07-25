<template>
    <button class="btn btn-outline-light border-0 m-2 position-fixed top-0 start-0 d-none d-md-block"
        style="z-index: 2000; width: 40px; height: 40px; font-size: 24px;" type="button" @click="toggleSidebar">
        ☰
    </button>

    <div class="d-flex">
        <!-- Desktop Sidebar -->
        <aside
            :class="['sidebar', 'd-none', 'd-md-flex', 'flex-column', 'bg-dark', 'text-light', 'p-3', { collapsed: isCollapsed, 'sidebar-hidden-mobile': isMobileSidebarOpen }]">
            <div v-if="!isCollapsed" class="mb-4 text-center fw-semibold fs-5">
                {{ companyName || "Şirkət adı" }}
            </div>
            <div v-else class="mb-4 text-center sidebar-collapsed-company-name">
            </div>
            <nav>
                <ul class="nav flex-column">
                    <li class="nav-item">
                        <router-link to="/dashboard/home" class="nav-link d-flex align-items-center">
                            <i class="bi bi-house me-2"></i>
                            <span>Əsas səhifə</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/dashboard/reservations" class="nav-link d-flex align-items-center">
                            <i class="bi bi-calendar-check me-2"></i>
                            <span>Rezervasiyalar</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/dashboard/specialities" class="nav-link d-flex align-items-center">
                            <i class="bi bi-person-vcard me-2"></i>
                            <span>Sahələr</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/dashboard/services" class="nav-link d-flex align-items-center">
                            <i class="bi bi-tools me-2"></i>
                            <span>Xidmətlər</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/dashboard/users" class="nav-link d-flex align-items-center">
                            <i class="bi bi-people me-2"></i>
                            <span>Əməkdaşlar</span>
                        </router-link>
                    </li>
                </ul>
            </nav>
        </aside>

        <button class="btn btn-outline-light border-0 position-fixed top-0 start-0 d-md-none"
            style="z-index: 2000; width: 40px; height: 40px; font-size: 24px;" type="button" data-bs-toggle="offcanvas"
            data-bs-target="#mobileSidebar" @click="onMobileSidebarOpen" v-show="!isMobileSidebarOpen">
            ☰
        </button>

        <!-- Mobile Offcanvas -->
        <div class="offcanvas offcanvas-start text-light bg-dark" tabindex="-1" id="mobileSidebar" ref="mobileSidebar">
            <div class="offcanvas-header border-bottom d-flex justify-content-between align-items-center">
                <div class="sidebar-collapsed-company-name-mobile">
                    {{ companyName || "Şirkət adı" }}
                </div>
                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="offcanvas"
                    aria-label="Bağla"></button>
            </div>
            <div class="offcanvas-body">
                <ul class="nav flex-column">
                    <li class="nav-item">
                        <router-link to="/dashboard/home" class="nav-link d-flex align-items-center">
                            <i class="bi bi-house me-2"></i>
                            <span>Əsas səhifə</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/dashboard/reservations" class="nav-link d-flex align-items-center">
                            <i class="bi bi-calendar-check me-2"></i>
                            <span>Rezervasiyalar</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/dashboard/specialities" class="nav-link d-flex align-items-center">
                            <i class="bi bi-person-vcard me-2"></i>
                            <span>Sahələr</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/dashboard/services" class="nav-link d-flex align-items-center">
                            <i class="bi bi-tools me-2"></i>
                            <span>Xidmətlər</span>
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/dashboard/users" class="nav-link d-flex align-items-center">
                            <i class="bi bi-people me-2"></i>
                            <span>Əməkdaşlar</span>
                        </router-link>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "DashSidebar",
    data() {
        return {
            companyName: '',
            isCollapsed: false,
            isMobileSidebarOpen: false
        };
    },
    created() {
        const user = JSON.parse(localStorage.getItem('user'));
        if (user && user.companyName) {
            this.companyName = user.companyName;
        }
    },
    mounted() {
        // Bootstrap offcanvas event listener for open/close
        const offcanvas = this.$refs.mobileSidebar;
        if (offcanvas) {
            offcanvas.addEventListener('shown.bs.offcanvas', () => {
                this.isMobileSidebarOpen = true;
            });
            offcanvas.addEventListener('hidden.bs.offcanvas', () => {
                this.isMobileSidebarOpen = false;
            });
        }
    },
    methods: {
        toggleSidebar() {
            this.isCollapsed = !this.isCollapsed;
        },
        onMobileSidebarOpen() {
            this.isMobileSidebarOpen = true;
        }
    }
};
</script>

<style scoped>
.sidebar,
.offcanvas-body {
    background-color: #212529;
    color: #f8f9fa;
}

.nav-link {
    color: #adb5bd;
    padding: 0.5rem 1rem;
    text-decoration: none;
    transition: background-color 0.2s ease-in-out, padding 0.2s ease;
    display: flex;
    align-items: center;
}

.nav-link:hover {
    background-color: #343a40;
    color: #ffffff;
    border-radius: 6px;
}

.nav-link.router-link-active {
    background-color: #495057;
    font-weight: bold;
    color: #ffffff;
    border-radius: 6px;
}

/* Desktop collapse üçün əlavə qalsın */
.sidebar.collapsed {
    width: 70px;
}

.sidebar .mb-4 {
    transition: all 0.2s ease;
}

.sidebar.collapsed .nav-link {
    padding-left: 0.5rem;
    padding-right: 0.5rem;
    justify-content: center;
}

.sidebar.collapsed .nav-link span {
    display: none;
}

.sidebar {
    width: 240px;
    min-height: 100vh;
    transition: width 0.3s ease;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    position: relative;
}

/* Mobil sidebar açıldıqda desktop sidebar gizlədilsin */
.sidebar.sidebar-hidden-mobile {
    opacity: 0;
    pointer-events: none;
    transition: opacity 0.3s ease;
}

/* Sidebar collapse şirkət adı */
.sidebar-collapsed-company-name {
    font-weight: 600;
    font-size: 1rem;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    user-select: none;
}

/* Mobil offcanvas header şirkət adı */
.sidebar-collapsed-company-name-mobile {
    font-weight: 600;
    font-size: 1.2rem;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    user-select: none;
    flex-grow: 1;
    text-align: center;
    color: #f8f9fa;
}

.offcanvas-body {
    max-height: 90vh;
    overflow-y: auto;
}

@media (max-width: 767.98px) {
    .sidebar {
        display: none !important;
    }
}
</style>
