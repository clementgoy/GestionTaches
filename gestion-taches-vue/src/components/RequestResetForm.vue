<template>
  <div class="request-reset-form">
    <form @submit.prevent="requestReset">
      <div>
        <label for="email">Email:</label>
        <input id="email" type="email" v-model="email" required placeholder="Votre email" />
      </div>
      <button type="submit">Recevoir mon email de réinitialisation</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      email: '',
    };
  },
  methods: {
    async requestReset() {
      if (!this.email) {
        alert("Veuillez entrer votre email.");
        return;
      }
      try {
        await axios.post('http://localhost:5138/api/forgetpassword/request-reset', { email: this.email });
        alert("Si cet email est associé à un compte, un lien de réinitialisation de mot de passe a été envoyé.");
      } catch (error) {
        console.error("Une erreur est survenue lors de la demande de réinitialisation du mot de passe.", error);
      }
    }
  }
};
</script>
