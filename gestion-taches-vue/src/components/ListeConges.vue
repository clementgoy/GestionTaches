<template>
  <div v-if="conges.length > 0">
    <ul>
      <li v-for="conge in conges" :key="conge.id">{{ conge.reason }}</li>
    </ul>
  </div>
  <div v-else>
    Aucun congé trouvé.
  </div>
</template>


<script>
import axios from 'axios';

export default {
  data() {
    return {
      conges: [],
    };
  },
  async mounted() {
    try {
      const employeeId = localStorage.getItem('employeeId');
      const token = localStorage.getItem('userToken');
      
      if (!employeeId) {
        throw new Error('employeeId non trouvé');
      }

      const response = await axios.get(`http://localhost:5138/api/Holiday/byEmployee/${employeeId}`, {
      headers: {
        'Authorization': `Bearer ${token}` // Inclut le token dans les en-têtes de la requête
      }
      });
      console.log(response.data); // Après avoir assigné les données à `this.taches`

      this.conges = response.data;
    } catch (error) {
      console.error('Erreur lors de la requête API', error);
    }
  },
};
</script>
