<template>
  <div>
    <BackButton/>
  </div>
  <div class="home-page">
    <h1>Bienvenue sur votre espace de travail</h1>
    <nav>
      <ul>
        <li><router-link to="/tasks">Ma liste de tâches</router-link></li>
        <li><router-link to="/holidays">Ma liste de congés</router-link></li>
        <li><router-link to="/contact-manager">Contacter mon manager</router-link></li>
      </ul>
    </nav>
    <LogoutButton />
  </div>
</template>

<script>
import LogoutButton from '@/components/LogoutButton';
import BackButton from '@/components/BackButton.vue';

export default {
  name: 'HomePageMembre',
  components: {
    LogoutButton,
    BackButton,
  }
};
</script>
