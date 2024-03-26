<template>
  <form @submit.prevent="login">
    <div>
      <label for="email">Email:</label>
      <input type="email" v-model="email" required />
    </div>
    <div>
      <label for="password">Password:</label>
      <input type="password" v-model="password" required />
    </div>
    <button type="submit">Login</button>
  </form>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      email: '',
      password: ''
    };
  },
  methods: {
    async login() {
      try {
        const response = await axios.post('http://localhost:5138/api/authentication/login', {
          email: this.email,
          password: this.password
        });

        // Supposons que la réponse inclut un token et l'ID de l'employé
        const { token, employeeId } = response.data;
        if (token && employeeId) {
          localStorage.setItem('userToken', token);
          localStorage.setItem('employeeId', employeeId); // Stocke l'ID de l'employé
          this.$router.push('/home');
        } else {
          console.error("Réponse inattendue du serveur");
        }
      } catch (error) {
        console.error('Erreur de connexion:', error.response.data);
      }
    }
  }
};
</script>

