import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/employees',
    name: 'Employees',
    component: () => import('../views/employees/MainContent.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  routes
})

export default router
