<template>
  <div class="header-container items-center d-flex">
    <div class="header-left items-center d-flex">
      <div
        class="header-icon mi mi-24 mi-three-stripes"
        @click="makeMenuSmall"
        v-show="bigMenu"
      ></div>
      <div class="header-branch-name pointer">CÔNG TY CỔ PHẦN MISA</div>
      <a class="flex items-center"
        ><div
          class="
            header-icon
            mi mi-14 mi-chevron-right
            header-branch-icon
            pointer
          "
        ></div
      ></a>
    </div>
    <div class="header-right d-flex items-center">
      <div class="pointer header-icon">
        <div class="header-notify header-menu-icon-simple">
          <div class="mi mi-24 mi-bell__nav wiggle"></div>
        </div>
      </div>

      <div class="dropdown-user d-flex items-center pointer">
        <div class="user-ava">
          <div class="mi mi-32 mi-user-avatar avatar"></div>
        </div>
        <div class="display-user">Phạm Hải Dương</div>
        <div class="mi mi-14 mi-chevron-down pointer"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Header",
  data() {
    return {
      bigMenu: true,
    };
  },
  methods: {
    /**
     * Hàm gọi cha để thu nhỏ navbar
     * NVTOAN 05/07/2021
     */
    makeMenuSmall() {
      this.$emit("makeMenuSmall");
      this.bigMenu = false;
    },

    /**
     * Hàm hiện icon khi navbar to
     * NVTOAN 05/07/2021
     */
    makeMenuBig() {
      this.bigMenu = true;
    },
  },
};
</script>