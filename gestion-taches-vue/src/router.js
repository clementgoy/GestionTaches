import { createRouter, createWebHistory } from 'vue-router';
import Login from '@/views/LoginView.vue';
import ResetPassword from '@/views/ResetPassword.vue';
import RequestResetPage from '@/views/RequestResetPage.vue';
import HomePage from '@/views/HomePage.vue';
import ListeTaches from '@/views/ListeTachesView.vue';
import ListeCongesView from '@/views/ListeCongesView.vue';

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
    component: ResetPassword, 
  },
  {
    path: '/request-reset',
    name: 'RequestReset',
    component: RequestResetPage,
  },
  { path: '/home', 
    name: 'Home', 
    component: HomePage 
  },
  { path: '/taches', 
    name: 'Taches', 
    component: ListeTaches 
  },
  { path: '/conges', 
    name: 'Conges', 
    component: ListeCongesView 
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
