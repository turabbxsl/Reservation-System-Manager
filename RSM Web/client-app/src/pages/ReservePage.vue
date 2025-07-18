<template>
  <div class="container py-5">
    <div class="p-4">
      <h2 class="text-2xl font-bold mb-4">Rezervasiya Et</h2>

      <!-- Sirket secimi -->
      <label>Şirkət:</label>
      <select v-model="selectedCompanyId" class="form-select mb-3">
        <option disabled>Şirkət seçin</option>
        <option v-for="company in companies" :key="company.id" :value="company.id">
          {{ company.name }}
        </option>
      </select>

      <!-- Xidmet secimi -->
      <label>Xidmət:</label>
      <select v-model="selectedServiceId" class="form-select mb-3" :disabled="!services.length">
        <option disabled>Xidmət seçin</option>
        <option v-for="service in services" :key="service.id" :value="service.id">
          {{ service.name }}
        </option>
      </select>

      <label>Tarix seçin:</label>
      <input type="date" v-model="selectedDate" class="form-control mb-3" @change="fetchAvailableHours"
        :disabled="!selectedServiceId" />

      <label>Vaxt seçin:</label>
      <select v-model="selectedTime" class="form-select mb-3" :disabled="!Object.keys(availableTimes).length">
        <option disabled value="">Vaxt seçin</option>
        <option v-for="(isAvailable, time) in availableTimes" :key="time" :value="time" :disabled="!isAvailable">
          {{ time }} <span v-if="!isAvailable"> (Dolu)</span>
        </option>
      </select>

      <button class="btn btn-primary" @click="createReservation" :disabled="!isValid">
        Rezervasiya Et
      </button>

    </div>
  </div>
</template>

<script>

import { getAllCompanies, getServicesByCompanyId } from "@/services/companyService";
import { getAvailableTimes } from "@/services/reservationService";
import { ref, onMounted, watch, computed } from "vue";


export default {
  name: 'ReservePage',
  setup() {
    const companies = ref([])
    const services = ref([]);
    const availableTimes = ref({});

    const selectedCompanyId = ref()
    const selectedServiceId = ref("");

    const selectedDate = ref("");
    const selectedTime = ref("");

    const fetchCompanies = async () => {
      try {
        const res = await getAllCompanies();
        if (res.isSuccess) {
          companies.value = res.data;
        }
        else {
          res.errors.forEach((error) => {
            this.$notify('error', error, 'Şirkətlər gətirilə bilmədi');
          });
        }
      }
      catch (error) {
        console.log('catch unexpecting error', error);
      }
    }

    const fetchServices = async (companyId) => {
      try {
        const res = await getServicesByCompanyId(companyId);
        if (res.isSuccess) {
          services.value = res.data;
          availableTimes.value = {}; // Reset available times when services change
        } else {
          res.errors.forEach((error) => {
            this.$notify('error', error, 'Xidmətlər gətirilə bilmədi');
          });
        }
      } catch (error) {
        console.log('catch unexpecting error', error);
      }
    }

    const fetchAvailableHours = async () => {
      if (!selectedCompanyId.value || !selectedServiceId.value || !selectedDate.value)
        return;

      try {
        const res = await getAvailableTimes(
          selectedCompanyId.value,
          selectedServiceId.value,
          selectedDate.value
        );
        if (res.isSuccess) {
          availableTimes.value = res.data;
        } else {
          res.errors.forEach((error) => {
            this.$notify('error', error, 'Xidmətlər gətirilə bilmədi');
          });
        }
      } catch (error) {
        console.log("Saatlar yüklənmədi:", error);
      }
    };

    watch(selectedCompanyId, (newId) => {
      if (newId) {
        fetchServices(newId);
      }
      else {
        services.value = [];
      }
    })

    const isValid = computed(() => {
      return selectedCompanyId.value && selectedServiceId.value && selectedDate.value && selectedTime.value;
    });

    onMounted(fetchCompanies)

    return {
      companies,
      services,
      selectedCompanyId,
      selectedServiceId,
      selectedDate,
      selectedTime,
      availableTimes,
      fetchAvailableHours,
      isValid
    }
  }
}
</script>

<style scoped>
h2 {
  color: #007bff;
}
</style>