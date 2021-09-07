import Vue from 'vue'
import App from './App.vue'
import router from './router'

import axios from 'axios'
import VueAxios from 'vue-axios'
import Multiselect from "vue-multiselect";
import "vue-multiselect/dist/vue-multiselect.min.css";
import {
  BootstrapVue,
  IconsPlugin
} from 'bootstrap-vue'


import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import Enum from "./js/enums/typeEnum.js"
import Format from "./js/format/formatData.js"

import BaseInput from "@/components/BaseInput";
import BasePaginate from "@/components/table/BasePaginate";
import BasePopup from "@/components/BasePopup";
import BaseSpinner from "@/components/BaseSpinner";
import BaseTable from "@/components/table/BaseTable";
import BaseToastMessage from "@/components/BaseToastMsg";

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

Vue.config.productionTip = false

Vue.component(BaseInput.name, BaseInput);
Vue.component(BasePaginate.name, BasePaginate);
Vue.component(BasePopup.name, BasePopup);
Vue.component(BaseSpinner.name, BaseSpinner);
Vue.component(BaseTable.name, BaseTable);
Vue.component(BaseToastMessage.name, BaseToastMessage);

Vue.component('multiselect', Multiselect)

Vue.prototype.$enum = Enum;
Vue.prototype.$format = Format;

Vue.use(VueAxios, axios);

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')