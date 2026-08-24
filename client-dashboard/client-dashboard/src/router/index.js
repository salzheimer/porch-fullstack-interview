import Vue from 'vue'
import VueRouter from 'vue-router'


Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'ClientDashboard',
    component: ()=>import('../views/ClientDashboard.vue')
  },
  {
    path: '/client/:id?',
    name: 'ClientManagement',
    component: () => import('../views/ClientManagement.vue')
  } 
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
