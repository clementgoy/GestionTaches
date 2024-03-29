<template>
  <div>
    <BackButton/>
  </div>
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
import BackButton from '@/components/BackButton.vue';

export default {
  components: {
    HolidaysList,
    LogoutButton,
    BackButton
  }
};
</script>

<style scoped>
/* Ajoutez vos styles ici */
</style>
