<!-- src/App.vue -->
<template>
  <div id="app">
    <ListeTaches />
  </div>
</template>

<script>
import ListeTaches from '@/components/ListeTaches.vue';


export default {
  name: 'App',
  components: {
    ListeTaches,
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>

