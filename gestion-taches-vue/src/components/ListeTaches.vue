<!-- src/components/ListeTaches.vue -->
<template>
  <div>
    <h1>Liste des Tâches</h1>
    <ul>
      <li v-for="tache in taches" :key="tache.id">{{ tache.description }}</li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      taches: [],
    };
  },
  mounted() {
    axios.get('/api/taches')
      .then(response => {
        this.taches = response.data;
      })
      .catch(error => {
        console.error('Erreur lors de la requête API', error);
      });
  },
};
</script>

<style scoped>
/* Styles spécifiques à ce composant ici */
</style>
