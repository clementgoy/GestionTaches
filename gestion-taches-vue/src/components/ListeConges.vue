<script>
import axios from 'axios';

export default {
  data() {
    return {
      conges: [],
    };
  },
  props: {
    emailEmploye: String,
  },
  async mounted() {
    try {
      const employeeId = localStorage.getItem('employeeId');
      
      if (!employeeId) {
        throw new Error('employeeId non trouvé');
      }

      const response = await axios.get(`http://localhost:5138/api/holiday/byEmploye/${employeeId}`);
      this.conges = response.data;
    } catch (error) {
      console.error('Erreur lors de la requête API', error);
    }
  },
};
</script>
