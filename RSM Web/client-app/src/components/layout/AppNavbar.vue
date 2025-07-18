<template>
  <nav class="navbar navbar-expand-lg bg-white shadow-sm px-3 py-2 mx-3 mx-md-4 mx-lg-5">
     <div class="container-fluid d-flex align-items-center justify-content-between w-100">

      <!-- Sol: Brand -->
      <router-link to="/" class="navbar-brand fw-bold fs-4 text-primary">
        RSM
      </router-link>

      <!-- Burger (mobile) -->
      <button class="navbar-toggler border-0" type="button" @click="toggleMenu">
        <i class="bi" :class="isMenuOpen ? 'bi-x-lg' : 'bi-list'" style="font-size: 1.5rem;"></i>
      </button>

      <!-- Desktop Menü -->
      <div class="collapse navbar-collapse d-none d-lg-flex w-100 justify-content-between">
        
        <!-- Orta: Menü -->
        <ul class="navbar-nav mx-auto gap-lg-4">
          <li class="nav-item">
            <router-link to="/" class="nav-link">Əsas səhifə</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/services" class="nav-link">Xidmətlər</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/partners" class="nav-link">Partnyorlar</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/contact" class="nav-link">Əlaqə</router-link>
          </li>
        </ul>

        <!-- Rezervasiya / Kabinet / Profil -->
        <div class="d-flex align-items-center gap-3 ms-4 me-4">
          <router-link v-if="authStore.isAuthenticated && authStore.user?.role === 'Customer'" to="/reserve"
            class="btn btn-primary btn-sm">
            Rezervasiya et
          </router-link>

          <router-link v-if="authStore.isAuthenticated && authStore.user?.role !== 'Customer'" to="/"
            class="btn btn-primary btn-sm">
            Kabinet
          </router-link>

          <router-link v-if="authStore.isAuthenticated && authStore.user?.role === 'Customer'" to="/profile"
            class="btn btn-outline-primary btn-sm">
            Profil
          </router-link>
        </div>

        <!-- Auth düymələri -->
        <div class="d-flex align-items-center gap-2">
          <router-link v-if="!authStore.isAuthenticated" to="/login" class="btn btn-outline-secondary btn-sm">
            Giriş
          </router-link>

          <router-link v-if="!authStore.isAuthenticated" to="/register" class="btn btn-outline-secondary btn-sm">
            Qeydiyyat
          </router-link>

          <button v-if="authStore.isAuthenticated" @click="logout" class="btn btn-outline-danger btn-sm">
            Çıxış
          </button>
        </div>
      </div>
    </div>

    <!-- Mobile Menu -->
    <transition name="slide">
      <div v-if="isMenuOpen" class="mobile-menu d-lg-none">
        <div class="p-4 d-flex flex-column gap-3">
          <router-link to="/" class="nav-link" @click="closeMenu">Əsas səhifə</router-link>
          <router-link to="/services" class="nav-link" @click="closeMenu">Xidmətlər</router-link>
          <router-link to="/partners" class="nav-link" @click="closeMenu">Partnyorlar</router-link>
          <router-link to="/contact" class="nav-link" @click="closeMenu">Əlaqə</router-link>

          <router-link v-if="authStore.isAuthenticated && authStore.user?.role === 'Customer'" to="/reserve"
            class="btn btn-primary btn-sm" @click="closeMenu">
            Rezervasiya et
          </router-link>

          <router-link v-if="authStore.isAuthenticated && authStore.user?.role !== 'Customer'" to="/"
            class="btn btn-primary btn-sm" @click="closeMenu">
            Kabinet
          </router-link>

          <router-link v-if="authStore.isAuthenticated && authStore.user?.role === 'Customer'" to="/profile"
            class="btn btn-outline-primary btn-sm" @click="closeMenu">
            Profil
          </router-link>

          <router-link v-if="!authStore.isAuthenticated" to="/login" class="btn btn-outline-secondary btn-sm"
            @click="closeMenu">
            Giriş
          </router-link>

          <router-link v-if="!authStore.isAuthenticated" to="/register" class="btn btn-outline-secondary btn-sm"
            @click="closeMenu">
            Qeydiyyat
          </router-link>

          <button v-if="authStore.isAuthenticated" @click="handleLogout" class="btn btn-outline-danger btn-sm">
            Çıxış
          </button>
        </div>
      </div>
    </transition>
  </nav>
</template>



<script setup>
import { ref } from 'vue'
import { useAuthStore } from '@/stores/authStore'
import { useRouter } from 'vue-router'

const isMenuOpen = ref(false)
const authStore = useAuthStore()
const router = useRouter()

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value
}

const closeMenu = () => {
  isMenuOpen.value = false
}

const handleLogout = () => {
  authStore.logout()
  isMenuOpen.value = false
  router.push({ name: 'Home' })
}

const logout = () => {
  authStore.logout()
  router.push({ name: 'Home' })
}
</script>

<style scoped>
.nav-link {
  color: #333;
  transition: 0.3s;
}

.nav-link:hover {
  color: #0d6efd;
}

.mobile-menu {
  position: absolute;
  top: 100%;
  left: 0;
  width: 100%;
  background-color: #fff;
  z-index: 999;
  border-top: 1px solid #ddd;
  box-shadow: 0 8px 30px rgba(0, 0, 0, 0.1);
}

.slide-enter-active,
.slide-leave-active {
  transition: all 0.3s ease;
}

.slide-enter-from,
.slide-leave-to {
  transform: translateY(-20px);
  opacity: 0;
}
</style>
