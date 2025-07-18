import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import {createPinia} from 'pinia';

import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';

import { VueMaskDirective } from 'v-mask';

import notify from './assets/js/notify.js';
 
const app = createApp(App);
app.directive('mask', VueMaskDirective);
app.config.globalProperties.$notify = notify;
app.use(createPinia())
app.use(router).mount('#app');
