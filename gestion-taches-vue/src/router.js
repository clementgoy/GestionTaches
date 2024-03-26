import { createRouter, createWebHistory } from 'vue-router';
import Login from '@/views/LoginView.vue';
import ResetPassword from '@/views/ResetPassword.vue';
import RequestResetPage from '@/views/RequestResetPage.vue';
import HomePage from '@/views/HomePage.vue';
import TasksListPage from '@/views/TasksListPage.vue';
import HolidaysListPage from '@/views/HolidaysListPage.vue';
import TaskDetails from '@/views/TaskDetailsPage.vue';
import HolidayDetails from '@/views/HolidayDetailsPage.vue';

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
  { path: '/tasks', 
    name: 'Tasks', 
    component: TasksListPage 
  },
  { path: '/holidays', 
    name: 'Holidays', 
    component: HolidaysListPage 
  },
  { path: '/task/:id', 
    name: 'TaskDetails', 
    component: TaskDetails 
  },
  { path: '/holiday/:id', 
    name: 'HolidayDetails', 
    component: HolidayDetails 
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
