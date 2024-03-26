<template>
  <div class="list-conges">
    <h1>Liste des Congés</h1>
    <ListeConges />
  </div>
  <div>
    <LogoutButton/>
  </div>
</template>

<script>
import ListeConges from '@/components/ListeConges.vue';
import LogoutButton from '@/components/LogoutButton';

export default {
  components: {
    ListeConges,
    LogoutButton,
  }
};
</script>

<style scoped>
/* Ajoutez vos styles ici */
</style>
