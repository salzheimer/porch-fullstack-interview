<template>
  <div
    class="max-w-2xl mx-auto bg-white shadow-md rounded-lg p-6 border border-gray-200"
  >
    <h2 class="text-xl font-bold text-gray-800 mb-6">
      {{ mode === "edit" ? "Client" : "Add New Client" }}
    </h2>

    <form @submit.prevent="submitForm" class="space-y-6">
      <!-- Basic Information -->
      <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">
            First Name
            <span class="text-red-500 ml-1">*</span>
          </label>
          <input
            type="text"
            v-model="form.firstName"
            required
            class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 text-sm"
            placeholder="John"
          />
        </div>
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">
            Last Name
            <span class="text-red-500 ml-1">*</span>
          </label>
          <input
            type="text"
            v-model="form.lastName"
            required
            class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 text-sm"
            placeholder="Doe"
          />
        </div>
      </div>

      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">
          Email Address
          <span class="text-red-500 ml-1">*</span>
        </label>
        <input
          type="email"
          v-model="form.email"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 text-sm"
          placeholder="john.doe@example.com"
        />
      </div>

      <hr class="border-gray-200 my-4" />

      <!-- Phone Numbers Section -->
      <div>
        <div class="flex justify-between items-center mb-3">
          <label class="block text-sm font-medium text-gray-700">
            Phone Numbers
          </label>
          <button
            type="button"
            @click="addPhoneField"
            class="inline-flex items-center px-3 py-1 border border-transparent text-xs font-medium rounded text-indigo-700 bg-indigo-100 hover:bg-indigo-200 focus:outline-none"
          >
            + Add Phone
          </button>
        </div>

        <div
          v-if="form.phones.length === 0"
          class="text-sm text-gray-500 italic mb-2"
        >
          No phone numbers added yet. Click "+ Add Phone" to include one.
        </div>

        <div
          v-for="(phone, index) in form.phones"
          :key="index"
          class="flex flex-col sm:flex-row items-start sm:items-center gap-2 bg-gray-50 p-3 rounded-md mb-3 border border-gray-200"
        >
          <!-- Country Code -->
          <div class="w-full sm:w-24">
            <label class="block text-sm font-medium text-gray-700 mb-1">
              Ctry Code
              <span class="text-red-500 ml-1">*</span>
            </label>
            <input
              type="number"
              v-model="phone.countryCode"
              required
              placeholder="1"
              class="w-full px-2 py-1.5 border border-gray-300 rounded text-sm bg-white"
            />
          </div>

          <!-- Phone Number -->
          <div class="flex-1 w-full">
            <label class="block text-sm font-medium text-gray-700 mb-1">
              Phone Number
              <span class="text-red-500 ml-1">*</span>
            </label>
            <input
              type="tel"
              v-model="phone.number"
              required
              placeholder="555-0199"
              class="w-full px-2 py-1.5 border border-gray-300 rounded text-sm bg-white"
            />
          </div>

          <!-- Phone Type Selector -->
          <div class="w-full sm:w-32">
            <label class="block text-sm font-medium text-gray-700 mb-1"
              >Phone Type
              <span class="text-red-500 ml-1">*</span>
            </label>
            <select
              v-model="phone.type"
              required
              class="w-full px-2 py-1.5 border border-gray-300 rounded text-sm bg-white"
            >
              <option value="">Select type</option>
              <option
                v-for="type in phonenumbertypes"
                :value="type.id"
                :key="type.id"
              >
                {{ type.displayName }}
              </option>
            </select>
          </div>

          <!-- Primary Checkbox -->
          <div class="flex items-center space-x-1 py-1">
            <label class="block text-sm font-medium text-gray-700 mb-1"></label>
            <input
              type="checkbox"
              :id="'primary-' + index"
              :checked="phone.isPrimary"
              @change="setPrimaryPhone(index)"
              class="h-4 w-4 text-indigo-600 focus:ring-indigo-500 border-gray-300 rounded"
            />
            <label
              :for="'primary-' + index"
              class="text-xs text-gray-600 whitespace-nowrap"
              >Primary</label
            >
          </div>

          <!-- Remove Button -->
          <button
            type="button"
            @click="removePhoneField(index, phone.id)"
            class="text-red-500 hover:text-red-700 p-1 text-sm font-bold ml-auto sm:ml-0"
            title="Remove phone"
          >
            &times;
          </button>
        </div>
      </div>

      <!-- Form Actions -->
      <div class="flex justify-end space-x-3 pt-4">
        <button
          type="button"
          @click="handleCancel"
          class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none"
        >
          Cancel
        </button>
        <button
          type="submit"
          class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none"
        >
          Save Client
        </button>
        <button
          v-if="mode === 'edit'"
          type="button"
          @click="handleArchive"
          class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-red-600 hover:bg-indigo-700 focus:outline-none"
        >
          Archive Client
        </button>
      </div>
    </form>
  </div>
</template>

<script>
import apiClient from "@/api";

export default {
  name: "ClientForm",
  props: {
    mode: {
      type: String,
      default: "create",
    },
    clientId: {
      type: [Number, String],
      default: null,
    },
  },
  data() {
    return {
      phonenumbertypes: [],
      form: {
        firstName: "",
        lastName: "",
        email: "",
        phones: [
          { countryCode: "+1", number: "", type: null, isPrimary: true },
        ],
      },
      isLoading: false,
    };
  },
  created() {
    this.getPhoneNumberTypes();
    if (this.mode === "edit" && this.clientId) {
      this.loadClientForEdit(this.clientId);
    }
  },
  methods: {
    getPhoneNumberTypes() {
      apiClient
        .get("api/PhoneNumber/types")
        .then((response) => {
          this.phonenumbertypes = response.data || [];
          if (this.phonenumbertypes.length && this.form.phones.length) {
            const firstPhone = this.form.phones[0];
            if (firstPhone.type === null || firstPhone.type === "") {
              firstPhone.type = this.phonenumbertypes[0].id;
            }
          }
        })
        .catch((error) => {
          console.error("api error", error);
        });
    },
    addPhoneField() {
      // If it's the very first phone being added, make it primary automatically
      const isFirst = this.form.phones.length === 0;
      this.form.phones.push({
        countryCode: "+1",
        number: "",
        type: this.phonenumbertypes.length ? this.phonenumbertypes[0].id : null,
        isPrimary: isFirst,
      });
    },
    removePhoneField(index, phoneNumberId) {
      const wasPrimary = this.form.phones[index].isPrimary;

      this.form.phones.splice(index, 1);

      // If we removed the primary number and there are still numbers left, make the first one primary
      if (wasPrimary && this.form.phones.length > 0) {
        this.form.phones[0].isPrimary = true;
      }
      if (this.mode === "edit" && phoneNumberId != undefined) {
        apiClient
          .delete(`api/PhoneNumber/${phoneNumberId}`)
          .then((response) => {
            console.log(response.data);
          })
          .catch((error) => {
            console.error(
              "remove phone number failed:",
              error.response?.data || error,
            );
          });
      }
    },
    setPrimaryPhone(selectedIndex) {
      // Enforce radio-button behavior (only one primary at a time)
      this.form.phones.forEach((phone, index) => {
        phone.isPrimary = index === selectedIndex;
      });
    },
    handleCancel() {
      if (this.mode === "edit") {
        this.$router.push({ name: "ClientDashboard" });
        return;
      }
      this.resetForm();
    },
    handleArchive() {
      // this.updateClient(true);
      apiClient
        .post(`api/Clients/${this.clientId}`)
        .then(() => {
          this.$router.push({ name: "ClientDashboard" });
        })
        .catch((error) => {
          console.error("archive failed", error.response?.data || error);
        });
    },
    resetForm() {
      this.form = {
        firstName: "",
        lastName: "",
        email: "",
        phones: [
          {
            countryCode: "+1",
            number: "",
            type: this.phonenumbertypes.length
              ? this.phonenumbertypes[0].id
              : null,
            isPrimary: true,
          },
        ],
      };
      this.$emit("cancel");
    },
    submitForm() {
      if (this.mode === "create") {
        this.createClient();
      } else {
        this.updateClient();
      }
      this.$router.push({ name: "ClientDashboard" });
    },
    //data exchange functions
    loadClientForEdit() {
      apiClient.get(`api/Clients/${this.clientId}`).then((response) => {
        const client = response.data;

        this.form.firstName = client.firstName;
        this.form.lastName = client.lastName;
        this.form.email = client.email;

        this.form.phones = (client.phoneNumbers || []).map((phone) => ({
          id: phone.phoneNumberId,
          countryCode: String(phone.countryCode || "+1"),
          number: phone.phoneNumber || "",
          type: phone.phoneNumberType,
          isPrimary: phone.isPrimary,
        }));

        if (!this.form.phones.length) {
          this.form.phones = [
            { countryCode: "+1", number: "", type: null, isPrimary: true },
          ];
        }
      });
    },
    createClient() {
      const client = {
        firstName: this.form.firstName,
        lastName: this.form.lastName,
        email: this.form.email,
      };
      apiClient
        .post("api/Clients", client)
        .then((response) => {
          const createdClient = response.data;
          // Emit the payload up to the parent component
          this.$emit("client-created", createdClient);

          const phonesToCreate = this.form.phones.filter((phone) => {
            return (
              phone &&
              phone.number &&
              phone.number.trim() &&
              phone.type !== null &&
              phone.type !== ""
            );
          });

          phonesToCreate.forEach((phone) => {
            this.createPhone(phone, createdClient.clientId);
          });
          //don't want to reset form before numbers are created
          this.resetForm();
        })
        .catch((error) => {
          console.error("client create failed:", error.response?.data || error);
        });
    },
    createPhone(phone, clientId) {
      const payload = {
        clientId: clientId,
        countryCode: Number(String(phone.countryCode).replace(/\D/g, "")) || 0,
        phoneNumber: phone.number.trim(),
        phoneNumberType: Number(phone.type),
        isPrimary: !!phone.isPrimary,
      };

      apiClient
        .post("api/PhoneNumber", payload)
        .then((phoneResponse) => {
          console.log("phone created:", phoneResponse.data);
        })
        .catch((error) => {
          console.error("phone create failed:", error.response?.data || error);
        });
    },
    updateClient() {
      const client = {
        firstName: this.form.firstName,
        lastName: this.form.lastName,
        email: this.form.email,
        isArchived: false,
      };
      apiClient
        .put(`api/Clients/${this.clientId}`, client)
        .then((response) => {
          console.log("client updated", response.data);
        })
        .catch((error) => {
          console.error("client update failed", error.response?.data || error);
        });
      //filter phone number list where 'phone number' has a value
      const phonesToCreateOrEdit = this.form.phones.filter((phone) => {
        return (
          phone &&
          phone.number &&
          phone.number.trim() &&
          phone.type !== null &&
          phone.type !== ""
        );
      });
      phonesToCreateOrEdit.forEach((phone) => {
        console.log("phone id", phone.id);

        //new phone
        if (phone.id === undefined) {
          this.createPhone(phone, this.clientId);
        } else {
          const payload = {
            countryCode:
              Number(String(phone.countryCode).replace(/\D/g, "")) || 0,
            phoneNumber: phone.number.trim(),
            phoneNumberType: Number(phone.type),
            isPrimary: !!phone.isPrimary,
          };
          apiClient
            .put(`api/PhoneNumber/${phone.id}`, payload)
            .then((response) => {
              console.log(response);
            })
            .catch((error) => {
              console.error(
                "updating phone failed",
                error.response?.data || error,
              );
            });
        }
      });
    },
  },
};
</script>
