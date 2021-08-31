<template>
  <div class="wrapper" id="app" :class="{'menu-small': !bigMenu}">
    <Menu ref="Menu" :customData="menuData" @makeMenuBig="makeMenuBig" />
    <Main ref="Main" @makeMenuSmall="makeMenuSmall"/>
  </div>
</template>

<script>
import Menu from "./layout/TheMenu.vue";
import Main from './layout/TheMain.vue'

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
      menuData: [
        {
          iconClass: "mi-sidebar-dashboard",
          itemName: "Tổng quan",
          routerLink: "/employees",
        },
        {
          iconClass: "mi-sidebar-cash",
          itemName: "Tiền mặt",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-bank",
          itemName: "Tiền gửi",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-purchase",
          itemName: "Mua hàng",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-sale",
          itemName: "Bán hàng",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-invoice",
          itemName: "Quản lý hóa đơn",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-stock",
          itemName: "Kho",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-tools",
          itemName: "Công cụ dụng cụ",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-fixed-assets",
          itemName: "Tài sản cố định",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-tax",
          itemName: "Thuế",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-price",
          itemName: "Giá thành",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-general",
          itemName: "Tổng hợp",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-budget",
          itemName: "Ngân sách",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-report",
          itemName: "Báo cáo",
          routerLink: "",
        },
        {
          iconClass: "mi-sidebar-finance",
          itemName: "Phân tích tài chính",
          routerLink: "",
        },
      ],
    }
  },
  computed: {
    // customToast() {
    //   return this.$store.getters.toastData;
    // },
    // showLoader() {
    //   return this.$store.getters.showLoader;
    // }
  },
  watch: {
    // customToast: {
    //   deep: true,
    //   handler() {
    //     this.$refs.ToastMessage.showToast();
    //   }
    // }
  },
  methods: {
    /**
     * Hàm thu nhỏ navbar
     * NVTOAN 05/07/2021
     */
    makeMenuSmall() {
      this.$refs.Menu.makeMenuSmall();
      this.bigMenu = false;
    },

    /**
     * Hàm thông báo cho component con mở rộng navbar
     * NVTOAN 05/07/2021
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
/* @import url("./assets/css/common/reset.css");
@import url("./assets/css/common/global.css"); */

.menu-small {
    --menu-width: 52px;
}

.wrapper {
    display: flex;
    width: 100%;
}
</style>
