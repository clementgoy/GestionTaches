<template>
  <div>
    <h2>Réinitialisation du mot de passe</h2>
    <form @submit.prevent="resetPassword">
      <input v-model="password" type="password" placeholder="Nouveau mot de passe" required>
      <input v-model="confirmPassword" type="password" placeholder="Confirmer le mot de passe" required>
      <button type="submit">Réinitialiser le mot de passe</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      password: '',
      confirmPassword: '',
      token: this.$route.query.token // Récupère le token de l'URL
    };
  },
  methods: {
    async resetPassword() {
      if (this.password !== this.confirmPassword) {
        alert("Les mots de passe ne correspondent pas.");
        return;
      }
      try {
        await axios.post('http://localhost:5138/api/forgetpassword/reset-password', {
          token: this.token,
          newPassword: this.password
        });
        alert("Votre mot de passe a été réinitialisé avec succès.");
        this.$router.push('/login'); // Redirige vers la page de connexion après la réussite
      } catch (error) {
        console.error("Erreur lors de la réinitialisation du mot de passe.", error);
      }
    }
  }
};
</script>
