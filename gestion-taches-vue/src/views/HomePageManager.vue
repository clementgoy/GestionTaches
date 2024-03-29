<template>
  <div>
    <BackButton/>
  </div>
  <div>
    <h1>Bienvenue {{ userName }},</h1>
    <div class="options">
      <h2>Actions disponibles :</h2>
      <ul>
        <li>
          <router-link to="/employees-manager">Gérer les employés</router-link>
        </li>
        <li>
          <router-link to="/tasks">Gérer les tâches</router-link>
        </li>
        <li>
          <router-link to="/holidays">Gérer les congés</router-link>
        </li>
      </ul>
    </div>
    <LogoutButton />
  </div>
</template>

<script>
import axios from 'axios';
import LogoutButton from '@/components/LogoutButton.vue';
import BackButton from '@/components/BackButton.vue';

export default {
    components: {
    LogoutButton, 
    BackButton
  },
  data() {
    return {
      userName: '',
    };
  },
  mounted() {
    this.getUserName();
  },
  methods: {
    async getUserName() {
      const employeeId = localStorage.getItem('employeeId');
      const token = localStorage.getItem('userToken');
      
      if (employeeId && token) {
        try {
          const response = await axios.get(`http://localhost:5138/api/employees/${employeeId}`, {
            headers: {
              'Authorization': `Bearer ${token}`
            }
          });
          this.userName = `${response.data.firstName} ${response.data.name}`;
        } catch (error) {
            console.error("Erreur lors de la récupération du nom de l'utilisateur:", error);
            this.$router.push('/login');
        }
      }
    },
  },
};

</script>
