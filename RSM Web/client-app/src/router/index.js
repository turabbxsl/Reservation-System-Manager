import { createRouter, createWebHistory } from 'vue-router'
import MainLayout from '@/layouts/MainLayout.vue'
import DashboardLayout from '@/layouts/DashboardLayout.vue'
import HomePage from '@/pages/HomePage.vue'
import LoginPage from '@/pages/LoginPage.vue'
import RegisterPage from '@/pages/RegisterPage.vue'
import ProfilePage from '@/pages/ProfilePage.vue'
import { useAuthStore } from '@/stores/authStore'
import ReservePage from '@/pages/ReservePage.vue'
import DashReservations from '@/pages/dashboard/DashReservations.vue'
import DashboardHome from '@/pages/dashboard/DashboardHome.vue'
import DashServices from '@/pages/dashboard/DashServices.vue'
import DashUsers from '@/pages/dashboard/DashUsers.vue'

const routes = [
  {
    path: '/',
    component: MainLayout,
    children: [
      { path: '', name: 'Home', component: HomePage },
      { path: 'login', name: 'Login', component: LoginPage },
      { path: 'register', name: 'Register', component: RegisterPage },
      {
        path: 'profile',
        name: 'Profile',
        component: ProfilePage,
        meta: { requiresAuth: true }
      },
      {
        path: 'reserve',
        name: 'Reserve',
        component: ReservePage,
        meta: { requiresAuth: true }
      }
    ]
  },
  {
    path: '/dashboard',
    component: DashboardLayout,
    redirect: '/dashboard/home',
    meta: { requiresAuth: true },
    children: [
      { path: 'home', name: 'DashboardHome', component: DashboardHome, meta: { requiresAuth: true } },
      { path: 'reservations', name: 'DashReservation', component: DashReservations, meta: { requiresAuth: true } },
      { path: 'services', name: 'DashServices', component: DashServices, meta: { requiresAuth: true } },
      { path: 'users', name: 'DashUsers', component: DashUsers, meta: { requiresAuth: true } },
    ],
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next({ name: 'Login' })
  } else {
    next()
  }
})

export default router
