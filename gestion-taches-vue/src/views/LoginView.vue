<template>
  <div>
    <h2>Login</h2>
    <LoginForm @update:email="updateEmail" />
    <ForgotPasswordButton :email="email" />
  </div>
</template>

<script>
import LoginForm from '@/components/LoginForm.vue';
import ForgotPasswordButton from '@/components/ForgotPasswordButton.vue';

export default {
  components: {
    LoginForm,
    ForgotPasswordButton
  },
  data() {
    return {
      email: '',
    };
  },
  methods: {
    updateEmail(newEmail) {
      this.email = newEmail;
    }
  }
};
</script>
