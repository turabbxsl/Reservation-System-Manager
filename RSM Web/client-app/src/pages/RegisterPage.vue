<template>
    <div>
        <h2>Giriş et</h2>
        <form @submit.prevent="login">
            <input v-model="email" placeholder="Email" />
            <input v-model="password" type="password" placeholder="Şifrə" />
            <button type="submit">Daxil ol</button>
        </form>
    </div>
</template>

<script>
import api from '../services/api'

export default {
    data() {
        return {
            email: '',
            password: ''
        }
    },
    methods: {
        async login() {
            try {
                const response = await api.post('/auth/login', {
                    email: this.email,
                    password: this.password
                })
                console.log('Login successful:', response.data)
            } catch (error) {
                console.error('Xəta:', error)
            }
        }
    }

}


</script>