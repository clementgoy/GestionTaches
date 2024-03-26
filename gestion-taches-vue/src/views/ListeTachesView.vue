<template>
  <div>
    <ListeTaches />
  </div>
  <div>
    <LogoutButton/>
  </div>
</template>

<script>
import ListeTaches from '@/components/ListeTaches.vue';
import LogoutButton from '@/components/LogoutButton.vue';

export default {
  components: {
    ListeTaches,
    LogoutButton,
  },
};
</script>
