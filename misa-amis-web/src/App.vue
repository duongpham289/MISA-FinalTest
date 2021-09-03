<template>
  <div class="wrapper" id="app" :class="{'menu-small': !bigMenu}">
    <Menu ref="Menu" :customData="menuData" @makeMenuBig="makeMenuBig" />
    <Main ref="Main" @makeMenuSmall="makeMenuSmall"/>
  </div>
</template>

<script>
import Menu from "./layout/TheMenu.vue";
import Main from './layout/TheMain.vue'
import { MenuData } from "./js/resources/MenuData";

export default {
  name: "App",
  components: {
    Main,
    Menu,
  },
  data() {
    return {
      bigMenu: true,
      overlayShow: false,
      menuData: MenuData,
    }
  },
  methods: {
    /**
     * Hàm thu nhỏ navbar
     * CreatedBy: PHDUONG (28/09/2021)
     */
    makeMenuSmall() {
      this.$refs.Menu.makeMenuSmall();
      this.bigMenu = false;
    },

    /**
     * Hàm thông báo cho component con mở rộng navbar
     * CreatedBy: PHDUONG (28/09/2021)
     */
    makeMenuBig() {
      this.$refs.Main.makeMenuBig();
      this.bigMenu = true;
    }
  }
};
</script>

<style>
@import url("./assets/css/base/Style.css");

.menu-small {
    --menu-width: 52px;
}

.wrapper {
    display: flex;
    width: 100%;
}
</style>
