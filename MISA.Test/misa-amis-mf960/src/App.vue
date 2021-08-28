<template>
  <div class="layout-main">
    <Menu />
    <div class="main-content">
      <Header />
      <Content />
    </div>
  </div>
</template>

<script>
import Content from "./components/layouts/TheContent.vue";

import Header from "./components/layouts/TheHeader.vue";
import Menu from "./components/layouts/TheMenu.vue";

export default {
  name: "App",
  components: {
    Content,
    Menu,
    Header,
  },
};
</script>

<style>
@import url("./assets/css/base/Style.css");
@import url("./assets/fonts/fontawesome-5.15.1/css/all.min.css");
</style>
