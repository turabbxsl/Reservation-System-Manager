<template>
  <!-- Login 1 - Bootstrap Brain Component -->
  <div class="bg-light py-3 py-md-5">
    <div class="container">
      <div class="row justify-content-md-center">
        <div class="col-12 col-md-11 col-lg-8 col-xl-7 col-xxl-6">
          <div class="bg-white p-4 p-md-5 rounded shadow-sm">
            <form @submit.prevent="Login">
              <div class="row gy-3 gy-md-4 overflow-hidden">
                <div class="col-12">
                  <label for="email" class="form-label">Email <span class="text-danger">*</span></label>
                  <div class="input-group">
                    <span class="input-group-text">
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                        class="bi bi-envelope" viewBox="0 0 16 16">
                        <path
                          d="M0 4a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V4Zm2-1a1 1 0 0 0-1 1v.217l7 4.2 7-4.2V4a1 1 0 0 0-1-1H2Zm13 2.383-4.708 2.825L15 11.105V5.383Zm-.034 6.876-5.64-3.471L8 9.583l-1.326-.795-5.64 3.47A1 1 0 0 0 2 13h12a1 1 0 0 0 .966-.741ZM1 11.105l4.708-2.897L1 5.383v5.722Z" />
                      </svg>
                    </span>
                    <input type="email" v-model="loginModel.email" class="form-control" name="email" id="email"
                      required>
                  </div>
                </div>
                <div class="col-12">
                  <label for="password" class="form-label">Şifrə <span class="text-danger">*</span></label>
                  <div class="input-group">
                    <span class="input-group-text">
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                        class="bi bi-key" viewBox="0 0 16 16">
                        <path
                          d="M0 8a4 4 0 0 1 7.465-2H14a.5.5 0 0 1 .354.146l1.5 1.5a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0L13 9.207l-.646.647a.5.5 0 0 1-.708 0L11 9.207l-.646.647a.5.5 0 0 1-.708 0L9 9.207l-.646.647A.5.5 0 0 1 8 10h-.535A4 4 0 0 1 0 8zm4-3a3 3 0 1 0 2.712 4.285A.5.5 0 0 1 7.163 9h.63l.853-.854a.5.5 0 0 1 .708 0l.646.647.646-.647a.5.5 0 0 1 .708 0l.646.647.646-.647a.5.5 0 0 1 .708 0l.646.647.793-.793-1-1h-6.63a.5.5 0 0 1-.451-.285A3 3 0 0 0 4 5z" />
                        <path d="M4 8a1 1 0 1 1-2 0 1 1 0 0 1 2 0z" />
                      </svg>
                    </span>
                    <input type="password" v-model="loginModel.password" class="form-control" name="password"
                      id="password" value="" required>
                  </div>
                </div>
                <div class="col-12">
                  <div class="d-grid">
                    <button class="btn btn-primary btn-lg" type="submit">Giriş</button>
                  </div>
                </div>
              </div>
            </form>
            <div class="row">
              <div class="col-12">
                <hr class="mt-5 mb-4 border-secondary-subtle">
                <div class="d-flex gap-2 gap-md-4 flex-column flex-md-row justify-content-md-center">
                  <a href="#!" class="link-secondary text-decoration-none">
                    <router-link to="/register" class="nav-link">Yeni Hesab</router-link>
                  </a>
                  <a href="#!" class="link-secondary text-decoration-none">
                    <router-link to="/reset" class="nav-link">Şifrəni unutmuşam</router-link>
                  </a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { loginAccount } from '../services/accountService';
import { useAuthStore } from '../stores/authStore';

export default {
  data() {
    return {
      loginModel: {
        email: '',
        password: ''
      }
    }
  },
  methods: {
    async Login() {
      try {
        const res = await loginAccount(this.loginModel);
        console.log('Login response:', res);
        if (!res.isSuccess && Array.isArray(res.errors)) {
          res.errors.forEach((error) => {
            this.$notify('error', error, 'Giriş Xətası');
          });
        }
        else if (res.isSuccess && res.data) {
          const authStore = useAuthStore();
          authStore.login(res.data);

          this.$notify('success', 'Uğurlu Giriş');
          this.$router.push({ name: 'Profile' });
        }
      } catch (err) {
        console.log('catch unexpecting error', err);
      }
    }
  },
  name: "LoginPage"
}
</script>
