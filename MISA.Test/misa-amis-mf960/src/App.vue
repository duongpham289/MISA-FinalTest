<template>
  <div class="layout-main">
    <Menu />
    <div id="content-area">
      <div id="main-content" class="content-wrapper relative">
        <Header />
        <div class="router-view fix-view">
          <div class="router-content">
            <div class="content-area__content">
              <div class="layout-dictionary">
                <Content />
              </div>
            </div>
          </div>
        </div>
      </div>
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
