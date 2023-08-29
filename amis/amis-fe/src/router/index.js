import { createRouter, createWebHistory } from 'vue-router'
import HomeIndex from '@/views/home/HomeIndex.vue'
import EmployeesList from '@/views/employees/EmployeesList.vue'

const routes = [
  {
    path: "/",
    component: HomeIndex,
  },
  {
    path: "/home",
    component: HomeIndex,
  },
  {
    path: "/employees",
    component: EmployeesList,
  }
];
const router = createRouter({
  history: createWebHistory(),
  routes: routes,
});

export default router
