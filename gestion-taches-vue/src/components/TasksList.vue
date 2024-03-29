<template>
  <div v-if="tasks.length > 0">
    <ul>
      <li v-for="task in tasks" :key="task.id"> 
        <router-link :to="`/task/${task.id}`">{{ task.description }}</router-link>
      </li>
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
      tasks: [],
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

      const response = await axios.get(`http://localhost:5138/api/Tasks/byEmployee/${employeeId}`, {
      headers: {
        'Authorization': `Bearer ${token}` // Inclut le token dans les en-têtes de la requête
      }
      });

      this.tasks = response.data.sort((a, b) => new Date(a.dueDate) - new Date(b.dueDate));
    } catch (error) {
      console.error('Erreur lors de la requête API', error);
    }
  },
};
</script>
