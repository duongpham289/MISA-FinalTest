<template>
  <div id="app">
   <Menu />
   <Header />
   <!-- <Content /> -->
  </div>
</template>

<script>
import Header from './components/layouts/TheHeader.vue'
import Menu from './components/layouts/TheMenu.vue'

export default {
  name: 'App',
  components: {
   Header,Menu
  }
}
</script>

<style>
  @import url('./assets/css/base/Style.css');
  @import url('./assets/fonts/fontawesome-5.15.1/css/all.min.css');
</style>
