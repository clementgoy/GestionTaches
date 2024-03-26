<template>
  <div class="home-page">
    <h1>Bienvenue sur votre espace de travail</h1>
    <nav>
      <ul>
        <li><router-link to="/taches">Ma liste de tâches</router-link></li>
        <li><router-link to="/conges">Ma liste de congés</router-link></li>
        <li><router-link to="/contact-manager">Contacter mon manager</router-link></li>
      </ul>
    </nav>
    <LogoutButton />
  </div>
</template>

<script>
import LogoutButton from '@/components/LogoutButton' 

export default {
  name: 'HomePage',
  components: {
    LogoutButton
  }
};
</script>
