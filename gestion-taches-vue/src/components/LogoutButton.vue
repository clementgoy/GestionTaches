<template>
  <button @click="logout">Se déconnecter</button>
</template>

<script>
export default {
  methods: {
    logout() {
      // Effacer les données de l'utilisateur stockées localement
      localStorage.removeItem('userEmail');
      localStorage.removeItem('userToken');
      localStorage.removeItem('employeeId');
      // Rediriger vers la page de connexion
      this.$router.push('/login');
    }
  }
}
</script>
