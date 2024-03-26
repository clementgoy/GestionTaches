<template>
  <div v-if="taches.length > 0">
    <ul>
      <li v-for="tache in taches" :key="tache.id">{{ tache.description }}</li>
    </ul>
  </div>
  <div v-else>
    Aucune tâche trouvée.
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
  async mounted() {
    try {
      const employeeId = localStorage.getItem('employeeId');
      const token = localStorage.getItem('userToken');
      
      if (!employeeId) {
        throw new Error('employeeId non trouvé');
      }

      const response = await axios.get(`http://localhost:5138/api/Tasks/byEmployee/${employeeId}`, {
      headers: {
        'Authorization': `Bearer ${token}` // Inclut le token dans les en-têtes de la requête
      }
      });
      console.log(response.data); // Après avoir assigné les données à `this.taches`

      this.taches = response.data;
    } catch (error) {
      console.error('Erreur lors de la requête API', error);
    }
  },
};
</script>
