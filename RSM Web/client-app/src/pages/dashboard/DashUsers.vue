<template>
    <div class="table-container">
        <DataTable v-model:editingRows="editingRows" :value="users" v-bind:filters="filters"
            @update:filters="filters = $event" resizableColumns columnResizeMode="fit" removableSort filterDisplay="row"
            paginator :rows="20" :rowsPerPageOptions="[5, 10, 20, 50]" stripedRows filterMode="lenient" scrollable
            :tableStyle="{ minWidth: '50rem' }" selectionMode="single" v-model:selection="selectedUser"
            v-model:expandedRows="expandedRows" showGridlines @rowExpand="onRowExpand" editMode="row"
            @row-edit-save="onRowEditSave" @row-edit-init="e => onRowEditServiceInit(e)" :pt="{
                table: { style: 'min-width: 80rem' },
                column: {
                    bodycell: ({ state }) => ({
                        style: state['d_editing'] && 'padding-top: 0.75rem; padding-bottom: 0.75rem'
                    })
                }
            }">
            <template #header>
                <div class="flex justify-between items-center gap-4 flex-nowrap w-full header-row">
                    <div class="flex items-center gap-3 flex-wrap">
                        <Button size="small" type="button" icon="pi pi-plus" label="Yeni əməkdaş əlavə et" outlined
                            @click="openCreateUserModal()" />
                        <ConfirmDialog></ConfirmDialog>
                        <Button size="small" v-if="selectedUser" type="button" icon="pi pi-trash"
                            label="Seçilən əməkdaşı sil" outlined severity="danger" @click="confirmDeleteUser" />
                    </div>

                    <div class="flex items-center gap-3 flex-wrap">
                        <!-- <Button text icon="pi pi-plus" severity="secondary" label="Hamısını Aç" @click="expandAll" />
                        <Button text icon="pi pi-minus" severity="secondary" label="Bağla" @click="collapseAll" /> -->
                        <Button size="small" type="button" icon="pi pi-filter-slash" severity="info"
                            label="Filtr Təmizlə" outlined @click="clearFilter()" />
                    </div>
                </div>
            </template>

            <template #empty>
                <div class="empty-state">
                    <i class="pi pi-info-circle empty-icon"></i>
                    <p class="empty-text">Məlumat mövcud deyil</p>
                </div>
            </template>

            <template #loading> Məlumatlar yüklənir, gözləyin </template>




            <Column expander style="width: 5rem" />

            <Column field="fullname" sortable filter header="Əməkdaş">
                <template #body="{ data }">
                    {{ data.fullname }}
                </template>
                <template #filter="{ filterModel, filterCallback }">
                    <InputText v-model="filterModel.value" type="text" @input="onDebouncedFilter(filterCallback)"
                        placeholder="əməkdaş adı" class="p-column-filter" />
                </template>
                <template #editor="{ data, field }">
                    <InputText v-model="data[field]" fluid />
                </template>
            </Column>
            <Column field="username" sortable header="İstifadəçi adı">
                <template #body="{ data }">
                    {{ data.username }}
                </template>
                <template #filter="{ filterModel, filterCallback }">
                    <InputText v-model="filterModel.value" type="text" @input="onDebouncedFilter(filterCallback)"
                        placeholder="istifadəçi adı" class="p-column-filter" />
                </template>
                <template #editor="{ data, field }">
                    <InputText v-model="data[field]" fluid />
                </template>
            </Column>
            <Column field="email" sortable header="Email">
                <template #body="{ data }">
                    {{ data.email }}
                </template>
                <template #filter="{ filterModel, filterCallback }">
                    <InputText v-model="filterModel.value" type="text" @input="onDebouncedFilter(filterCallback)"
                        placeholder="email" class="p-column-filter" />
                </template>
                <template #editor="{ data, field }">
                    <InputText v-model="data[field]" fluid />
                </template>
            </Column>
            <Column field="phoneNumber" header="Telefon nömrəsi">
                <template #body="{ data }">
                    {{ data.phoneNumber }}
                </template>
                <template #filter="{ filterModel, filterCallback }">
                    <InputText v-model="filterModel.value" type="text" @input="onDebouncedFilter(filterCallback)"
                        placeholder="telefon nömrə" class="p-column-filter" />
                </template>
                <template #editor="{ data, field }">
                    <InputMask v-model="data[field]" mask="(99)-999-99-99" placeholder="(00)-000-00-00" fluid />
                </template>
            </Column>
            <Column field="createdDate" header="Yaradılma"></Column>
            <!-- <Column field="updateDate" header="Son dəyişiklik"></Column> -->
            <Column field="role.label" header="Rol">
                <template #editor="{ data }">
                    <Dropdown v-model="data.role.value" :options="roleOptions" optionLabel="label" optionValue="value"
                        placeholder="Rol seçin" class="w-full" />
                </template>
            </Column>
            <Column field="status" header="Status">
                <template #body="{ data }">
                    <span class="status-badge" :class="{
                        'status-active': data.status === 'Aktiv',
                        'status-inactive': data.status === 'Deaktiv'
                    }">
                        <i v-if="data.status === 'Aktiv'" class="pi pi-check-circle mr-1"></i>
                        <i v-else class="pi pi-times-circle mr-1"></i>
                        {{ data.status }}
                    </span>
                </template>
            </Column>
            <Column :rowEditor="true" header="Əməliyyat" bodyStyle="text-align:center">
                <template #roweditoriniticon>
                    <span class="rowedit-btn edit-btn">Düzəliş et</span>
                </template>
                <template #roweditorsaveicon>
                    <span class="rowedit-btn save-btn">Yadda saxla</span>
                </template>
                <template #roweditorcancelicon>
                    <span class="rowedit-btn cancel-btn">Ləğv et</span>
                </template>
            </Column>

            <template #expansion="slotProps">
                <div class="p-4">
                    <div class="mb-3 d-flex justify-content-end align-items-center">
                        <Button size="small" icon="pi pi-plus" label="Yeni Xidmət Kateqoriya Əlavə Et"
                            @click="openSpecialtyModal(slotProps.data)" severity="success" />
                    </div>

                    <div class="table-container">
                        <DataTable :value="slotProps.data.specialities" :tableStyle="{ minWidth: '50rem' }" scrollable>

                            <Column field="specialityName" header="Xidmət Kateqoriya Adı" sortable></Column>

                            <Column header="Əməliyyat" :style="{ width: '5rem' }" bodyStyle="text-align:center">
                                <template #body="{ data }">
                                    <div class="d-flex justify-content-center">
                                        <button type="button"
                                            class="btn btn-sm btn-danger d-flex align-items-center justify-content-center"
                                            style="width: 3.5rem; height: 2.5rem;"
                                            @click="confirmDeleteSpecialty(data, slotProps.data)">
                                            Sil <i class="bi bi-trash ms-2" style="font-size: 0.8rem;"></i>
                                        </button>
                                    </div>
                                </template>
                            </Column>

                            <Column field="services" header="Xidmətlər">
                                <template #body="{ data }">
                                    <div class="container-fluid">
                                        <div class="row mb-2">
                                            <div class="col text-end">
                                                <Button size="small" icon="pi pi-plus" label="Yeni Xidmət Əlavə Et"
                                                    @click="openServiceModal(slotProps.data, data)"
                                                    severity="success" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col">
                                                <div v-if="data.services.length" class="list-group">
                                                    <div v-for="service in data.services.filter(s =>
                                                        !(tempDeletedMap[slotProps.data.id] || []).includes(s.id)
                                                    )" :key="service.id"
                                                        class="list-group-item d-flex justify-content-between align-items-center">
                                                        <span>{{ service.name }}</span>
                                                        <button type="button"
                                                            class="btn btn-sm btn-danger d-flex align-items-center justify-content-center"
                                                            style="width: 3.5rem; height: 2.5rem;"
                                                            @click="confirmDeleteService(service, slotProps.data)">
                                                            Sil <i class="bi bi-trash ms-2"
                                                                style="font-size: 0.8rem;"></i>
                                                        </button>

                                                    </div>
                                                </div>

                                                <div v-else
                                                    class="d-flex justify-content-center align-items-center bg-light text-muted py-3 rounded">
                                                    <i class="pi pi-info-circle me-2"></i>
                                                    Xidmət mövcud deyil
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </template>
                            </Column>

                            <template #footer>
                                <div v-if="!slotProps.data.specialities.length" class="empty-footer">
                                    <i class="pi pi-info-circle"></i>
                                    <span>Bu istifadəçinin xidmətləri mövcud deyil</span>
                                </div>
                            </template>
                        </DataTable>
                    </div>
                </div>
            </template>

            <Dialog v-model:visible="showSpecialtyModal" header="Yeni Xidmət Kateqoriyası Seçimi" :modal="true"
                :style="{ width: '30vw' }">
                <div class="p-fluid service-form">
                    <div class="form-field">
                        <label for="specialities">Xidmət kateqoriyaları seçin:</label>
                        <MultiSelect v-model="selectedSpecialties" :options="allSpecialities" optionLabel="name"
                            optionValue="id" placeholder="kateqoriyanı seçin"
                            :optionDisabled="option => option.disabled" />
                    </div>
                </div>

                <template #footer>
                    <Button size="small" label="Əlavə et" icon="pi pi-check" @click="AddSpecialities" />
                    <Button size="small" label="Bağla" icon="pi pi-times" class="p-button-text"
                        @click="showSpecialtyModal = false" />
                </template>
            </Dialog>

            <Dialog v-model:visible="showServiceModal" header="Yeni Xidmət Seçimi" :modal="true"
                :style="{ width: '30vw' }">
                <div class="p-fluid service-form">
                    <div class="form-field">
                        <label for="services">Xidmətləri seçin:</label>
                        <MultiSelect v-model="selectedServices" :options="allServices" optionLabel="name"
                            optionValue="id" placeholder="xidməti seçin" :optionDisabled="option => option.disabled" />
                    </div>
                </div>

                <template #footer>
                    <Button size="small" label="Əlavə et" icon="pi pi-check"
                        :disabled="specServices?.length == existingServices?.length" @click="AddServices" />
                    <Button size="small" label="Bağla" icon="pi pi-times" class="p-button-text"
                        @click="showServiceModal = false" />
                </template>
            </Dialog>

            <Dialog v-model:visible="showCreateDialog" header="Yeni əməkdaş əlavə et" modal :style="{ width: '450px' }">
                <div class="p-fluid user-form">
                    <div class="form-field">
                        <label for="fullName">Əməkdaş Adı</label>
                        <InputText id="fullName" v-model="newUser.fullName" />
                    </div>
                    <div class="form-field">
                        <label for="username">İstifadəçi Adı</label>
                        <InputText id="username" v-model="newUser.username" />
                    </div>
                    <div class="form-field">
                        <label for="email">Email</label>
                        <InputText id="email" v-model="newUser.email" />
                        <small v-if="errors.emailError" class="text-danger">{{ errors.emailError }}</small>
                    </div>
                    <div class="form-field">
                        <label for="phone">Telefon</label>
                        <InputMask id="phone" v-model="newUser.phone" mask="(99)-999-99-99"
                            placeholder="(__)___-__-__" />
                        <small v-if="errors.phoneError" class="text-danger">{{ errors.phoneError }}</small>
                    </div>
                    <div class="form-field">
                        <label for="specialty">İxtisas seçin:</label>
                        <Select v-model="newUser.selectedSpecialty" :options="specialties" optionLabel="name"
                            optionValue="id" placeholder="İxtisas seçin" @update:modelValue="loadServicesBySpecialty"
                            class="w-full" />
                    </div>
                    <div class="form-field">
                        <label for="services">Xidmətləri seçin:</label>
                        <MultiSelect id="services" v-model="newUser.selectedServices" :options="services"
                            optionLabel="name" optionValue="id" placeholder="Xidmət seçin" class="w-full" />
                    </div>
                </div>

                <template #footer>
                    <Button size="small" label="Əlavə et" icon="pi pi-check" @click="createUser()" autofocus />
                    <Button size="small" label="Bağla" icon="pi pi-times" @click="showCreateDialog = false" text />
                </template>
            </Dialog>

            <Dialog v-model:visible="showDeleteDialog" header="Xidməti silmək istədiyinizə əminsiniz?" modal>
                <p>{{ deleteTarget?.service?.name }}</p>
                <template #footer>
                    <Button label="İmtina" icon="pi pi-times" class="p-button-text" @click="cancelDelete" />
                    <Button label="Sil" icon="pi pi-check" class="p-button-danger" @click="deleteConfirmed" />
                </template>
            </Dialog>

        </DataTable>
    </div>
</template>

<script>
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';
import Dialog from 'primevue/dialog';
import MultiSelect from 'primevue/multiselect';
import InputMask from 'primevue/inputmask';
import ConfirmDialog from 'primevue/confirmdialog';
import Dropdown from 'primevue/dropdown';
import { Select } from 'primevue';
import moment from 'moment';
import 'moment/locale/az';

import { getUsersByCompanyId, deleteUserServices, addUserServices, createUserPost, deleteStaffMember, updateStaffMember, deleteServiceByStaffMemberId, deleteSpecialtyByStaffMemberId, addUserSpecialities } from '@/services/userService';
import { getServicesByCompanyId, getSpecialtiesByCompanyId } from '@/services/companyService';
import { getSpecialtiesByCompanyType, getDetailsBySpecialty, getServicesBySpecialty } from '@/services/specialtyService';
import { useAuthStore } from "@/stores/authStore";

export default {
    name: 'RegisterPage',
    components: {
        DataTable,
        Column,
        InputText,
        Button,
        Dialog,
        MultiSelect,
        InputMask,
        Select,
        ConfirmDialog,
        Dropdown
    },
    data() {
        return {
            users: [],
            filters: {
                fullname: { value: null, matchMode: 'contains' },
                username: { value: null, matchMode: 'contains' },
                email: { value: null, matchMode: 'contains' },
                phoneNumber: { value: null, matchMode: 'contains' },
                createdDate: { value: null, matchMode: 'dateEquals' },
                updateDate: { value: null, matchMode: 'dateEquals' }
            },
            selectedUser: null,
            rowSelectedSpecialty: null,
            specServices: [],
            expandedRows: [],
            tempDeletedMap: {},
            showServiceModal: false,
            showSpecialtyModal: false,
            showCreateDialog: false,
            allServices: [],
            allSpecialities: [],
            selectedServices: [],
            selectedSpecialties: [],
            existingSpecialties: [],
            existingServices: [],
            newUser: {
                fullName: '',
                username: '',
                email: '',
                phone: '',
                selectedServices: [],
                selectedSpecialty: null,
            },
            errors: {
                emailError: '',
                phoneError: ''
            },
            specialties: [],
            services: [],
            editingRows: [],
            roleOptions: [
                { label: 'Admin', value: 'CompanyAdmin' },
                { label: 'İstifadəçi', value: 'CompanyUser' },
                { label: 'Nəzarətçi', value: 'CompanySupervisor' },
            ],
            showDeleteDialog: false,
            deleteTarget: {
                type: 'service' | 'specialty',
                data: {},
                user: {}
            }
        };
    },
    mounted() {
        this.getUsers();
    },
    methods: {
        async deleteConfirmed() {
            const { type, data, user } = this.deleteTarget;

            if (!user || !data) {
                this.$notify('warn', 'Item seçilməyib', '');
                return;
            }

            try {
                if (type === 'service')
                    await this.deleteServiceConfirmed(data, user);
                else if (type === 'specialty')
                    await this.deleteSpecialtyConfirmed(data, user);
            } catch (error) {
                console.error(error);
                this.$notify('error', 'Silinərkən xəta baş verdi', '');
            } finally {
                this.showDeleteDialog = false;
                this.deleteTarget = null;
            }
        },
        confirmDeleteService(service, user) {
            this.deleteTarget = {
                type: 'service',
                data: service,
                user: user
            };
            this.showDeleteDialog = true;
        },
        confirmDeleteSpecialty(specialty, user) {
            this.deleteTarget = {
                type: 'specialty',
                data: specialty,
                user: user
            };
            this.showDeleteDialog = true;
        },
        async deleteServiceConfirmed(service, user) {

            if (!user || !service) {
                this.$notify('warn', 'Xidmət və ya istifadəçi seçilməyib', '');
                return;
            }

            try {
                const res = await deleteServiceByStaffMemberId(user.id, service.id);
                if (!res.isSuccess && Array.isArray(res.errors)) {
                    res.errors.forEach((error) => {
                        this.$notify('error', error, '');
                    });
                } else if (res.isSuccess) {
                    this.$notify('success', 'Xidmət silindi', '');
                    this.getUsers();
                }
                else
                    this.$notify('error', 'Xidmət silinərkən xəta baş verdi', '');
            }
            catch (error) {
                console.error('Xidmət silinərkən xəta:', error);
                this.$notify('error', 'Xidmət silinərkən gözlənilməz xəta baş verdi', '');
                return;
            }

            const services = user.services || [];
            user.services = services.filter(s => s.id !== service.id);

            this.showDeleteDialog = false;
            this.deleteTarget = null;
        },
        async deleteSpecialtyConfirmed(specialty, user) {
            if (!user || !specialty) {
                this.$notify('warn', 'Xidmət kateqoriyası və ya istifadəçi seçilməyib', '');
                return;
            }

            try {
                const res = await deleteSpecialtyByStaffMemberId(user.id, specialty.specialityId);
                if (!res.isSuccess && Array.isArray(res.errors)) {
                    res.errors.forEach((error) => {
                        this.$notify('error', error, '');
                    });
                } else if (res.isSuccess) {
                    this.$notify('success', 'Xidmət kateqoriyası silindi', '');
                    this.getUsers();
                } else {
                    this.$notify('error', 'Xidmət kateqoriyası silinərkən xəta baş verdi', '');
                }
            } catch (error) {
                console.error('Xidmət kateqoriyası silinərkən xəta:', error);
                this.$notify('error', 'Xidmət kateqoriyası silinərkən gözlənilməz xəta baş verdi', '');
                return;
            }
        },
        cancelDelete() {
            this.showDeleteDialog = false;
            this.deleteTarget = null;
        },
        async getUsers() {
            try {
                const res = await getUsersByCompanyId();
                if (!res.isSuccess && Array.isArray(res.errors)) {
                    res.errors.forEach((error) => {
                        this.$notify('error', error, '');
                    });
                } else if (res.isSuccess && Array.isArray(res.data)) {
                    this.users = res.data.map(user => ({
                        ...user,
                        createdDate: user.createdDate
                            ? moment.utc(user.createdDate).local().format('DD-MM-YYYY HH:mm:ss')
                            : null,
                        updateDate: user.updateDate
                            ? moment.utc(user.updateDate).local().format('DD-MM-YYYY HH:mm:ss')
                            : null
                    }));
                }
            } catch (err) {
                console.error('Xəta baş verdi:', err);
            }
        },
        async getAllServices() {
            try {
                const authStore = useAuthStore();
                const companyId = authStore.user?.companyId;

                const res = await getServicesByCompanyId(companyId);

                if (res.isSuccess && Array.isArray(res.data)) {
                    this.allServices = res.data.map(s => ({
                        id: s.id ?? s.Id,
                        name: s.name ?? s.Name,
                        price: s.price ?? s.Price
                    }));
                }
            } catch (error) {
                console.error("getAllServices error:", error);
            }
        },
        async getAllSpecialties() {
            try {
                const authStore = useAuthStore();
                const companyId = authStore.user?.companyId;

                const res = await getSpecialtiesByCompanyId(companyId);

                if (res.isSuccess && Array.isArray(res.data)) {
                    this.allSpecialities = res.data.map(s => ({
                        id: s.specialtyId,
                        name: s.name
                    }));
                }
            } catch (error) {
                console.error("getAllSpecialities error:", error);
            }
        },
        async createUser() {
            try {
                const authStore = useAuthStore();
                const companyId = authStore.user?.companyId;

                const payload = {
                    fullName: this.newUser.fullName,
                    username: this.newUser.username,
                    email: this.newUser.email,
                    phone: this.newUser.phone,
                    specialtyId: this.newUser.selectedSpecialty,
                    serviceIds: this.newUser.selectedServices,
                    companyId: companyId
                };
                const response = await createUserPost(payload);
                if (response.isSuccess) {
                    this.$notify('success', 'Yeni əməkdaş uğurla əlavə edildi', '');
                    this.showCreateDialog = false;

                    this.newUser = {
                        fullName: '',
                        username: '',
                        email: '',
                        phone: '',
                        selectedServices: [],
                        selectedSpecialty: null,
                    };

                    await this.getUsers();
                } else {
                    response.errors.forEach(err => this.$notify('error', err, 'Əməkdaş əlavə edilə bilmədi'));
                }
            } catch (error) {
                this.$notify('error', 'Əməkdaş əlavə edilərkən gözlənilməz xəta baş verdi', '');
            }
        },
        async confirmDeleteUser() {
            if (!this.selectedUser) {
                this.$notify('warn', 'Silmək üçün əməkdaş seçilməyib', '');
                return;
            }

            this.$confirm.require({
                message: 'Bu əməkdaşı silmək istədiyinizə əminsiniz?',
                header: 'Əməkdaş silmə təsdiqi',
                icon: 'pi pi-exclamation-triangle',
                acceptLabel: 'Bəli',
                rejectLabel: 'Xeyr',
                acceptClass: 'p-button-danger',
                accept: async () => {
                    const authStore = useAuthStore();
                    const companyId = authStore.user?.companyId;
                    const userId = this.selectedUser.id;

                    const res = await deleteStaffMember(companyId, userId);

                    if (res.isSuccess) {
                        this.$notify('success', 'Əməkdaş uğurla silindi', '');
                        await this.getUsers();
                    } else {
                        if (Array.isArray(res.errors)) {
                            res.errors.forEach(err =>
                                this.$notify('error', err, 'Silmə əməliyyatı uğursuz oldu')
                            );
                        }
                    }
                }
            });
        },
        onRowEditSave(event) {
            let { newData, index } = event;

            const payload = {
                id: newData.id,
                fullName: newData.fullname,
                username: newData.username,
                email: newData.email,
                phoneNumber: newData.phoneNumber,
                role: newData.role.value
            };

            updateStaffMember(newData.id, payload)
                .then((res) => {
                    if (res.isSuccess) {
                        this.$notify("success", "İstifadəçi uğurla yeniləndi", "");
                        this.getUsers();

                        this.users[index] = { ...this.users[index], ...newData };
                    } else {
                        res.errors?.forEach(err =>
                            this.$notify("error", err, "Yeniləmə uğursuz oldu")
                        );
                    }
                })
                .catch(() => {
                    this.$notify("error", "Gözlənilməz xəta baş verdi", "");
                });
        },
        onRowEditServiceInit(e) {
            this.selectedUser = e.data;
        },
        onDebouncedFilter(callback) {
            clearTimeout(this.debounceTimer);
            this.debounceTimer = setTimeout(() => {
                callback();
            }, 300);
        },
        clearFilter() {
            this.filters = {
                fullname: { value: null, matchMode: 'contains' },
                username: { value: null, matchMode: 'contains' },
                email: { value: null, matchMode: 'contains' },
                phoneNumber: { value: null, matchMode: 'contains' },
                createdDate: { value: null, matchMode: 'dateEquals' },
                updateDate: { value: null, matchMode: 'dateEquals' }
            };
        },
        expandAll() {
            this.expandedRows = this.users.slice();
        },
        onRowExpand(event) {
            this.expandedRows.push(event.data);
            this.selectedUser = event.data;
        },
        collapseAll() {
            this.expandedRows = [];
        },
        async AddServices() {
            if (!this.selectedUser || !Array.isArray(this.selectedServices) || this.selectedServices.length === 0) {
                this.$notify('warn', 'Xidmət seçilməyib və ya istifadəçi təyin edilməyib', '');
                return;
            }

            try {
                const res = await addUserServices(this.selectedUser.id, this.rowSelectedSpecialty.specialityId, this.selectedServices);
                if (res.isSuccess) {
                    this.$notify('success', 'Xidmətlər uğurla əlavə olundu', '');
                    this.showServiceModal = false;
                    this.selectedServices = [];

                    await this.getUsers();
                } else {
                    res.errors?.forEach(err => {
                        this.$notify('error', err, 'Xidmətlər əlavə edilə bilmədi');
                    });
                }
            } catch (error) {
                console.error('Xidmət əlavə edərkən xəta:', error);
                this.$notify('error', 'Xidmət əlavə edilərkən gözlənilməz xəta baş verdi', '');
            }
            this.showServiceModal = false;
            this.selectedServices = [];
            this.getUsers();
            this.allServices = [];
        },
        async AddSpecialities() {
            if (!this.selectedUser || !Array.isArray(this.selectedSpecialties) || this.selectedSpecialties.length === 0) {
                this.$notify('warn', 'Xidmət kateqoriyası seçilməyib və ya istifadəçi təyin edilməyib', '');
                return;
            }

            try {
                const res = await addUserSpecialities(this.selectedUser.id, this.selectedSpecialties);
                if (res.isSuccess) {
                    this.$notify('success', 'Xidmət kateqoriyaları əlavə olundu', '');
                    this.showSpecialtyModal = false;
                    this.selectedSpecialties = [];

                    await this.getUsers();
                } else {
                    res.errors?.forEach(err => {
                        this.$notify('error', err, 'Xidmət kateqoriyaları əlavə edilə bilmədi');
                    });
                }
            } catch (error) {
                console.error('Xidmət kateqoriyası əlavə edərkən xəta:', error);
                this.$notify('error', 'Xidmət kateqoriyası əlavə edilərkən gözlənilməz xəta baş verdi', '');
            }
        },
        async deleteService() {
            let totalDeleted = 0;

            for (const [userId, serviceIds] of Object.entries(this.tempDeletedMap)) {
                const deletedServices = serviceIds.map(id => ({ id }));

                if (!deletedServices.length) {
                    this.$notify('warn', 'Silinəcək xidmət seçilməyib', '');
                    continue;
                }

                try {
                    const res = await deleteUserServices(userId, serviceIds);

                    if (!res.isSuccess && Array.isArray(res.errors)) {
                        res.errors.forEach((error) => {
                            this.$notify('error', error, '');
                        });
                    } else {
                        this.$notify('success', `${deletedServices.length} xidmət silindi`, '');
                        totalDeleted += deletedServices.length;
                    }

                } catch (err) {
                    console.error(`Xidmət silinərkən xəta: ${userId}`, err);
                }
            }

            this.tempDeletedMap = {};

            await this.getUsers();

            this.expandedRows = [...this.expandedRows];

            if (totalDeleted === 0) {
                this.$notify('info', 'Heç bir xidmət silinmədi', '');
            }
        },
        async openSpecialtyModal(user) {
            this.selectedUser = user;
            await this.getAllSpecialties();

            const existingSpecialties = user.specialities
                .map(spec => {

                    const specialty = this.allSpecialities.find(
                        s => s.id === spec.specialityId
                    );
                    if (!specialty) return null;

                    return {
                        id: specialty.id,
                        name: specialty.name
                    };
                });

            this.existingSpecialties = existingSpecialties.filter(s => s !== null);
            this.selectedSpecialties = this.existingSpecialties.map(s => s.id);

            this.allSpecialities = this.allSpecialities.map(spec => ({
                ...spec,
                disabled: this.existingSpecialties.some(e => e.id === spec.id)
            }));

            this.showSpecialtyModal = true;
        },
        async openServiceModal(user, data) {
            this.selectedUser = user;
            this.rowSelectedSpecialty = data;

            const res = await getServicesBySpecialty(this.rowSelectedSpecialty.specialityId);
            if (res.isSuccess) {
                this.specServices = res.data;

                this.allServices = this.specServices.map(s => ({
                    id: s.id,
                    name: s.name
                }));


                const existingServices = user.specialities
                    .flatMap(spec => spec.services || [])
                    .map(serv => {
                        const service = this.allServices.find(s => s.id === serv.id);
                        if (!service) return null;
                        return {
                            id: service.id,
                            name: service.name
                        };
                    })
                    .filter(s => s !== null);


                this.existingServices = existingServices.filter(s => s !== null);
                this.selectedServices = this.existingServices.map(s => s.id);

                this.allServices = this.allServices.map(serv => ({
                    ...serv,
                    disabled: this.existingServices.some(e => e.id === serv.id)
                }));

                this.showServiceModal = true;
            } else {
                res.errors.forEach(err => this.$notify('error', err, 'Xidmətlər gətirilə bilmədi'));
            }
        },
        async openCreateUserModal() {
            this.showCreateDialog = true;
            this.getAllServices().then(() => {
                this.newUser.selectedServices = [];
            });

            const res = await getSpecialtiesByCompanyType();
            if (res.isSuccess) {
                this.specialties = res.data;
            } else {
                res.errors.forEach(err => this.$notify('error', err, 'İxtisaslar gətirilə bilmədi'));
            }
        },
        async loadServicesBySpecialty() {
            if (!this.newUser.selectedSpecialty) return;

            const res = await getDetailsBySpecialty(this.newUser.selectedSpecialty);
            if (res.isSuccess) {
                this.services = res.data;
            } else {
                res.errors.forEach(err => this.$notify('error', err, 'Xidmətlər gətirilə bilmədi'));
            }
        }
    }
};
</script>

<style scoped>
.btn-edit {
    background-color: #6c7ae0;
    border: none;
    padding: 6px 12px;
    color: white;
    border-radius: 4px;
    cursor: pointer;
    margin-right: 5px;
    font-size: 13px;
    transition: background-color 0.3s ease;
}

.btn-edit:hover {
    background-color: #4a55c7;
}

.btn-delete {
    background-color: #e76f51;
    border: none;
    padding: 6px 12px;
    color: white;
    border-radius: 4px;
    cursor: pointer;
    font-size: 13px;
    transition: background-color 0.3s ease;
}

.btn-delete:hover {
    background-color: #c14a2c;
}

.header-row {
    display: flex;
    justify-content: space-between;
    align-items: center;
    gap: 1rem;
    flex-wrap: nowrap;
}

.p-button {
    margin: 0.2rem;
}

.new-service-btn {
    border-radius: 8px;
    padding: 0.5rem 1rem;
    font-size: 0.9rem;
    box-shadow: 0 2px 6px rgba(0, 128, 0, 0.15);
    transition: all 0.3s ease;
}

.new-service-btn:hover {
    box-shadow: 0 2px 10px rgba(0, 128, 0, 0.25);
}

.user-form {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}

.form-field label {
    font-weight: 500;
    margin-bottom: 0.3rem;
}

.form-field {
    display: flex;
    flex-direction: column;
}

.service-form {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}

.p-column-filter {
    width: 100% !important;
}

.p-column-filter input,
.p-column-filter .p-inputtext {
    width: 100% !important;
    box-sizing: border-box;
}

.custom-danger-btn {
    background-color: #d32f2f !important;
    border-color: #d32f2f !important;
    color: white !important;
}

.empty-footer {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 0.5rem;
    padding: 1rem;
    font-size: 0.95rem;
    color: #6b7280;
    background-color: #f9fafb;
    border-radius: 6px;
    margin: 0.5rem;
}

.empty-footer i {
    font-size: 1.2rem;
    color: #9ca3af;
}

.empty-state {
    text-align: center;
    padding: 2rem;
    color: #6b7280;
    font-size: 1.1rem;
}

.empty-icon {
    font-size: 2rem;
    color: #9ca3af;
    margin-bottom: 0.5rem;
    display: block;
}

.empty-text {
    margin: 0;
    font-weight: 500;
}

.status-badge {
    display: inline-flex;
    align-items: center;
    gap: 6px;
    align-items: center;
    padding: 4px 8px;
    border-radius: 12px;
    font-weight: 500;
    font-size: 0.9rem;
}

.status-active {
    background-color: #d4f8e8;
    color: #0f5132;
    border: 1px solid #a3e4c0;
}

.status-inactive {
    background-color: #fde2e2;
    color: #842029;
    border: 1px solid #f5b5b5;
}

:deep(.p-datatable-row-editor-init),
:deep(.p-datatable-row-editor-save),
:deep(.p-datatable-row-editor-cancel) {
    width: auto !important;
    height: auto !important;
    border-radius: 6px !important;
    padding: 0.45rem 0.9rem !important;
    border: none;
    cursor: pointer;
    background-color: #e62051;
    transition: background-color 0.2s ease, color 0.2s ease;
}

:deep(.rowedit-btn) {
    font-size: 0.9rem;
    font-weight: 500;
    letter-spacing: 0.3px;
}

:deep(.edit-btn) {
    color: #4a90e2;
}

:deep(.p-datatable-row-editor-init:hover .edit-btn) {
    color: #357abd;
}

:deep(.save-btn) {
    color: #5cb85c;
}

:deep(.p-datatable-row-editor-save:hover .save-btn) {
    color: #449d44;
}

:deep(.cancel-btn) {
    color: #d9534f;
}

:deep(.p-datatable-row-editor-cancel:hover .cancel-btn) {
    color: #c9302c;
}

.table-container {
    max-width: calc(100vw - 22.5rem);
    overflow-x: auto;
    overflow-y: auto;
}
</style>