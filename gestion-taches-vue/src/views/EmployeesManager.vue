<template>
    <div>
      <BackButton/>
    </div>
  <div>
    <h1>Gestion des Employés</h1>
    <div>
      <button @click="showAddEmployeeForm = true">Ajouter un Employé</button>
    </div>
    <div v-if="showAddEmployeeForm">
      <AddEmployeeForm @employeeAdded="fetchEmployees"/>
    </div>
    <div>
      <h2>Liste des Employés</h2>
      <ul>
        <li v-for="employee in employees" :key="employee.id">
          {{ employee.name }} - {{ employee.email }}
          <button @click="editEmployee(employee.id)">Modifier</button>
          <button @click="deleteEmployee(employee.id)">Supprimer</button>
        </li>
      </ul>
    </div>
  </div>
  <div>
      <LogoutButton/>
    </div>
</template>

<script>
import axios from 'axios';
import AddEmployeeForm from '@/components/AddEmployeeForm.vue'; 
import BackButton from '@/components/BackButton.vue';
import LogoutButton from '@/components/LogoutButton.vue';

export default {
  name: 'EmployeesManager',
  components: {
    AddEmployeeForm,
    BackButton,
    LogoutButton,
  },
  data() {
    return {
      employees: [],
      showAddEmployeeForm: false,
    };
  },
  mounted() {
    this.fetchEmployees();
  },
  methods: {
    async fetchEmployees() {
      try {
        const token = localStorage.getItem('userToken');
      const response = await axios.get('http://localhost:5138/api/employees', {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });
        this.employees = response.data;
      } catch (error) {
        console.error("Erreur lors de la récupération des employés:", error);
      }
    },
    editEmployee(id) {
      this.$router.push({ name: 'EditEmployee', params: { id } });
    },
    async deleteEmployee(id) {
      try {
        const token = localStorage.getItem('userToken');
        await axios.delete(`http://localhost:5138/api/employees/${id}`, {
          headers: {
            'Authorization': `Bearer ${token}`
          }
        });
        this.fetchEmployees(); 
      } catch (error) {
        console.error("Erreur lors de la suppression de l'employé:", error);
      }
    },
  },
};
</script>

