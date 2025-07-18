<template>
  <div class="d-flex flex-column min-vh-100">
    <AppNavbar />

    <main class="flex-grow-1">
      <router-view />
    </main>

    <AppFooter />
  </div>
</template>

<script>
import AppNavbar from "@/components/layout/AppNavbar.vue";
import AppFooter from "@/components/layout/AppFooter.vue";

export default {
  name: "MainLayout",
  components: {
    AppNavbar,
    AppFooter,
  }
};
</script>

<style scoped>

</style>
