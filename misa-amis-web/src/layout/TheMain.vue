<template>
  <div class="main">
    <Header ref="Header" @makeMenuSmall="makeMenuSmall" />
    <Content />
  </div>
</template>

<script>
import Header from "./main/TheHeader.vue";
import Content from "./main/TheContent.vue";

export default {
  components: {
    Header,
    Content,
  },
  methods: {
    /**
     * Hàm gọi cha để thu nhỏ navbar
     * CeatedBy: PHDUONG(29/08/2021)
     */
    makeMenuSmall() {
      this.$emit("makeMenuSmall");
    },

    /**
     * Hàm thông báo header mở rộng navbar
     * CeatedBy: PHDUONG(29/08/2021)
     */
    makeMenuBig() {
      this.$refs.Header.makeMenuBig("makeMenuBig");
    },
  },
};
</script>

<style>
.main {
  width: calc(100% - var(--menu-width));
  height: 100vh;
}
</style>