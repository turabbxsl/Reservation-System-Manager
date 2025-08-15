<template>
  <div class="bg-light">
    <div class="container-fluid px-3 px-md-5 py-4">
      <div class="d-flex justify-content-between align-items-center border-bottom py-2 d-lg-none px-3">
        <button class="navbar-toggler border-0" type="button" data-bs-toggle="offcanvas" data-bs-target="#mobileMenu">
          <i class="bi bi-list toggle-icon list-icon fs-1"></i>
        </button>
        <div class="custom-tab-label">
          {{ currentTabLabel }}
        </div>
      </div>
      <div class="offcanvas offcanvas-start d-lg-none" tabindex="-1" style="top: 130px; height: calc(100% - 145px)"
        id="mobileMenu">
        <div class="offcanvas-header">
          <h5 class="offcanvas-title">Profil menyusu</h5>
          <button type="button" class="btn-close" data-bs-dismiss="offcanvas"></button>
        </div>
        <div class="offcanvas-body">
          <ul class="nav flex-column nav-pills">
            <li class="nav-item" v-for="item in tabs" :key="item.name">
              <a class="nav-link" :class="{ active: activeTab === item.name }" @click="setTab(item.name)"
                data-bs-dismiss="offcanvas">
                <i :class="item.icon" class="me-2"></i>{{ item.label }}
              </a>
            </li>
            <li class="nav-item">
              <a class="nav-link" @click.prevent="logout" data-bs-dismiss="offcanvas">
                <i class="fas fa-sign-out-alt me-2"></i>Çıxış
              </a>
            </li>
          </ul>
        </div>
      </div>

      <div class="row">
        <div class="col-12 my-1">
          <div class="card border-0 shadow-sm">
            <div class="card-body p-0">
              <div class="row g-0 full-height">

                <div class="col-lg-3 border-end d-none d-lg-block">
                  <div class="p-4">
                    <div class="nav flex-column nav-pills">
                      <a class="nav-link" :class="{ active: activeTab === 'info' }" @click="activeTab = 'info'">
                        <i class="fas fa-user me-2"></i>Məlumatlarım
                      </a>
                      <a class="nav-link" :class="{ active: activeTab === 'reservations' }"
                        @click="activeTab = 'reservations'">
                        <i class="fas fa-lock me-2"></i>Reservasiyalarım
                      </a>
                      <a class="nav-link" :class="{ active: activeTab === 'settings' }" @click="activeTab = 'settings'">
                        <i class="fas fa-bell me-2"></i>Tənzimləmələr
                      </a>
                      <a class="nav-link" @click.prevent="logout">
                        <i class="fas fa-sign-out-alt me-2"></i>Çıxış
                      </a>
                    </div>
                  </div>
                </div>

                <div class="col-lg-9" v-if="activeTab === 'info'">
                  <div class="d-flex justify-content-end p-3 gap-2">
                    <button class="btn btn-sm btn-primary d-flex align-items-center gap-2 btn-modern"
                      @click="isEdit = !isEdit" v-if="!isEdit">
                      <i class="bi bi-pencil"></i> Düzəliş et
                    </button>

                    <button class="btn btn-sm btn-success d-flex align-items-center gap-2 btn-modern"
                      @click="EditCustomer" v-if="isEdit">
                      <i class="bi bi-save"></i> Yadda saxla
                    </button>

                    <button class="btn btn-sm btn-danger d-flex align-items-center gap-2 btn-modern"
                      @click="isEdit = false" v-if="isEdit">
                      <i class="bi bi-x-lg"></i> Ləğv et
                    </button>
                  </div>


                  <div class="p-4">
                    <div class="mb-4">
                      <div class="row g-3">
                        <div class="col-md-6">
                          <label class="form-label">Ad və Soyad</label>
                          <input type="text" class="form-control" v-model="user.fullName"
                            @input="onInputChange('fullName', $event.target.value)" :disabled="!isEdit">
                        </div>
                        <div class="col-md-6">
                          <label class="form-label">Telefon Nömrəsi</label>
                          <input type="tel" class="form-control" v-model="user.phoneNumber"
                            @input="onInputChange('phoneNumber', $event.target.value)" :disabled="!isEdit">
                        </div>
                        <div class="col-md-6">
                          <label class="form-label">Email</label>
                          <input type="email" class="form-control" v-model="user.email"
                            @input="onInputChange('email', $event.target.value)" :disabled="!isEdit">
                        </div>
                        <div class="col-md-6">
                          <label class="form-label">Müştəri kodu</label>
                          <input type="text" class="form-control" v-model="user.clientCode" disabled>
                        </div>
                        <div class="col-12">
                          <label class="form-label">Əlavə Qeyd</label>
                          <textarea class="form-control" rows="3" v-model="user.note"
                            @input="onInputChange('note', $event.target.value)" :disabled="!isEdit"></textarea>
                        </div>
                      </div>
                    </div>

                    <div class="row g-4 mb-4">
                      <div class="col-md-6">
                        <div class="settings-card card">
                          <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center">
                              <div>
                                <h6 class="mb-1">SMS xəbərdarlıq</h6>
                                <p class="text-muted mb-0 small">
                                  Rezervasiyalarınız ilə bağlı bildirişləri <b>SMS</b> ilə alın
                                </p>
                              </div>
                              <div class="form-check form-switch">
                                <input class="form-check-input" type="checkbox" :checked="user.isSms">
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div class="col-md-6">
                        <div class="settings-card card">
                          <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center">
                              <div>
                                <h6 class="mb-1">Email Xəbərdarlıq</h6>
                                <p class="text-muted mb-0 small">
                                  Rezervasiyalarınız ilə bağlı bildirişləri <b>Email</b> ilə alın
                                </p>
                              </div>
                              <div class="form-check form-switch">
                                <input class="form-check-input" type="checkbox" :checked="user.isEmail">
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>

                <div class="col-lg-9" v-if="activeTab === 'reservations'">
                  <div class="p-4">

                    <div v-if="!reservations?.length" class="text-center py-5 empty-state">
                      <p class="mt-3 text-muted fs-5">Rezervasiyalarınızı buradan izləyə bilərsiniz.</p>
                      <router-link to="/reserve" class="btn btn-primary btn-modern" aria-label="Make a reservation">
                        Yeni Rezervasiya
                      </router-link>
                    </div>

                    <div class="row gx-4 gy-4" v-else aria-live="polite">
                      <div class="col-12 col-md-4" v-for="(reserve, i) in getCards" :key="reserve.id">
                        <div class="reservation-card card shadow-sm h-100">
                          <div class="card-body d-flex flex-column">

                            <div class="d-flex justify-content-end align-items-center mb-3">
                              <span class="badge status-badge" :class="statusClass(reserve.status.statusName)"
                                :style="{ backgroundColor: pastelColors[i % pastelColors.length] }" aria-label="Status">
                                {{ reserve.status.statusName }}
                              </span>
                            </div>


                            <table class="table table-borderless table-sm">
                              <tbody>
                                <tr>
                                  <td class="label-text fw-bold">Şirkət:</td>
                                  <td class="value-text">{{ reserve.company.name || '—' }}</td>
                                </tr>
                                <tr>
                                  <td class="label-text fw-bold">Xidmət kateqoriyası:</td>
                                  <td class="value-text">{{ reserve.specialty.specialtyName || '—' }}</td>
                                </tr>
                                <td colspan="2">
                                  <hr class="my-3" />
                                </td>
                                <tr>
                                  <td class="label-text fw-bold">Rezervasiya nömrəsi:</td>
                                  <td class="value-text">{{ reserve.reservationNumber || '—' }}</td>
                                </tr>
                                <tr>
                                  <td class="label-text fw-bold">Rezervasiya vaxtı:</td>
                                  <td class="value-text">
                                    {{ formatDateTimeWithWeekday(reserve.reservationDateTime).formattedDate }}
                                    {{ formatDateTimeWithWeekday(reserve.reservationDateTime).formattedTime }}
                                  </td>
                                </tr>
                                <tr>
                                  <td class="label-text fw-bold">Ümumi məbləğ:</td>
                                  <td class="value-text">{{ formatCurrency(reserve.totalPrice) || '—' }}</td>
                                </tr>
                              </tbody>
                            </table>


                            <div class="services-section mt-3 mb-3">
                              <div v-if="reserve.services?.length">
                                <button class="btn btn-link btn-sm p-0 fw-semibold" type="button"
                                  @click="toggleCollapse(reserve.id)">
                                  Xidmətləri göstər
                                  <span v-if="collapsedStates[reserve.id]" class="service-toggle-icon fs-5">▼</span>
                                  <span v-else class="service-toggle-icon fs-5">▲</span>
                                </button>

                                <transition @before-enter="beforeEnter" @enter="enter" @after-enter="afterEnter"
                                  @before-leave="beforeLeave" @leave="leave" @after-leave="afterLeave">
                                  <table v-show="collapsedStates[reserve.id]" class="table table-sm mt-2"
                                    ref="collapseList">
                                    <tbody>
                                      <tr v-for="svc in reserve.services" :key="svc.id">
                                        <td>{{ svc.name }}</td>
                                        <td class="text-end">{{ formatCurrency(svc.price) }}</td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </transition>
                              </div>
                              <div v-else class="text-muted small">Xidmət göstərilməyib</div>
                            </div>

                            <p v-if="reserve.note" class="note-text">{{ reserve.note }}</p>
                          </div>


                          <div
                            class="card-footer bg-white border-0 d-flex justify-content-between align-items-center mb-2">
                            <div class="d-flex gap-2 align-items-center">
                              <button class="btn btn-sm btn-danger btn-modern"
                                v-if="ShowCancelReservation(reserve.status.statusName)"
                                @click="OpenCancelModal(reserve.id)">
                                Ləğv et
                              </button>
                            </div>
                            <div class="text-end small d-flex flex-wrap gap-2 align-items-center">
                              <i class="text-muted">{{
                                formatDateTimeWithWeekday(reserve.createdDate).formattedDate }}</i>
                              <i class="text-muted">{{
                                formatDateTimeWithWeekday(reserve.createdDate).formattedTime }}</i>
                            </div>
                          </div>

                        </div>
                      </div>
                    </div>
                    <div class="text-center mt-3">
                      <button @click="handleShowMore" v-if="currentStep < total"
                        class="btn btn-outline-primary btn-modern" :disabled="loading">
                        <span v-if="!loading">Daha çox göstər</span>
                        <span v-else>
                          <span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                          Yüklənir...
                        </span>
                      </button>

                      <button @click="ShowLess" v-if="getCards.length > 6" class="btn btn-outline-secondary btn-modern">
                        Daha az göstər
                      </button>
                    </div>
                  </div>
                </div>

                <div class="col-lg-9" v-if="activeTab === 'settings'">
                  <div class="p-4">

                    <fieldset class="border rounded p-3 p-md-4 mx-auto">
                      <legend class="w-auto px-2 px-md-3 fs-6 fs-md-5 fw-semibold">Şifrə Yenilə</legend>

                      <div class="mb-3">
                        <label for="currentPassword" class="form-label fw-medium">Cari Şifrə</label>
                        <input type="password" id="currentPassword" v-model="passwords.currentPassword"
                          class="form-control w-100" placeholder="Cari şifrənizi daxil edin" />
                      </div>

                      <div class="mb-3">
                        <label for="newPassword" class="form-label fw-medium">Yeni Şifrə</label>
                        <input type="password" id="newPassword" v-model="passwords.newPassword"
                          class="form-control w-100" placeholder="Yeni şifrənizi daxil edin" />
                      </div>

                      <div class="mb-3">
                        <label for="confirmPassword" class="form-label fw-medium">Yeni Şifrəni Təsdiqlə</label>
                        <input type="password" id="confirmPassword" v-model="passwords.confirmPassword"
                          class="form-control w-100" placeholder="Yeni şifrəni yenidən daxil edin" />
                      </div>

                      <button
                        class="btn btn-success btn-modern btn-mobile-full mb-2 d-flex justify-content-center align-items-center gap-2"
                        @click="UpdatePassword"
                        :disabled="passwords.currentPassword === '' || passwords.newPassword === '' || passwords.confirmPassword === ''">
                        <i class="bi bi-save"></i> Yadda saxla
                      </button>
                    </fieldset>

                  </div>
                </div>
              </div>

            </div>
          </div>
        </div>
      </div>

      <div v-if="showOtpModal" class="modal-backdrop fade show" style="z-index: 1040"></div>
      <div class="modal fade" :class="{ show: showOtpModal }" style="display: block;" tabindex="-1" role="dialog"
        v-if="showOtpModal" @click.self="closeOtpModal">
        <div class="modal-dialog modal-dialog-centered" role="document">
          <div class="modal-content rounded-4 shadow p-4">
            <div class="modal-header border-0 pb-0">
              <h5 class="modal-title">6 Rəqəmli OTP Təsdiqi</h5>
              <button type="button" class="btn-close" @click="closeOtpModal"></button>
            </div>

            <div class="modal-body text-center">
              <p class="mb-3 text-muted">Zəhmət olmasa, {{ formatPhoneNumber(user.phoneNumber) }} göndərilən 6 rəqəmli
                təsdiq
                kodunu daxil edin.</p>

              <div class="d-flex justify-content-center gap-2 otp-inputs">
                <input v-for="(digit, index) in otpDigits" :key="index" type="text" maxlength="1"
                  class="text-center fs-3" :value="otpDigits[index]" @input="(e) => onOtpInput(index, e)"
                  :ref="`otp_${index}`" autocomplete="one-time-code" inputmode="numeric" pattern="[0-9]*"
                  @keydown.backspace="onOtpBackspace(index, $event)" />
              </div>

              <button class="btn btn-success w-100 mt-4" :disabled="!isOtpComplete || loading" @click="verifyOtp">
                {{ loading ? "Yoxlanılır..." : "Təsdiqlə" }}
              </button>

              <button class="btn btn-link mt-3 w-100" @click="resendOtp" :disabled="resendTimer > 0">
                {{ resendTimer > 0
                  ? `Kodu yenidən göndərmək üçün ${resendTimer}s gözləyin`
                  : "Kodu yenidən göndər" }}
              </button>
            </div>

            <div class="modal-footer border-0 pt-0">
              <button type="button" class="btn btn-secondary w-100" @click="closeOtpModal">
                Bağla
              </button>
            </div>
          </div>
        </div>
      </div>

      <div class="modal fade" id="cancelModal" tabindex="-1" aria-labelledby="cancelModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-dialog-top">
          <div class="modal-content">
            <div class="modal-header bg-danger text-white">
              <h5 class="modal-title" id="cancelModalLabel">Rezervasiya ləğv</h5>
              <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"
                aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <p class="mb-0">Rezervasiyanızı ləğv etmək istədiyinizə əminsiniz mi ?</p>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Xeyr</button>
              <button type="button" class="btn btn-danger" @click="ConfirmCancelReservation">Ləğv et</button>
            </div>
          </div>
        </div>
      </div>

    </div>
  </div>
</template>

<script>
import { useAuthStore } from '@/stores/authStore'
import { getCustomerDetails, EditCustomerDetails, getCustomerReservations, UpdateCustomerPassword, CancelReservation } from "@/services/customerService";
import { useRouter } from 'vue-router'

import { Modal } from 'bootstrap';

export default {
  name: "ProfilePage",
  data() {
    return {
      activeTab: "info",
      user: {
        fullName: "",
        phoneNumber: "",
        email: "",
        createdDate: "",
        note: "",
        isSms: false,
        isEmail: false
      },
      reservations: [],
      showMenu: false,
      router: useRouter(),
      isEdit: false,
      originalUser: {},
      editableUser: {
        fullName: { value: '', isEdit: false },
        phoneNumber: { value: '', isEdit: false },
        email: { value: '', isEdit: false },
        note: { value: '', isEdit: false },
      },
      authStore: useAuthStore(),
      collapsedStates: {},
      pastelColors: [
        '#ffffff',
        '#fffeff',
        '#fffffe',
        '#fefeff',
        '#fffefd',
        '#fffcfe',
        '#fffdfa',
        '#fffefa',
        '#fefdfc',
        '#fffefb',
        '#fffefe',
        '#fefeff',
        '#fffffd',
        '#fefcff',
        '#fffeff',
        '#fffefe',
        '#fefeff',
        '#fffffe',
        '#fffefd',
        '#fffefe'
      ],
      passwords: {
        currentPassword: '',
        newPassword: '',
        confirmPassword: ''
      },
      showOtpModal: false,
      otpDigits: ["", "", "", "", "", ""],
      loading: false,
      resendTimer: 0,
      resendInterval: null,
      selectedReservationId: null,
      stepBy: 6,
      currentStep: 6
    };
  },
  mounted() {
    this.customerDetails();
    this.customerReservations();
    this.convertToEditable(this.user);
  },
  methods: {
    handleShowMore() {
      this.loading = true;

      setTimeout(() => {
        this.ShowMore();
        this.loading = false;
      }, 350);
    },
    beforeEnter(el) {
      el.style.height = '0';
      el.style.opacity = '0';
      el.style.overflow = 'hidden';
    },
    enter(el) {
      el.style.transition = 'height 0.3s ease, opacity 0.3s ease';
      el.style.height = '0';
      el.style.opacity = '0';

      requestAnimationFrame(() => {
        el.style.height = el.scrollHeight + 'px';
        el.style.opacity = '1';
      });
    },
    afterEnter(el) {
      el.style.height = 'auto';
      el.style.overflow = 'hidden';
    },
    beforeLeave(el) {
      el.style.height = el.scrollHeight + 'px';
      el.style.opacity = '1';
      el.style.overflow = 'hidden';
    },
    leave(el) {
      el.style.transition = 'height 0.3s ease, opacity 0.3s ease';

      requestAnimationFrame(() => {
        el.style.height = '0';
        el.style.opacity = '0';
      });
    },
    afterLeave(el) {
      el.style.height = '0';
      el.style.overflow = 'hidden';
    },

    toggleCollapse(id) {
      this.collapsedStates[id] = !this.collapsedStates[id];
    },
    formatDateTimeWithWeekday(dateString) {
      const daysAz = ["Bazar", "Bazar ertəsi", "Çərşənbə axşamı", "Çərşənbə", "Cümə axşamı", "Cümə", "Şənbə"];

      const dt = new Date(dateString);

      const day = String(dt.getDate()).padStart(2, '0');
      const month = String(dt.getMonth() + 1).padStart(2, '0');
      const year = dt.getFullYear();

      const formattedDate = `${day}-${month}-${year}`;

      const hours = String(dt.getHours()).padStart(2, '0');
      const minutes = String(dt.getMinutes()).padStart(2, '0');
      const seconds = String(dt.getSeconds()).padStart(2, '0');

      const formattedTime = `${hours}:${minutes}:${seconds}`;

      const weekdayName = daysAz[dt.getDay()];

      return {
        formattedDate,
        formattedTime,
        weekdayName
      };
    },
    formatCurrency(amount) {
      if (amount == null) return '';
      return Number(amount).toLocaleString('az-AZ', { style: 'currency', currency: 'AZN' });
    },
    formatPhoneNumber(phoneNumber) {
      if (!phoneNumber) return '';
      // Format: (50) 123-45-67
      const cleaned = phoneNumber.replace(/\D/g, '');
      const match = cleaned.match(/^(\d{2})(\d{3})(\d{2})(\d{2})$/);
      if (match) {
        return `+994 (${match[1]}) *** ** ${match[4]}`;
      }
      return phoneNumber;
    },
    statusClass(status) {
      const s = (status || '').toLowerCase();
      if (s === 'pending' || s === 'gözlənilir') return 'bg-warning text-dark';
      if (s === 'approved' || s === 'təsdiq edildi') return 'bg-success text-white';
      if (s === 'cancelled' || s === 'imtina edilib') return 'bg-danger text-white';
      if (s === 'completed' || s === 'tamamlandı') return 'bg-primary text-white';
      if (s === 'refunded' || s === 'geri qaytarılıb') return 'bg-primary text-white';
      if (s === 'failedduetoverbooking' || s === 'yer yoxdur') return 'bg-primary text-white';
      if (s === 'customerCancelled' || s === 'müştəri ləğvi') return 'bg-danger text-white';
      return 'bg-secondary text-white';
    },
    setTab(tab) {
      this.activeTab = tab;
    },
    onInputChange(key, val) {
      if (!this.isEdit) return;
      if (!(key in this.editableUser)) return;

      this.editableUser[key].value = val;
      if (val === this.originalUser[key]) {
        this.editableUser[key].isEdit = false;
      } else {
        this.editableUser[key].isEdit = true;
      }
    },
    async customerDetails() {
      {
        try {
          const res = await getCustomerDetails();
          if (!res.isSuccess && Array.isArray(res.errors)) {
            res.errors.forEach((error) => {
              this.$notify('error', error, '');
            });
          } else if (res.isSuccess && res.data) {
            this.user = res.data;
            this.originalUser = JSON.parse(JSON.stringify(res.data));
          }
        } catch (err) {
          console.error('Xəta baş verdi:', err);
        }
      }
    },
    async customerReservations() {
      try {
        const res = await getCustomerReservations();
        if (!res.isSuccess && Array.isArray(res.errors)) {
          res.errors.forEach((error) => {
            this.$notify('error', error, '');
          });
        } else if (res.isSuccess && res.data) {
          this.reservations = res.data;
        }
      } catch (err) {
        console.error('Xəta baş verdi:', err);
      }
    },
    convertToEditable(user) {
      const updatedEditable = {};
      for (const key in this.editableUser) {
        updatedEditable[key] = {
          value: user[key],
          isEdit: false
        };
      }
      this.editableUser = updatedEditable;
    },
    async EditCustomer() {
      if (!this.isEdit) {
        this.isEdit = true;
        return;
      }

      if (JSON.stringify(this.user) === JSON.stringify(this.originalUser)) {
        this.$notify('warning', 'Heç bir dəyişiklik edilməyib.', '');
        this.isEdit = false;
        return;
      }

      const res = await EditCustomerDetails(this.editableUser);

      if (!res.isSuccess && Array.isArray(res.errors)) {
        res.errors.forEach((error) => {
          this.$notify('warning', error, 'Məlumatlar yadda saxlanılmadı');
        });
      } else if (res.isSuccess) {
        this.isEdit = false;
        this.originalUser = JSON.parse(JSON.stringify(this.user));
        this.$notify('success', 'Məlumatlarınız yadda saxlanıldı', '');
      } else {
        this.$notify('error', 'Məlumatlar yadda saxlanılmadı', '');
        return;
      }
    },
    logout() {
      if (!this.authStore.isAuthenticated) {
        return;
      }

      this.authStore.logout();
      this.router.push({ name: 'Home' })
    },
    async UpdatePassword() {
      if (!this.canSubmitPassword) {
        this.$notify('warning', 'Daxil etdiyiniz yeni şifrə ilə təstiqləmə şifrəsi uyğun gəlmir', '');
        return;
      }
      this.openOtpModal();
      this.startResendTimer();
    },
    openOtpModal() {
      this.showOtpModal = true;
      this.otpDigits = ["", "", "", "", "", ""];
      this.loading = false;
      this.resendTimer = 0;
      if (this.resendInterval) clearInterval(this.resendInterval);
      this.$nextTick(() => {
        this.focusInput(0);
      });
    },
    closeOtpModal() {
      this.showOtpModal = false;
      this.otpDigits = ["", "", "", "", "", ""];
      this.loading = false;
      this.resendTimer = 0;
      if (this.resendInterval) clearInterval(this.resendInterval);
    },
    focusInput(index) {
      const refName = `otp_${index}`;
      let input = this.$refs[refName];

      if (Array.isArray(input)) {
        input = input[0];
      }

      if (input && input.focus) {
        input.focus();
      }
    },
    onOtpInput(index, event) {
      let val = event.target.value;

      if (val.length > 1) {
        val = val.slice(-1);
      }

      if (!/^\d$/.test(val)) {
        this.otpDigits[index] = "";
        return;
      }

      this.otpDigits[index] = val;

      if (index < 5) {
        this.$nextTick(() => {
          this.focusInput(index + 1);
        });
      }
    },
    onOtpBackspace(index, e) {
      if (e.key === "Backspace" && !this.otpDigits[index] && index > 0) {
        this.focusInput(index - 1);
      }
    },
    async verifyOtp() {
      if (!this.isOtpComplete) {
        this.$notify('warning', 'Zəhmət olmasa 6 rəqəmli kodu tam daxil edin.', '');
        return;
      }
      this.loading = true;

      if (this.otpCode === "123456") {
        try {
          const payload = {
            currentPassword: this.passwords.currentPassword,
            newPassword: this.passwords.newPassword
          };

          const res = await UpdateCustomerPassword(payload);

          if (!res.isSuccess && Array.isArray(res.errors)) {
            res.errors.forEach((error) => {
              this.$notify('error', error, '');
            });
            this.otpDigits = ["", "", "", "", "", ""];
            this.$nextTick(() => this.focusInput(0));
            this.loading = false;
            return;
          }
          else if (res.isSuccess) {
            this.closeOtpModal();
            this.passwords = {
              currentPassword: '',
              newPassword: '',
              confirmPassword: ''
            };
            this.$notify('success', 'Şifrəniz yeniləndi!', '');
            this.$nextTick(() => this.focusInput(0));
          } else {
            this.$notify('error', 'Şifrə yenilənmədi!', '');
            this.loading = false;
            return;
          }

        } catch (error) {
          this.$notify('error', 'Şifrə yenilənmədi!', '');
        }
      }
      else {
        this.$notify('warning', 'Yanlış OTP kodu daxil etdiniz.', '');
        this.otpDigits = ["", "", "", "", "", ""];
        this.$nextTick(() => this.focusInput(0));
      }
      this.loading = false;
    },
    resendOtp() {
      if (this.resendTimer === 0) {
        this.startResendTimer();
      }
    },
    startResendTimer() {
      this.resendTimer = 30;
      this.resendInterval = setInterval(() => {
        if (this.resendTimer > 0) {
          this.resendTimer--;
        } else {
          clearInterval(this.resendInterval);
          this.resendInterval = null;
        }
      }, 1000);
    },
    OpenCancelModal(id) {
      const modalEl = document.getElementById('cancelModal');
      const cancelModal = new Modal(modalEl);
      cancelModal.show();
      this.selectedReservationId = id;
    },
    async ConfirmCancelReservation() {
      const reservationId = this.selectedReservationId;
      if (!reservationId) return;


      try {
        const res = await CancelReservation(reservationId);

        if (!res.isSuccess && Array.isArray(res.errors)) {
          res.errors.forEach((error) => {
            this.$notify('error', error, '');
          });
        } else if (res.isSuccess) {
          this.$notify('success', 'Rezervasiyanız ləğv edildi.', '');
          this.customerReservations();
        } else {
          this.$notify('error', 'Rezervasiya ləğv edilə bilmədi.', '');
        }
      }
      catch (error) {
        console.error('Xəta baş verdi:', error);
        this.$notify('error', 'Rezervasiya ləğv edilərkən xəta baş verdi.', '');
      }
      finally {
        this.selectedReservationId = null;
        const modalEl = document.getElementById('cancelModal');
        const cancelModal = Modal.getInstance(modalEl);
        if (cancelModal) {
          cancelModal.hide();
        }
      }
    },
    ShowMore() {
      this.currentStep += this.stepBy;
      if (this.currentStep > this.reservations.length) {
        this.currentStep = this.reservations.length;
      }
    },
    ShowLess() {
      this.currentStep = this.stepBy;
    },
    ShowCancelReservation(statusName) {
      if (!statusName) return false;
      statusName = statusName?.toLowerCase();
      return statusName === 'təsdiq edildi' || statusName === 'gözlənilir';
    },
  },
  computed: {
    tabs() {
      return [
        { name: "info", label: "Məlumatlarım", icon: "fas fa-user" },
        { name: "reservations", label: "Reservasiyalarım", icon: "fas fa-lock" },
        { name: "settings", label: "Tənzimləmələr", icon: "fas fa-bell" },
      ];
    },
    currentTabLabel() {
      const tab = this.tabs.find(t => t.name === this.activeTab);
      return tab ? tab.label : '';
    },
    canSubmitPassword() {
      const p = this.passwords;
      return (
        p.currentPassword &&
        p.newPassword &&
        p.confirmPassword &&
        p.newPassword === p.confirmPassword
      );
    },
    isOtpComplete() {
      return this.otpDigits.every((d) => d.length === 1 && /^\d$/.test(d));
    },
    otpCode() {
      return this.otpDigits.join("");
    },
    total() {
      return this.reservations.length;
    },
    getCards() {
      return this.reservations.slice(0, this.currentStep);
    }
  }
};
</script>

<style scoped>
.otp-inputs input {
  width: 3.5rem;
  height: 3.5rem;
  font-size: 2rem;
  text-align: center;
  border-radius: 0.2rem;
  border-left: none;
  border-right: none;
  border-top: none;
  border-bottom: 2.5px solid #ebecf0;
  transition: border-color 0.3s;
  background-color: #f8f9fa;
}

.otp-inputs input:focus {
  border-color: #3c6943;
  box-shadow: 0 0 8px #daf7e9;
  outline: none;
}

.modal-backdrop {
  z-index: 1040;
}

.modal.fade.show {
  display: block;
  background-color: rgba(0, 0, 0, 0.5);
}

.modal-content {
  border: none;
  border-radius: 1rem;
}

.reservation-card {
  border-radius: 12px;
  overflow: hidden;
  position: relative;
  background: #fff;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
  border: 1px solid #e0e0e0;
}

.reservation-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.08);
  cursor: pointer;
}

.status-badge {
  border-radius: 50px;
  font-weight: 600;
  white-space: nowrap;
  padding: 6px 12px;
  font-size: 0.8rem;
  align-self: flex-start;
}

.info-item {
  margin: 0;
  padding: 0;
  display: flex;
  align-items: center;
}

.info-section {
  display: flex;
  flex-direction: column;
  row-gap: 0px;
}

.label-text {
  font-size: 0.85rem;
  font-weight: 600;
  color: #6c757d;
  margin-right: 6px;
  letter-spacing: 0.5px;
}

.label-text,
.value-text {
  line-height: 1.2;
}

.value-text {
  font-size: 0.95rem;
  font-weight: 600;
  color: #212529;
}

.note-text {
  font-size: 0.85rem;
  font-style: italic;
  color: #6c757d;
}

.modern-service-list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.service-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: #f9f9f9;
  padding: 6px 10px;
  border-radius: 6px;
  margin-top: 4px;
}

.bullet {
  width: 6px;
  height: 6px;
  background-color: #666;
  border-radius: 50%;
  margin-right: 8px;
}

.btn-modern {
  border-radius: 8px;
  font-weight: 600;
  padding: 3px 8px;
  font-size: 0.75rem;
  line-height: 1;
  transition: all 0.3s ease;
}

.btn-modern:hover {
  opacity: 0.9;
}

.modern-service-list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.service-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: #f9f9f9;
  padding: 6px 10px;
  border-radius: 6px;
  margin-top: 4px;
}

.bullet {
  width: 6px;
  height: 6px;
  background-color: #666;
  border-radius: 50%;
  margin-right: 8px;
}

.btn-modern {
  border-radius: 8px;
  font-weight: 600;
  padding: 6px 12px;
  transition: all 0.3s ease;
}

.btn-modern:hover {
  opacity: 0.9;
}


.collapse-enter-active,
.collapse-leave-active {
  transition: height 0.3s ease;
  overflow: hidden;
}

.collapse-enter-from,
.collapse-leave-to {
  height: 0;
  opacity: 0;
}

.collapse-enter-to,
.collapse-leave-from {
  height: auto;
  opacity: 1;
}

.offcanvas.offcanvas-start {
  background-color: #f8f9fa;
  color: #212529;
}

.offcanvas.offcanvas-start .offcanvas-header {
  border-bottom: 1px solid #dee2e6;
}

.offcanvas.offcanvas-start .btn-close {
  filter: none;
}

.offcanvas.offcanvas-start .nav-link {
  color: #495057;
}

.offcanvas.offcanvas-start .nav-link.active {
  background-color: #0d6efd;
  color: #fff;
}

.offcanvas.offcanvas-start .nav-link:hover {
  background-color: #e2e6ea;
  color: #000;
}

.nav-link {
  cursor: pointer;
}

.nav-link.active {
  font-weight: bold;
  color: #0d6efd;
}

.full-height {
  min-height: calc(100vh - 200px);
}

.nav-pills .nav-link {
  cursor: pointer;
  color: #6c757d;
  border-radius: 10px;
  padding: 12px 20px;
  margin: 4px 0;
  transition: all 0.3s ease;
}

.nav-pills .nav-link:hover {
  background-color: #f8f9fa;
}

.nav-pills .nav-link.active {
  background-color: #fff;
  color: #4158D0;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
}

.settings-card {
  border-radius: 15px;
  border: none;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.05);
  transition: all 0.3s ease;
}

.settings-card:hover {
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.1);
}

.form-switch .form-check-input {
  width: 3em;
  height: 1.5em;
  margin-left: -3.5em;
}

.navbar-toggler {
  position: relative;
  width: 2rem;
  height: 2rem;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0;
}

.custom-tab-label {
  color: #4a4a4a;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  font-size: 1rem;
  font-weight: 600;
}

.btn-modern {
  border-radius: 8px;
  font-weight: 600;
  padding: 0.5rem 1.25rem;
  box-shadow: 0 2px 6px rgb(0 0 0 / 0.12);
  transition: background-color 0.3s ease, box-shadow 0.3s ease;
}

.btn-modern:hover {
  box-shadow: 0 4px 12px rgb(0 0 0 / 0.18);
}

.btn-primary.btn-modern:hover {
  background-color: #004aad;
  border-color: #004aad;
}

.btn-success.btn-modern:hover {
  background-color: #2a8a3e;
  border-color: #2a8a3e;
}

.btn-danger.btn-modern:hover {
  background-color: #b7322c;
  border-color: #b7322c;
}

.form-control:focus {
  border-color: #0d6efd !important;
  box-shadow: 0 0 0 0.2rem rgba(179, 207, 250, 0.25) !important;
  outline: none !important;
}

table.table-sm {
  border-collapse: collapse;
}

table.table-sm td {
  padding-top: 0.5px !important;
  padding-bottom: 0.5px !important;
}


fieldset {
  position: relative;
  padding-top: 2.5rem;
  border: 1.5px solid #ced4da;
  border-radius: 0.5rem;
}

legend {
  position: absolute;
  top: 0;
  left: 1rem;
  transform: translateY(-50%);
  background: white;
  padding: 0 0.75rem;
  font-size: 1.1rem;
  font-weight: 600;
  color: #343a40;
  border-radius: 0.5rem;
  user-select: none;
  white-space: nowrap;
}

@media (max-width: 576px) {
  .btn-modern {
    font-size: 12px;
    padding: 6px 10px;
  }

  .form-label {
    font-size: 14px;
    font-weight: 500;
  }

  legend {
    font-size: 1rem;
    left: 0.75rem;
    padding: 0 0.5rem;
  }

  .form-label {
    font-size: 0.9rem;
  }

  .btn-mobile-full {
    width: 100% !important;
  }

  .reservation-card {
    border-radius: 10px;
  }

  .reservation-card .card-body {
    padding: 1rem;
  }

  .label-text {
    font-size: 0.8rem;
  }

  .value-text {
    font-size: 0.9rem;
  }

  .status-badge {
    font-size: 0.75rem;
    padding: 4px 8px;
  }

  .service-toggle-icon {
    font-size: 1rem !important;
  }

  .modal-dialog {
    max-width: 90%;
    margin: auto;
  }

  .modal-content {
    font-size: 14px;
    padding: 10px;
  }

  .modal-header h5 {
    font-size: 16px;
  }

  .modal-footer .btn {
    font-size: 14px;
    padding: 5px 10px;
  }

  .modal-dialog-top {
    display: flex;
    align-items: flex-start;
    margin-top: 20px;
  }
}
</style>
