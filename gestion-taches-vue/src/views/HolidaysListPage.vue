<template>
  <div class="holidays-list">
    <h1>Liste des Congés</h1>
    <HolidaysList />
  </div>
  <div>
    <LogoutButton/>
  </div>
</template>

<script>
import HolidaysList from '@/components/HolidaysList.vue';
import LogoutButton from '@/components/LogoutButton';

export default {
  components: {
    HolidaysList,
    LogoutButton,
  }
};
</script>

<style scoped>
/* Ajoutez vos styles ici */
</style>
