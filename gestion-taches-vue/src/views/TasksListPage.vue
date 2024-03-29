<template>
  <div>
    <BackButton/>
  </div>
  <div>
    <TasksList/>
  </div>
  <div>
    <LogoutButton/>
  </div>
</template>

<script>
import LogoutButton from '@/components/LogoutButton.vue';
import TasksList from '@/components/TasksList.vue';
import BackButton from '@/components/BackButton.vue';

export default {
  components: {
    TasksList,
    LogoutButton,
    BackButton,
  },
};
</script>
