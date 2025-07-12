<template>
  <div>
    <!-- Header -->
    <nav class="navbar navbar-expand-lg navbar-light bg-white shadow-sm px-4">
      <a class="navbar-brand fw-bold text-primary" href="#">RSM</a>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav ms-auto">
          <li class="nav-item" v-if="!isAuthenticated">
            <router-link class="nav-link" to="/login">Giriş</router-link>
          </li>
          <li class="nav-item" v-if="!isAuthenticated">
            <router-link class="nav-link" to="/register">Qeydiyyat</router-link>
          </li>
          <li class="nav-item dropdown" v-else>
            <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown">
              Profil
            </a>
            <ul class="dropdown-menu dropdown-menu-end">
              <li><router-link class="dropdown-item" to="/profile">Mənim Profilim</router-link></li>
              <li><button class="dropdown-item" @click="logout">Çıxış</button></li>
            </ul>
          </li>
        </ul>
      </div>
    </nav>

    <!-- Content -->
    <main class="container mt-4">
      <slot />
    </main>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isAuthenticated: false, 
    };
  },
  methods: {
    logout() {
      // Çıxış əməliyyatı (token silmək və s.)
      this.isAuthenticated = false;
    },
  },
};
</script>

<style scoped>
.navbar-brand {
  font-size: 24px;
}
.nav-link {
  font-size: 16px;
  color: #4A90E2;
}
.nav-link:hover {
  color: #F5A623;
}
.dropdown-menu {
  min-width: 160px;
}
</style>
