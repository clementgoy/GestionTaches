import { createRouter, createWebHistory } from 'vue-router';
import Login from '@/views/LoginView.vue';
import ResetPassword from '@/views/ResetPassword.vue';
import RequestResetPage from '@/views/RequestResetPage.vue';
import HomePageMembre from '@/views/HomePageMembre.vue';
import TasksListPage from '@/views/TasksListPage.vue';
import HolidaysListPage from '@/views/HolidaysListPage.vue';
import TaskDetails from '@/views/TaskDetailsPage.vue';
import HolidayDetails from '@/views/HolidayDetailsPage.vue';
import HomePageManager from '@/views/HomePageManager.vue';
import EmployeesManager from '@/views/EmployeesManager.vue';
import HolidaysManager from '@/views/HolidaysManager.vue';
import TasksManager from '@/views/TasksManager.vue';
import AddEmployeeForm from '@/components/AddEmployeeForm.vue';
import EditEmployee from '@/components/EditEmployee.vue';

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
  { path: '/homepage-membre', 
    name: 'HomePageMemmbre', 
    component: HomePageMembre 
  },
  { path: '/homepage-manager', 
    name: 'HomePageManager', 
    component: HomePageManager 
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
  { path: '/employees-manager', 
    name: 'EmployeesManager', 
    component: EmployeesManager, 
  },
  { path: '/holidays-manager', 
    name: 'HolidaysManager', 
    component: HolidaysManager, 
  },
  { path: '/tasks-manager', 
    name: 'TasksManager', 
    component: TasksManager, 
  },
  { path: '/add-employee', 
    name: 'AddEmployeeForm', 
    component: AddEmployeeForm, 
  },
  { path: '/edit-employee', 
    name: 'EditEmployee', 
    component: EditEmployee, 
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
