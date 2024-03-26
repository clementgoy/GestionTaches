<template>
  <div>
    <TasksList />
  </div>
  <div>
    <LogoutButton/>
  </div>
</template>

<script>
import LogoutButton from '@/components/LogoutButton.vue';
import TasksList from '@/components/TasksList.vue';

export default {
  components: {
    TasksList,
    LogoutButton,
  },
};
</script>
