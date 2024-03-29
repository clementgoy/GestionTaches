<template>
  <form @submit.prevent="submitForm">
    <div>
      <label for="id">Id:</label>
      <input id="id" type="text" v-model="employee.id" placeholder="id de l'employé" required />
    </div>
    <div>
      <label for="name">Nom:</label>
      <input id="name" type="text" v-model="employee.name" placeholder="Nom de l'employé" required />
    </div>
    <div>
      <label for="firstname">Prenom:</label>
      <input id="firstname" type="text" v-model="employee.firstname" placeholder="Prenom de l'employé" required />
    </div>
    <div>
      <label for="email">Email:</label>
      <input id="email" type="email" v-model="employee.email" placeholder="Email de l'employé" required />
    </div>
    <div>
      <label for="password">Mot de passe:</label>
      <input id="password" type="password" v-model="employee.password" placeholder="Mot de passe" required />
    </div>
    <div>
      <label for="status">Status:</label>
      <input id="status" type="text" v-model="employee.status" placeholder="Statut de l'employé" required />
    </div>
    <div>
      <label for="pole">Pole:</label>
      <input id="pole" type="text" v-model="employee.pole" placeholder="Pole de l'employé" required />
    </div>
    <button type="submit">Ajouter</button>
  </form>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      employee: {
        id: '',
        name: '',
        firstname: '',
        email: '',
        password: '',
        status: '',
        pole: '',
      },
      errorMessage: '',
    };
  },
  methods: {
    async submitForm() {
      try {
        const token = localStorage.getItem('userToken');
    if (!token) {
      console.error("Jeton d'authentification non trouvé.");
      this.$router.push('/login');
    }

    await axios.post('http://localhost:5138/api/employees', this.employee, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
        this.$emit('employeeAdded');
        this.resetForm();
      } catch (error) {
        console.error("Erreur lors de la création de l'employé:", error);
        this.errorMessage = error.response?.data?.error || "Une erreur s'est produite lors de la création de l'employé.";
      }
    },
    resetForm() {
      this.employee = {
        id: '',
        name: '',
        firstname: '',
        email: '',
        password: '',
        status: '',
        pole: '',
      };
      this.errorMessage = '';
    },
  },
};
</script>
