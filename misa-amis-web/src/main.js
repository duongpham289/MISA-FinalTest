import Vue from 'vue'
import App from './App.vue'
import router from './router'

import axios from 'axios'
import VueAxios from 'vue-axios'

import BaseInput from "@/components/BaseInput";
import { BaseComboBox, BaseComboBoxOption } from "@/components/comboBox";
import {
  BaseTable,
  // BasePagination
} from "@/components/table";

Vue.config.productionTip = false

Vue.component(BaseInput.name, BaseInput);
Vue.component(BaseTable.name, BaseTable);

Vue.component(BaseComboBox.name, BaseComboBox);
Vue.component(BaseComboBoxOption.name, BaseComboBoxOption);

Vue.use(VueAxios, axios)
new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
