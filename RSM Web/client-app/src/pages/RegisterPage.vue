<template>
    <div class="container py-4">
        <div class="card shadow border-0">
            <div class="card-header bg-white border-bottom-0">
                <ul class="nav nav-tabs nav-fill" role="tablist">
                    <li class="nav-item">
                        <button class="nav-link" :class="{ active: activeTab === 'customer' }"
                            @click="activeTab = 'customer'">
                            Müştəri
                        </button>
                    </li>
                    <li class="nav-item">
                        <button class="nav-link" :class="{ active: activeTab === 'company' }"
                            @click="activeTab = 'company'">
                            Şirkət
                        </button>
                    </li>
                </ul>
            </div>

            <div class="card-body">
                <!-- Müştəri qeydiyyatı -->
                <div v-show="activeTab === 'customer'">
                    <h5 class="mb-3 text-center">Müştəri Qeydiyyatı</h5>
                    <form @submit.prevent="submitForm">
                        <div class="row row-cols-1 row-cols-md-2 g-3">
                            <div>
                                <input type="text" class="form-control" placeholder="Ad Soyad"
                                    v-model="customer.fullName" required />
                            </div>
                            <div>
                                <input type="email" class="form-control" placeholder="Email" v-model="customer.email"
                                    required />
                            </div>
                            <div class="mb-3">
                                <input type="text" class="form-control" v-model="customer.phoneNumber"
                                    v-mask="'(###)###-####'" placeholder="+994 __ ___ __ __" required />
                            </div>
                            <div>
                                <input type="password" class="form-control" placeholder="Şifrə"
                                    v-model="customer.password" required />
                            </div>
                        </div>
                        <button type="submit" class="btn btn-primary w-100 mt-4">Qeydiyyatdan keç</button>
                    </form>
                </div>

                <!-- Şirkət qeydiyyatı -->
                <div v-show="activeTab === 'company'">
                    <h5 class="mb-3 text-center">Şirkət Qeydiyyatı</h5>
                    <form @submit.prevent="submitForm">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Şirkət Adı</label>
                                <input type="text" class="form-control" v-model="company.name" />
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Şirkət Növü</label>
                                <select class="form-select" v-model="company.type">
                                    <option disabled value="">Seçin</option>
                                    <option v-for="type in companyTypes" :key="type.id" :value="type.id">
                                        {{ type.label }}
                                    </option>
                                </select>
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Telefon</label>
                                <input type="text" class="form-control" v-model="company.phone" v-mask="'(###)###-####'"
                                    placeholder="+994 __ ___ __ __" />
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Ünvan</label>
                                <input type="text" class="form-control" v-model="company.address" />
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Website</label>
                                <input type="url" class="form-control" v-model="company.website"
                                    placeholder="https://example.com" />
                            </div>

                            <div class="col-md-3 mb-3">
                                <label class="form-label">Başlama Saatı</label>
                                <select class="form-select" v-model="company.startTime">
                                    <option disabled value="">Seçin</option>
                                    <option v-for="time in timeOptions" :key="time" :value="time">{{ time }}</option>
                                </select>
                            </div>

                            <div class="col-md-3 mb-3">
                                <label class="form-label">Bitmə Saatı</label>
                                <select class="form-select" v-model="company.endTime">
                                    <option disabled value="">Seçin</option>
                                    <option v-for="time in timeOptions" :key="time" :value="time">{{ time }}</option>
                                </select>
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Email</label>
                                <input type="email" class="form-control" v-model="company.email" />
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Şifrə</label>
                                <input type="password" class="form-control" v-model="company.adminPassword" />
                            </div>

                            <div class="col-md-6 mb-3">
                                <label class="form-label">Məsul şəxs ad / soyad</label>
                                <input type="text" class="form-control" v-model="company.adminFullName" />
                            </div>
                        </div>

                        <button type="submit" class="btn btn-primary w-100 mt-3">Qeydiyyatdan Keç</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { registerCompany } from "@/services/companyService";
import { registerCustomer } from "@/services/customerService";

export default {
    name: "RegisterPage",
    data() {
        return {
            activeTab: "customer",
            customer: {
                fullName: "turabbl",
                email: "casfadfsa@gmail.com",
                password: "dasdsad",
                phoneNumber: "21312312312",
            },
            company: {
                name: 'Turab MMC',
                email: 'turabbl@gmail.com',
                phone: '74923794623',
                address: 'dasjdhsauy dsah dsah',
                website: '',
                startTime: '09:00',
                endTime: '18:00',
                type: '0',
                adminPassword: 'turab2025',
                adminFullName: 'Turab Baxisli'
            },
            companyTypes: [
                { id: 0, label: 'Xəstəxana' },
                { id: 1, label: 'Diş Həkimi' },
                { id: 2, label: 'Gözəllik Salonu' },
                { id: 3, label: 'Bərbər' }
            ],
            timeOptions: Array.from({ length: 48 }, (_, i) => {
                const hour = Math.floor(i / 2).toString().padStart(2, '0');
                const minute = i % 2 === 0 ? '00' : '30';
                return `${hour}:${minute}`;
            })
        };
    },
    methods: {
        async submitForm() {
            try {
                if (this.activeTab === 'company') {
                    const payload = {
                        name: this.company.name,
                        email: this.company.email,
                        phone: this.company.phone,
                        address: this.company.address,
                        website: this.company.website,
                        startTime: this.company.startTime,
                        endTime: this.company.endTime,
                        type: parseInt(this.company.type),
                        adminFullName: this.company.adminFullName,
                        adminPassword: this.company.adminPassword
                    };
                    const res = await registerCompany(payload);
                    if (!res.isSuccess && Array.isArray(res.errors)) {
                        res.errors.forEach((error) => {
                            this.$notify('error', error, 'Qeydiyyat Xətası');
                        });
                    }
                    else if (res.isSuccess && res.data) {
                        this.$notify('success', 'Şirkət uğurla qeydiyyatdan keçirildi!', 'Uğurlu Qeydiyyat');
                        this.$router.push('/login');
                    }
                } else {
                    const res = await registerCustomer(this.customer);
                    if (!res.isSuccess && Array.isArray(res.errors)) {
                        res.errors.forEach((error) => {
                            this.$notify('error', error, 'Qeydiyyat Xətası');
                        });
                    }
                    else if (res.isSuccess && res.data) {
                        this.$notify('success', 'Müştəri uğurla qeydiyyatdan keçirildi!', 'Uğurlu Qeydiyyat');
                        this.$router.push('/login');
                    }
                }
            } catch (err) {
                console.log('catch unexpecting error', err);
            }
        },
    },
};
</script>

<style scoped>
.nav-tabs .nav-link {
    cursor: pointer;
}

.card {
    max-width: 800px;
    margin: auto;
}
</style>