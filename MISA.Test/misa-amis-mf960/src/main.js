import Vue from 'vue'
import App from './App.vue'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'


// Import Bootstrap an BootstrapVue CSS files (order is important)
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import './assets/scss/app.scss'

Vue.config.productionTip = false

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

Vue.use(Vuetify)

const opts = {}

export default new Vuetify(opts)

new Vue({
  render: h => h(App),
  vuetify: new Vuetify()
}).$mount('#app')
