<template>
  <div class="back-button" @click="goBack">
    ← Retour
  </div>
</template>

<script>
export default {
  methods: {
    goBack() {
      this.$router.go(-1); 
    },
  },
};
</script>

<style>
.back-button {
  position: fixed;
  top: 20px;
  left: 20px;
  cursor: pointer;
  padding: 10px;
  background-color: #f0f0f0;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  font-weight: bold;
}
.back-button:hover {
  background-color: #e2e2e2;
}
</style>
