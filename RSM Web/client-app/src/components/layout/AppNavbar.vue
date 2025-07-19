<template>
  <nav class="navbar navbar-expand-lg bg-white shadow-sm px-3 py-2 mx-3 mx-md-4 mx-lg-5 rounded-5">
    <div class="container-fluid d-flex align-items-center justify-content-between w-100">
      <!-- Sol: Brand -->
      <router-link to="/" class="navbar-brand fw-bold fs-4 text-primary">
        RSM
      </router-link>

      <!-- Burger (mobile) -->
      <button class="navbar-toggler border-0" type="button" @click="toggleMenu">
        <i class="bi bi-list toggle-icon list-icon" :class="{ 'hide': isMenuOpen }"></i>
        <i class="bi bi-x-lg toggle-icon x-icon" :class="{ 'show': isMenuOpen }"></i>
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
            class="btn btn-primary btn-sm rounded-4 p-2">
            Kabinet
          </router-link>

          <router-link v-if="authStore.isAuthenticated && authStore.user?.role === 'Customer'" to="/profile"
            class="btn btn-outline-primary btn-sm rounded-4 p-2">
            Profil
          </router-link>
        </div>

        <!-- Auth düymələri -->
        <div class="d-flex align-items-center gap-2">
          <router-link v-if="!authStore.isAuthenticated" to="/login" class="btn-auth btn-login">
            Giriş
            <i class="bi bi-box-arrow-in-right login-icon"></i>
          </router-link>

          <router-link v-if="!authStore.isAuthenticated" to="/register" class="btn-auth btn-register">
            Qeydiyyat
            <i class="bi bi-pencil-square register-icon"></i>
          </router-link>

          <button v-if="authStore.isAuthenticated" @click="logout" class="btn btn-outline-danger btn-sm rounded-4 p-2">
            Çıxış
          </button>
        </div>
      </div>
    </div>

    <!-- Mobile Menu -->
    <transition name="slide">
      <div v-if="isMenuOpen" class="mobile-menu  d-lg-none">
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

          <div class="auth-buttons-mobile d-flex flex-row gap-2">
            <router-link to="/login" class="btn-auth btn-sm" @click="closeMenu">Giriş</router-link>
            <router-link to="/register" class="btn-auth btn-sm" @click="closeMenu">Qeydiyyat</router-link>
          </div>

          <button v-if="authStore.isAuthenticated" @click="handleLogout" class="btn btn-outline-danger btn-sm">
            Çıxış
          </button>
        </div>
      </div>
    </transition>
  </nav>
  <div v-if="isMenuOpen" class="backdrop d-lg-none" @click="closeMenu"></div>
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
.navbar {
  z-index: 999;
}

.nav-link {
  position: relative;
  color: #333;
  transition: color 0.3s ease;
}


.nav-link::after {
  content: "";
  position: absolute;
  left: 0;
  bottom: -4px;
  width: 0%;
  height: 2px;
  background-color: #0d6efd;
  transition: width 0.6s ease;
}

.nav-link:hover {
  color: #0d6efd;
}

.nav-link:hover::after {
  color: #5791e8;
  width: 100%;
}

.router-link-active.nav-link {
  color: #5791e8;
  font-weight: 600;
}

.router-link-active.nav-link::after {
  width: 100%;
}

.toggle-icon {
  font-size: 1.5rem;
  transition: opacity 0.3s ease, transform 0.3s ease;
  position: absolute;
}

.list-icon {
  opacity: 1;
  transform: scale(1);
}

.x-icon {
  opacity: 0;
  transform: scale(0.8);
}

.show {
  opacity: 1 !important;
  transform: scale(1) !important;
}

.hide {
  opacity: 0 !important;
  transform: scale(0.8) !important;
}

.navbar-toggler:focus {
  outline: none;
  box-shadow: none;
  border: none;
}

.navbar-toggler {
  position: relative;
  width: 2rem;
  height: 2rem;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0;
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

.backdrop {
  position: fixed;
  inset: 0;
  z-index: 998;
  background-color: rgba(255, 255, 255, 0.4);
  backdrop-filter: blur(3px);
  transition: backdrop-filter 0.3s ease, background-color 0.3s ease;
}

.btn-auth {
  padding: 0.45rem 1rem;
  font-size: 0.95rem;
  border-radius: 12px;
  background: rgba(255, 255, 255, 0.15);
  /* Şüşə hissi */
  border: 1px solid rgba(255, 255, 255, 0.4);
  backdrop-filter: blur(8px);
  -webkit-backdrop-filter: blur(8px);
  /* Safari üçün */
  color: #0d6efd;
  letter-spacing: 0.5px;
  box-shadow: 0 2px 8px rgba(13, 110, 253, 0.08);
  transition: all 0.3s ease;
  text-decoration: none;
  font-family: 'Inter', 'Segoe UI', 'Roboto', sans-serif;
}

.btn-auth:hover {
  background: rgba(13, 110, 253, 0.08);
  color: #0d6efd; 
  box-shadow: 0 3px 10px rgba(13, 110, 253, 0.1); 
  transform: none; 
}

.auth-buttons-mobile {
  justify-content: space-around;
  flex-wrap: wrap;
  margin-top: 0.5rem;
}

@media (max-width: 576px) {
  .btn-auth {
    padding: 0.4rem 1rem;
    font-size: 0.85rem;
    border-radius: 10px;
  }

  .login-icon {
    display: none;
  }

  .register-icon {
    display: none;
  }
}

.login-icon,
.register-icon {
  opacity: 0;
  transform: translateX(-6px);
  transition: opacity 0.3s ease, transform 0.3s ease, color 0.3s ease;
  font-size: 1rem;
  color: #0d6efd;
}

.btn-login:hover .login-icon,
.btn-register:hover .register-icon {
  opacity: 1;
  transform: translateX(0);
  color: #0d6efd;
}

.btn-login,
.btn-register {
  display: inline-flex;
  align-items: center;
  gap: 0.35rem;
}

</style>
