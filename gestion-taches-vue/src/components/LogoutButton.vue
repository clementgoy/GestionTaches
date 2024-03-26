<template>
  <button @click="logout">Se déconnecter</button>
</template>

<script>
export default {
  methods: {
    logout() {
      // Effacer les données de l'utilisateur stockées localement
      localStorage.removeItem('userEmail');
      // Rediriger vers la page de connexion
      this.$router.push('/login');
    }
  }
}
</script>
