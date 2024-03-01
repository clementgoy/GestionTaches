<template>
  <div>
    <h2>Demande de réinitialisation du mot de passe</h2>
    <RequestResetForm />
  </div>
</template>

<script>
import RequestResetForm from '@/components/RequestResetForm.vue';

export default {
  components: {
    RequestResetForm,
  },
};
</script>
