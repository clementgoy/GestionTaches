<template>
  <div class="edit-employee-form">
    <h2>Modifier un Employé</h2>
    <form @submit.prevent="submitForm">
      <div>
        <label for="name">Nom:</label>
        <input id="name" v-model="employee.name" required>
      </div>
      <div>
        <label for="firstname">Prénom:</label>
        <input id="firstname" v-model="employee.firstname" required>
      </div>
      <div>
        <label for="email">Email:</label>
        <input id="email" type="email" v-model="employee.email" required>
      </div>
      <div>
        <label for="status">Statut:</label>
        <select id="status" v-model="employee.status" required>
          <option value="Manager">Manager</option>
          <option value="TeamMember">TeamMember</option>
        </select>
      </div>
      <div>
        <label for="pole">Pole:</label>
        <select id="pole" v-model="employee.pole" required>
          <option value="Logistics">Logistics</option>
          <option value="Development">Development</option>
          <option value="Design">Design</option>
          <option value="Production">Production</option>
        </select>
      </div>
      <button type="submit">Mettre à jour</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  props: ['employeeId'],
  data() {
    return {
      employee: {
        id: this.employeeId,
        name: '',
        firstname: '',
        email: '',
        status: '',
        pole: ''
      },
    };
  },
  mounted() {
    this.fetchEmployee();
  },
  methods: {
    async fetchEmployee() {
      try {
        const token = localStorage.getItem('userToken');
        const response = await axios.get(`http://localhost:5138/api/employees/${this.employeeId}`, {
            headers: {
            'Authorization': `Bearer ${token}`
            }
        });
        this.employee = { ...response.data, id: this.employeeId };
      } catch (error) {
        console.error("Erreur lors de la récupération des informations de l'employé:", error);
      }
    },
    async submitForm() {
      try {
        const token = localStorage.getItem('userToken');
        await axios.put(`http://localhost:5138/api/employees/${this.employeeId}`, this.employee, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });
        this.$emit('employeeUpdated');
        this.$emit('closeForm');
      } catch (error) {
        console.error("Erreur lors de la mise à jour de l'employé:", error);
      }
    }
  },
};
</script>
