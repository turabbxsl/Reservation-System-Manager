<template>
  <div class="container py-5">
    <div class="row shadow rounded-4 overflow-hidden bg-white">

      <div class="col-lg-7 col-md-12 p-5 bg-white">
        <h2 class="mb-4 text-primary fw-bold text-center reservation-title">
          Rezervasiya Et
        </h2>

        <div class="mb-3">
          <label class="form-label fw-semibold">Şirkət Adı</label>
          <Dropdown v-model="selectedCompanyId" :options="companies" :disabled="!companies.length" optionLabel="name"
            optionValue="id" placeholder="Şirkət seçin" class="w-100" />
        </div>

        <div class="mb-3">
          <label class="form-label fw-semibold">Xidmət Kateqoriyası</label>
          <Dropdown v-model="selectedSpecId" :options="specialities" :disabled="!specialities.length" optionLabel="name"
            optionValue="specialtyId" placeholder="Xidmət seçin" class="w-100"
            @update:modelValue="loadServicesBySpecialty" />
        </div>

        <div class="mb-3">
          <label class="form-label fw-semibold">Xidmət(lər)</label>
          <MultiSelect v-model="selectedServices" :options="services" :disabled="!services.length" optionLabel="name"
            optionValue="id" placeholder="Xidmət seçin" class="w-100" />
        </div>

        <div class="mb-3">
          <label class="form-label fw-semibold">Tarix və saat:</label>
          <div class="d-flex flex-column flex-md-row gap-3 mt-2">
            <DatePicker v-model="selectedDate" showIcon :minDate="today" iconDisplay="input"
              :disabled="!selectedServices.length" @update:modelValue="onDateChange" dateFormat="dd-mm-yy"
              :locale="azLocale" class="flex-fill" />


            <Dropdown v-model="selectedTime" :options="timeOptions" optionLabel="label" optionValue="value"
              optionDisabled="disabled" placeholder="Vaxt seçin" class="flex-fill" :disabled="!timeOptions.length"
              @change="onTimeChange" />
          </div>
        </div>

        <div class="mt-4">
          <button class="btn btn-primary w-100 py-2 fs-5" @click="submitReservation" :disabled="!isValid">
            Rezervasiya Et
          </button>
        </div>
      </div>

      <div class="col-lg-5 d-none d-lg-block p-0">
        <div class="h-100 w-100" :style="{
          backgroundImage: `url(${reservationImage})`,
          backgroundSize: 'cover',
          backgroundPosition: 'center'
        }">
        </div>
      </div>
    </div>
  </div>
</template>


<script>

import { getAllCompanies, getSpecialtiesByCompanyId } from "@/services/companyService";
import { getAvailableTimes, createReservation, getServicesBySpecialtyWithCompanyId } from "@/services/reservationService";
import { ref, onMounted, watch, computed } from "vue";
import { useAuthStore } from "@/stores/authStore";
import moment from "moment";
import notify from "@/assets/js/notify";
import { useRouter } from "vue-router";

import Dropdown from "primevue/dropdown";
import { MultiSelect, DatePicker } from "primevue";

import reservationImage from '@/assets/img/reservation.jpg';

export default {
  name: 'ReservePage',
  components: { Dropdown, MultiSelect, DatePicker },
  setup() {

    const authStore = useAuthStore();
    const router = useRouter();

    const companies = ref([])
    const specialities = ref([]);
    const services = ref([]);
    const availableTimes = ref({});

    const selectedCompanyId = ref()
    const selectedSpecId = ref("");
    const selectedServices = ref([]);

    const selectedDate = ref(null);
    const selectedTime = ref("");

    const today = ref("");

    const azLocale = {
      firstDayOfWeek: 1,
      dayNames: ['Bazar', 'Bazar ertəsi', 'Çərşənbə axşamı', 'Çərşənbə', 'Cümə axşamı', 'Cümə', 'Şənbə'],
      dayNamesShort: ['Bazar', 'B.ertəsi', 'Ç.axşamı', 'Çərşənbə', 'C.axşamı', 'Cümə', 'Şənbə'],
      monthNames: ['Yanvar', 'Fevral', 'Mart', 'Aprel', 'May', 'İyun', 'İyul', 'Avqust', 'Sentyabr', 'Oktyabr', 'Noyabr', 'Dekabr'],
      monthNamesShort: ['Yanv.', 'Fevr.', 'Mart', 'Apr.', 'May.', 'İyun.', 'İyul.', 'Avq.', 'Sent.', 'Okt.', 'Noyab.', 'Dek.']
    };

    const fetchCompanies = async () => {
      try {
        const res = await getAllCompanies();
        if (res.isSuccess) {
          companies.value = res.data;
        }
        else {
          res.errors.forEach((error) => {
            notify('error', error, 'Şirkətlər gətirilə bilmədi');
          });
        }
      }
      catch (error) {
        console.log('catch unexpecting error', error);
      }
    }

    const onDateChange = (date) => {
      if (date) {
        selectedDate.value = date;
        console.log("Selected date:", formattedDate.value);
        fetchAvailableHours();
      }
    };

    const timeOptions = computed(() => {
      return Object.entries(availableTimes.value).map(([time, isAvailable]) => ({
        label: isAvailable ? time : `${time} (Dolu)`,
        value: time,
        disabled: !isAvailable
      }));
    });

    const onTimeChange = (value) => {
      const option = timeOptions.value.find(opt => opt.value === value);
      if (option && option.disabled) {
        selectedTime.value = null;
        notify('error', 'Bu vaxt artıq doludur', 'Seçim edilə bilməz');
      }
    };

    const fetchServices = async (companyId) => {
      try {
        const res = await getSpecialtiesByCompanyId(companyId);
        if (res.isSuccess) {
          specialities.value = res.data;
          availableTimes.value = {};
        } else {
          res.errors.forEach((error) => {
            notify('error', error, 'Xidmətlər gətirilə bilmədi');
          });
        }
      } catch (error) {
        console.log('catch unexpecting error', error);
      }
    }

    const loadServicesBySpecialty = async () => {
      if (selectedSpecId.value) {
        const res = await getServicesBySpecialtyWithCompanyId(selectedCompanyId.value, selectedSpecId.value);
        if (res.isSuccess) {
          services.value = res.data;

          if (selectedDate.value)
            fetchAvailableHours();
        } else {
          res.errors.forEach(err => this.$notify('error', err, 'Xidmətlər gətirilə bilmədi'));
        }
      }
      else {
        services.value = [];
      }
    };

    const fetchAvailableHours = async () => {
      if (!selectedCompanyId.value || !selectedSpecId.value || !selectedDate.value)
        return;

      try {
        const res = await getAvailableTimes(
          selectedCompanyId.value,
          selectedSpecId.value,
          formattedDate.value
        );
        if (res.isSuccess) {
          availableTimes.value = res.data;
        } else {
          res.errors.forEach((error) => {
            notify('error', error, 'Xidmətlər gətirilə bilmədi');
          });
        }
      } catch (error) {
        console.log("Saatlar yüklənmədi:", error);
      }
    };

    const submitReservation = async () => {
      if (!isValid.value) {
        notify('error', 'Bütün sahələri doldurun', 'Rezervasiya yaradıla bilmədi');
        return;
      }

      const formattedDate = moment(selectedDate.value).format('DD-MM-YYYY');

      const reservationData = {
        companyId: selectedCompanyId.value,
        selectedSpecId: selectedSpecId.value,
        selectedServices: selectedServices.value,
        customerId: authStore.user?.id,
        reservationDate: formattedDate,
        reservationTime: selectedTime.value
      };

      try {
        const res = await createReservation(reservationData);
        if (res.isSuccess) {
          notify('success', 'Rezervasiya uğurla yaradıldı', 'Uğurlu');
          router.push('/profile');
          selectedCompanyId.value = null;
          selectedSpecId.value = "";
          selectedDate.value = "";
          selectedTime.value = "";
          availableTimes.value = {};
          specialities.value = [];
        } else {
          res.errors.forEach((error) => {
            notify('error', error, 'Rezervasiya yaradıla bilmədi');
          });
        }
      } catch (error) {
        console.log('catch unexpecting error', error);
      }
    }

    watch(selectedCompanyId, (newId) => {
      if (newId) {
        fetchServices(newId);
      }
      else {
        specialities.value = [];
      }
    })

    const isValid = computed(() => {
      return selectedCompanyId.value && selectedSpecId.value && selectedDate.value && selectedTime.value;
    });

    onMounted(fetchCompanies)

    onMounted(() => {
      const now = new Date();
      today.value = now;
    });

    const formattedDate = computed(() => {
      return selectedDate.value
        ? moment(selectedDate.value).format('DD-MM-YYYY')
        : '';
    });

    return {
      companies,
      specialities,
      services,
      selectedCompanyId,
      selectedSpecId,
      selectedServices,
      selectedDate,
      selectedTime,
      availableTimes,
      isValid,
      today,
      timeOptions,
      reservationImage,
      fetchAvailableHours,
      submitReservation,
      loadServicesBySpecialty,
      onDateChange,
      onTimeChange,
      azLocale,
      formattedDate
    }
  }
}
</script>

<style scoped>
.reservation-title {
  font-size: 1.75rem;
  font-family: "Times New Roman", Times, serif;
  font-weight: 600;
  color: #007bff;
  letter-spacing: 0.3px;
  margin-bottom: 1rem;
  margin-top: 0.5rem;
}

.container.py-5 {
  padding-left: 2rem;
  padding-right: 2rem;
  padding-top: 3rem;
  padding-bottom: 3rem;
}

@media (max-width: 991.98px) {
  .container.py-5 {
    padding-left: 1rem;
    padding-right: 1rem;
    padding-top: 2rem;
    padding-bottom: 2rem;
  }

  .row.shadow.rounded-4 {
    margin-left: 0;
    margin-right: 0;
  }

  .reservation-title {
    margin-top: 0.5rem;
    margin-bottom: 1rem;
    font-size: 1.4rem;
  }
}

@media (max-width: 575.98px) {

  .container.py-5 {
    padding-left: 0.75rem;
    padding-right: 0.75rem;
    padding-top: 1.5rem;
    padding-bottom: 1.5rem;
  }

  .reservation-title {
    margin-top: 0.3rem;
    font-size: 1.3rem;
  }
}


button.btn-primary {
  transition: 0.3s ease;
}

button.btn-primary:hover {
  background-color: #0056b3;
}

button.btn-primary:disabled {
  background-color: #86bce7;
  cursor: not-allowed;
  border: none;
}

.form-field label {
  font-weight: 500;
  margin-bottom: 0.3rem;
}

.form-field {
  display: flex;
  flex-direction: column;
}

.date-time-container {
  display: flex;
  gap: 10px;
}

.flex-item {
  flex: 1;
}

@media (max-width: 768px) {
  .date-time-container {
    flex-direction: column;
  }
}
</style>