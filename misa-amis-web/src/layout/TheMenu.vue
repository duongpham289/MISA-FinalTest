<template>
  <div class="menu-container" :class="{ 'expand': bigMenu }">
    <div class="logo-container">
      <div
        class="header-icon mi mi-24 mi-sidebar-three-stripes"
        v-show="!bigMenu"
        @click="makeMenuBig"
      ></div>
      <a href="#" v-show="bigMenu" class="menu-container-option"></a>
      <a href="#" v-show="bigMenu" class="logo"
        ><img
          src="../assets/img/Logo_Module_TiengViet_White.66947422.svg"
          class="img-icon"
      /></a>
    </div>
    <div class="menu-item-list scroller">
      <router-link
        v-for="(item, index) in customData"
        :key="index"
        :customData="item.itemName"
        :to="item.routerLink"
      >
        <div
          class="menu-item"
          :class="item.routerLink == $route.path ? 'active' : ''"
        >
          <div class="menu-item__icon">
            <div class="mi mi-24" :class="item.iconClass"></div>
          </div>
          <div class="menu-item__title" v-show="bigMenu">{{ item.itemName }}</div>
        </div>
      </router-link>
    </div>
  </div>
</template>

<script>
// import Tooltip from "../components/Tooltip.vue";
export default {
  components: {
    // Tooltip,
  },
  props: {
    customData: {
      type: Array,
      required: true,
    },
  },
  data() {
    return {
      bigMenu: true,
      currentHoverMenu: null,
    };
  },
  created() {},
  methods: {
    /**
     * Hàm mở rộng navbar
     * NVTOAN 05/07/2021
     */
    makeMenuBig() {
      this.bigMenu = true;
      this.$emit("makeMenuBig");
    },

    /**
     * Hàm thu nhỏ navbar
     * NVTOAN 05/07/2021
     */
    makeMenuSmall() {
      this.bigMenu = false;
    },
  },
};
</script>