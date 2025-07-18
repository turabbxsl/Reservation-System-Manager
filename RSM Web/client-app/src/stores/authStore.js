import { defineStore } from 'pinia'
import axios from 'axios'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: JSON.parse(localStorage.getItem('user')) || null,
    token: localStorage.getItem('token') || null,
  }),
  getters: {
    isAuthenticated: (state) => !!state.token,
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
    }
  }
})
