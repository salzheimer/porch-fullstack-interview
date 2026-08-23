<template>
  <div class="space-y-6">
    <h1 class="text-xl font-bold text-gray-800 mb-6">Client Management</h1>
    <ClientForm mode="edit" :client-id="Number($route.params.id)" />
  </div>
</template>

<script>
import ClientForm from "@/components/ClientForm.vue";

export default {
  name: "ClientManagementView",
  components: {
    ClientForm,
  },
};
</script>
