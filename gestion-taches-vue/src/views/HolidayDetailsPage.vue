<!-- Composant HolidayDetails.vue -->
<template>
  <div>
    <div v-if="holiday">
      <h2>{{ holiday.reason }}</h2>
      <p>Duration : {{ holiday.duration }} Hours</p>
      <p>Date : {{ new Date(holiday.date).toLocaleDateString() }}</p>
    </div>
    <div v-else>
      Chargement des détails de la tâche...
    </div>
  </div>
</template>


<script>
import axios from 'axios';

export default {
  data() {
    return {
      holiday: null, 
    };
  },
  async mounted() {
    const id = this.$route.params.id;
    const token = localStorage.getItem('userToken');
    
    if (!token) {
      console.error('Token JWT non trouvé ou expiré');
      this.$router.push('/login');
      return;
    }
    
    try {
      const response = await axios.get(`http://localhost:5138/api/Holiday/Holiday/${id}`, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });
      this.holiday = response.data;
    } catch (error) {
      console.error('Erreur lors de la récupération des détails du congé:', error);
    }
  },
};
</script>
