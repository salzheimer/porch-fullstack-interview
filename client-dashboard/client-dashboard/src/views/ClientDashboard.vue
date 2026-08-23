<template>
  <div
    class="max-w-5xl mx-auto bg-white shadow-md rounded-lg overflow-hidden border border-gray-200"
  >
    <div class="space-y-6">
      <h1 class="text-xl font-bold text-gray-800 mb-6">Client Dashboard</h1>
    </div>
    <!-- Header / Actions Bar -->
    <div
      class="px-6 py-4 border-b border-gray-200 flex justify-between items-center bg-gray-50"
    >
      <h2 class="text-xl font-bold text-gray-800">Clients</h2>
      <button
        @click="isModalOpen = true"
        class="px-4 py-2 bg-indigo-600 hover:bg-indigo-700 text-white text-sm font-medium rounded-md shadow-sm focus:outline-none"
      >
        + Add New Client
      </button>
    </div>

    <!-- Empty State -->
    <div v-if="clients.length === 0" class="text-center py-12 px-4">
      <p class="text-gray-500 text-sm mb-4">No clients found.</p>
      <button
        @click="isModalOpen = true"
        class="text-indigo-600 hover:text-indigo-800 font-medium text-sm"
      >
        Get started by adding your first client
      </button>
    </div>

    <!-- Client Table -->
    <div v-else class="overflow-x-auto">
      <table class="min-w-full divide-y divide-gray-200">
        <thead class="bg-gray-50">
          <tr>
            <th
              scope="col"
              class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
            >
              First Name
            </th>
            <th
              scope="col"
              class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
            >
              Last Name
            </th>
            <th
              scope="col"
              class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
            >
              Email
            </th>
            <th
              scope="col"
              class="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider"
            >
              Actions
            </th>
          </tr>
        </thead>
        <tbody class="bg-white divide-y divide-gray-200">
          <tr
            v-for="client in clients"
            :key="client.clientId"
            class="hover:bg-gray-50 transition-colors"
          >
            <td
              class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900"
            >
              {{ client.firstName }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
              {{ client.lastName }}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
              {{ client.email }}
            </td>
            <td
              class="px-6 py-4 whitespace-nowrap text-right text-sm font-medium"
            >
              <router-link
                :to="{ path: `/client/${client.clientId}` }"
                class="text-indigo-600 hover:text-indigo-900 bg-indigo-50 hover:bg-indigo-100 px-3 py-1 rounded-md transition-colors"
              >
                View Details &rarr;
              </router-link>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Modal Popup Component -->
    <div
      v-if="isModalOpen"
      class="fixed inset-0 z-50 overflow-y-auto bg-gray-900 bg-opacity-50 flex items-center justify-center p-4"
    >
      <div
        class="bg-white rounded-lg shadow-xl max-w-2xl w-full max-h-[90vh] overflow-y-auto"
      >
        <!-- Modal Content Container -->
        <div class="p-6 relative">
          <!-- Close 'X' Button -->
          <button
            @click="isModalOpen = false"
            class="absolute top-4 right-4 text-gray-400 hover:text-gray-600 text-xl font-bold"
          >
            &times;
          </button>

          <!-- Embedded Add Client Form Component -->
          <ClientForm
            mode="create"
            @client-created="handleClientCreated"
            @cancel="isModalOpen = false"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import apiClient from "@/api";
import ClientForm from "@/components/ClientForm.vue";

export default {
  name: "ClientList",
  components: {
    ClientForm,
  },
  data() {
    return {
      clients: [],
      isModalOpen: false,
    };
  },
  mounted() {
    this.loadClients();
  },
  methods: {
    loadClients() {
      apiClient
        .get("api/Clients/active")
        .then((response) => {
          this.clients = response.data || [];
        })
        .catch((error) => {
          console.error("Failed to load clients:", error);
        });
    },
    handleClientCreated(client) {
      this.clients.unshift(client);
      this.isModalOpen = false;
    },
  },
};
</script>
