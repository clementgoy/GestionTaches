import { createRouter, createWebHistory } from 'vue-router';
import Login from '@/views/LoginView.vue';
import ResetPassword from '@/views/ResetPassword.vue';
import RequestResetPage from '@/views/RequestResetPage.vue';

const routes = [
  {
    path: '/',
    redirect: '/login',
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
  {
    path: '/reset-password',
    name: 'ResetPassword',
    component: ResetPassword, // Assurez-vous que le chemin d'accès à votre composant est correct
  },
  {
    path: '/request-reset',
    name: 'RequestReset',
    component: RequestResetPage,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
