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
        console.log("Données envoyées au serveur :", {
          courriel: this.email,
          motDePasse: this.password
        });

        const response = await axios.post('http://localhost:5138/api/authentification/login', {
          email: this.email,
          password: this.password
        });

        console.log("Réponse du serveur :", response.data);
      } catch (error) {
        console.error('Erreur de connexion:', error);
      }
    }
  }
};
</script>