<template>
  <div v-if="holidays.length > 0">
    <ul>
      <li v-for="holiday in holidays" :key="holiday.id"> 
        <router-link :to="`/holiday/${holiday.id}`">{{ holiday.reason }}</router-link>
      </li>
    </ul>
  </div>
  <div v-else>
    Aucun congé trouvée.
  </div>
</template>


<script>
import axios from 'axios';

export default {
  data() {
    return {
      holidays: [],
    };
  },
  async mounted() {
    try {
      const employeeId = localStorage.getItem('employeeId');
      const token = localStorage.getItem('userToken');
      
      if (!token) {
        console.error('Token JWT non trouvé ou expiré');
        this.$router.push('/login'); // Redirige l'utilisateur vers la page de connexion
      }

      if (!employeeId) {
        throw new Error('employeeId non trouvé');
      }

      const response = await axios.get(`http://localhost:5138/api/Holiday/byEmployee/${employeeId}`, {
      headers: {
        'Authorization': `Bearer ${token}` // Inclut le token dans les en-têtes de la requête
      }
      });
      console.log(response.data); // Après avoir assigné les données à `this.taches`

    this.holiday = response.data.sort((a, b) => new Date(a.date) - new Date(b.date));
    } catch (error) {
      console.error('Erreur lors de la requête API', error);
    }
  },
};
</script>
