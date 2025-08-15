import { defineStore } from 'pinia'
import axios from 'axios'


export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: JSON.parse(localStorage.getItem('user')) || null,
    token: localStorage.getItem('token') || null,
  }),
  getters: {
    isAuthenticated: (state) => !!state.token,
    companyId: (state) => state.user?.companyId,
    role: (state) => state.user?.role,
    isAdmin: (state) => state.user?.role === 'CompanyAdmin',
    isUser: (state) => state.user?.role === 'Customer',
    isCompanyUser: (state) => state.user?.role === 'CompanyUser',
    isCompanySupervisor: (state) => state.user?.role === 'CompanySupervisor'
  },
  actions: {
    login(userData) {
      this.user = userData
      this.token = userData.token

      localStorage.setItem('user', JSON.stringify(userData))
      localStorage.setItem('token', userData.token)

      axios.defaults.headers.common['Authorization'] = `Bearer ${userData.token}`
    },
    logout() {
      this.user = null
      this.token = null

      localStorage.removeItem('user')
      localStorage.removeItem('token')

      delete axios.defaults.headers.common['Authorization']
    },
    updateUser(updatedUser) {
      this.user = updatedUser
      localStorage.setItem('user', JSON.stringify(updatedUser))
    }
  }
})
