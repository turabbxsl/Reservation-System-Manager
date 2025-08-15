<template>
    <div class="card">
        <DataTable class="my-compact-table" v-model:editingRows="editingRows" :value="specialties"
            v-model:expandedRows="expandedRows" v-model:selection="selectedSpecialty" selectionMode="single"
            dataKey="specialtyId" @row-click="toggleRowExpansion" scrollable tableStyle="min-width: 50rem"
            @rowExpand="onRowExpand" paginator :rows="20" :rowsPerPageOptions="[5, 10, 20, 50]" stripedRows
            editMode="row" @row-edit-save="onRowEditSave" :pt="{
                table: { style: 'min-width: 50rem' },
                column: {
                    bodycell: ({ state }) => ({
                        style: state['d_editing'] && 'padding-top: 0.75rem; padding-bottom: 0.75rem'
                    })
                }
            }">
            <template #empty>
                <div class="empty-state">
                    <i class="pi pi-info-circle empty-icon"></i>
                    <p class="empty-text">Məlumat mövcud deyil</p>
                </div>
            </template>
            <template #loading> Məlumatlar yüklənir, gözləyin </template>
            <template #header>
                <div class="flex justify-between items-center gap-4">
                    <Button size="small" type="button" icon="pi pi-plus" label="Yeni Xidmət Kateqoriyası Əlavə Et"
                        outlined @click="openCreateSpecialtyModal" />
                </div>
            </template>
            <Column expander style="width: 3rem" />
            <Column field="name" header="Xidmət Kateqoriyası" sortable>
                <template #editor="{ data, field }">
                    <InputText v-model="data[field]" fluid />
                </template>
            </Column>
            <Column field="restMinute" header="Fasilə müddəti" sortable>
                <template #body="{ data, field }">
                    {{ data[field] }} dəqiqə
                </template>

                <template #editor="{ data, field }">
                    <InputText v-model="data[field]" fluid />
                </template>
            </Column>

            <Column field="isActive" header="Bağlı / Açıq" sortable>
                <template #body="{ data }">
                    <InputSwitch v-model="data.isActive" @change="onSpecialtyStatusChange(data)" />
                </template>
            </Column>

            <Column :rowEditor="true" style="width: 11%; min-width: 8rem" bodyStyle="text-align:center">
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
            <Column header="" style="width: 6rem; text-align:center">
                <template #body="{ data }">
                    <Button icon="pi pi-trash" class="p-button-text p-button-danger"
                        @click="selectedSpecialty = data; confirmDeleteSpecialty()" />
                </template>
            </Column>

            <ConfirmDialog></ConfirmDialog>
            <template #expansion="slotProps">
                <div class="p-4">
                    <div class="mb-3 d-flex justify-content-end">
                        <Button size="small" icon="pi pi-plus" label="Yeni Xidmət Əlavə Et"
                            @click="openInSpecialtyServiceModal(slotProps.data)" severity="success" />
                    </div>

                    <DataTable v-model:editingRows="editingServiceRows" :value="slotProps.data.services"
                        tableStyle="min-width: 30rem" editMode="row"
                        @row-edit-init="e => onRowEditServiceInit(e, slotProps.data)"
                        @row-edit-save="onRowEditServiceSave" :pt="{
                            table: { style: 'min-width: 50rem' },
                            column: {
                                bodycell: ({ state }) => ({
                                    style: state['d_editing'] && 'padding-top: 0.75rem; padding-bottom: 0.75rem'
                                })
                            }
                        }">
                        <Column field="name" header="Xidmət Adı" sortable>
                            <template #editor="{ data, field }">
                                <InputText v-model="data[field]" fluid />
                            </template>
                        </Column>
                        <Column field="price" header="Qiymət" sortable>
                            <template #body="slotProps">
                                <span>{{ slotProps.data.price }} AZN</span>
                            </template>
                            <template #editor="{ data, field }">
                                <InputText v-model="data[field]" fluid />
                            </template>
                        </Column>
                        <Column field="duration" header="Xidmət vaxtı" sortable>
                            <template #body="slotProps">
                                <span>{{ slotProps.data.duration }} dəqiqə</span>
                            </template>
                            <template #editor="{ data, field }">
                                <InputText v-model="data[field]" fluid />
                            </template>
                        </Column>
                        <Column field="isActive" header="Bağlı / Açıq" sortable>
                            <template #body="{ data }">
                                <InputSwitch v-model="slotProps.data.isActive" @change="onServiceStatusChange(data)" />
                            </template>
                        </Column>
                        <Column :rowEditor="true" style="width: 11%; min-width: 8rem" bodyStyle="text-align:center">
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
                        <Column header="" style="width: 6rem; text-align:center">
                            <template #body="{ data }">
                                <ConfirmDialog></ConfirmDialog>
                                <Button icon="pi pi-trash" class="p-button-text p-button-danger"
                                    @click="selectedService = data; selectedSpecialty = slotProps.data; confirmDeleteSpecialtyService()" />
                            </template>
                        </Column>

                        <template #empty>
                            <div class="empty-footer">
                                <i class="pi pi-info-circle"></i>
                                <span>Kateqoriyanın xidmətləri boşdur</span>
                            </div>
                        </template>
                        <template #loading> Məlumatlar yüklənir, gözləyin </template>
                    </DataTable>
                </div>
            </template>

            <Dialog v-model:visible="createSpecialtyDialog" header="Yeni Xidmət Kateqoriyası Əlavə Et" modal
                :style="{ width: '30rem', maxWidth: '95vw' }">
                <div class="p-fluid">
                    <!-- Xidmət Kateqoriyası Adı -->
                    <div class="form-field mb-4">
                        <label for="specialtyName" class="font-semibold">Xidmət Kateqoriyası
                            Adı</label>
                        <Dropdown v-model="selectedOptionSpecialty" :options="specialtyOptions" optionLabel="name"
                            placeholder="Seçin və ya yeni ad daxil edin" editable class="w-full"
                            @update:modelValue="loadServicesBySpecialty" />
                        <small class="text-color-secondary">
                            Əgər siyahıda uyğun xidmət kateqoriyası yoxdursa, yeni ad daxil edin.
                        </small>
                    </div>

                    <!-- Xidmət(lər) -->
                    <div v-if="selectedOptionSpecialty && (typeof selectedOptionSpecialty === 'string' ? selectedOptionSpecialty.trim() !== '' : true)"
                        class="field">
                        <div class="mb-3">
                            <label for="serviceName" class="form-label fw-semibold">Xidmət(lər)</label>
                            <div class="form-field mb-4">
                                <Dropdown v-model="newServiceName" :options="servicesOptions" optionLabel="name"
                                    optionValue="name" placeholder="Seçin və ya yeni ad daxil edin" editable
                                    class="w-full" />
                                <small class="text-color-secondary">
                                    Əgər siyahıda uyğun xidmət yoxdursa, yeni xidmət adı daxil edin.
                                </small>
                            </div>
                            <div class="row g-2">
                                <div class="col-12 col-md-12 mb-1">
                                    <InputNumber v-model="newServicePrice" placeholder="Qiymət" mode="decimal" :min="0"
                                        :step="0.01" :minFractionDigits="2" :maxFractionDigits="2" suffix=" ₼" />
                                </div>
                                <div class="col-12 col-md-12">
                                    <label for="serviceDuration" class="font-semibold">Xidmət
                                        vaxtı</label>
                                    <InputNumber v-model="newServiceDurationMinute"
                                        placeholder="xidmət olunacaq vaxtı yazın" mode="decimal" :min="0" :step="5"
                                        suffix=" dəqiqə" />
                                </div>
                                <div class="col-12 col-md-12">
                                    <Button label="Əlavə et" icon="pi pi-plus" @click="addServiceToList"
                                        class="w-100" />
                                </div>
                            </div>

                            <small class="text-muted d-block mt-1">
                                Xidmət adını daxil edin və "Əlavə et" düyməsini basın.
                            </small>
                        </div>

                        <div v-if="Array.isArray(tempServices) && tempServices.length > 0" class="mt-3">
                            <h5 class="mb-3">Əlavə olunan xidmətlər</h5>
                            <div class="d-flex flex-wrap gap-2">
                                <Chip v-for="(srv, index) in tempServices" :key="index" :label="srv.name" removable
                                    @remove="removeTempService(index)" />
                            </div>
                        </div>

                    </div>
                </div>

                <!-- Footer -->
                <template #footer>
                    <Button size="small" label="Ləğv et" icon="pi pi-times" @click="createSpecialtyDialog = false"
                        text />
                    <Button size="small" label="Əlavə et" :loading="loading" icon="pi pi-check" @click="saveSpecialty"
                        autofocus />
                </template>
            </Dialog>

            <Dialog v-model:visible="inSpecilatyaddServiceDialog" header="Yeni Xidmət(lər) Əlavə Et" modal
                :style="{ width: '30rem' }">
                <div class="p-fluid">
                    <div class="form-field mb-4">
                        <label for="serviceName" class="font-semibold">Xidmət adı</label>
                        <Dropdown v-model="inSpecilatynewServiceName" :options="inSpecialtyServicesOptions"
                            optionLabel="name" optionValue="name" placeholder="Seçin və ya yeni ad daxil edin" editable
                            class="w-full" />
                        <small class="text-color-secondary">
                            Əgər siyahıda uyğun xidmət yoxdursa, yeni xidmət adı daxil edin.
                        </small>
                    </div>
                    <div class="form-field mb-4">
                        <label for="servicePrice" class="font-semibold">Qiymət</label>
                        <InputNumber v-model="inSpecilatynewServicePrice" placeholder="xidmətin qiymətini yazın"
                            mode="decimal" :min="0" :step="0.01" :minFractionDigits="2" :maxFractionDigits="2"
                            suffix=" ₼" />
                    </div>
                    <div class="form-field mb-4">
                        <label for="serviceDuration" class="font-semibold">Xidmət vaxtı</label>
                        <InputNumber v-model="inSpecilatynewServiceDurationMinute"
                            placeholder="xidmət olunacaq vaxtı yazın" mode="decimal" :min="0" :step="5"
                            suffix=" dəqiqə" />
                    </div>
                    <Button size="small" label="Siyahıya əlavə et" icon="pi pi-plus" @click="inSpecilatyaddTempService"
                        class="mb-4" />

                    <div v-if="inSpecilatytempNewServices.length">
                        <h5>Əlavə olunacaq xidmətlər</h5>
                        <div class="d-flex flex-wrap gap-2">
                            <Chip v-for="(srv, index) in inSpecilatytempNewServices" :key="index" :label="srv.name"
                                removable @remove="inSpecilatytempNewServices.splice(index, 1)" />
                        </div>
                    </div>
                </div>

                <template #footer>
                    <Button size="small" label="Ləğv et" icon="pi pi-times" @click="inSpecilatyaddServiceDialog = false"
                        text />
                    <Button size="small" label="Əlavə et" icon="pi pi-check" @click="saveNewServices" autofocus />
                </template>
            </Dialog>


        </DataTable>
    </div>
</template>

<script>
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import Button from "primevue/button";
import Dialog from "primevue/dialog";
import Dropdown from "primevue/dropdown";
import InputNumber from "primevue/inputnumber"
import InputText from "primevue/inputtext";
import InputSwitch from "primevue/inputswitch";
import { Chip } from "primevue";

import ConfirmDialog from 'primevue/confirmdialog';

import { getAllSpecialties, getServicesBySpecialty, deleteSpecialty, getSpecialtiesByCompanyType, saveSpecialtyWithServices, deleteSpecialtyService, saveInSpecialtyWithServices, updateSpeciality, updateInSpecialityService, getDetailsBySpecialty, updateIsActiveSpecialty } from '@/services/specialtyService';

export default {
    name: "ServiceManagementPage",
    components: { DataTable, Column, Button, ConfirmDialog, Dialog, Dropdown, Chip, InputNumber, InputText, InputSwitch },
    data() {
        return {
            specialties: [],
            specialtyOptions: [],
            servicesOptions: [],
            inSpecialtyServicesOptions: [],
            expandedRows: {},
            selectedSpecialty: null,
            selectedService: null,
            createSpecialtyDialog: false,
            selectedOptionSpecialty: null,
            tempServices: [],
            newServiceName: '',
            newServicePrice: '',
            newServiceDurationMinute: 0,
            inSpecilatyaddServiceDialog: false,
            inSpecilatytempNewServices: [],
            inSpecilatynewServiceName: '',
            inSpecilatynewServicePrice: '',
            inSpecilatynewServiceDurationMinute: 0,
            editingRows: [],
            editingServiceRows: []
        };
    },
    mounted() {
        this.getSpecialties();
    },
    methods: {
        async getSpecialties() {
            try {
                const res = await getAllSpecialties();
                if (res.isSuccess && Array.isArray(res.data)) {
                    this.specialties = res.data.map(s => ({
                        ...s,
                        services: []
                    }));
                } else {
                    res.errors?.forEach(err => this.$notify("error", err, "Specialty yüklənmədi"));
                }
            } catch (error) {
                this.$notify("error", "Xidmət kateqoriyaları yüklənərkən xəta baş verdi", "");
            }
        },
        async onRowExpand(event) {
            const specialty = event.data;
            console.log("Row expanded", specialty);
            try {
                const res = await getDetailsBySpecialty(specialty.specialtyId);
                if (res.isSuccess && Array.isArray(res.data)) {
                    specialty.services = res.data;
                } else {
                    specialty.services = [];
                    res.errors?.forEach(err => this.$notify("error", err, "Xidmətlər yüklənmədi"));
                }
            } catch (error) {
                specialty.services = [];
                this.$notify("error", "Xidmətlər yüklənərkən xəta baş verdi", "");
            }
        },
        toggleRowExpansion(event) {
            const rowData = event.data;

            const expanded = { ...this.expandedRows };

            if (expanded[rowData.specialtyId]) {
                delete expanded[rowData.specialtyId];
            } else {
                expanded[rowData.specialtyId] = true;
            }

            this.expandedRows = expanded;
        },
        async confirmDeleteSpecialty() {
            if (!this.selectedSpecialty) {
                this.$notify('warn', 'Silmək üçün xidmət kateqoriyası seçilməyib', '');
                return;
            }

            this.$confirm.require({
                message: 'Bu xidmət kateqoriyasını silmək istədiyinizə əminsiniz?',
                header: 'Xidmət kateqoriyası silmə təsdiqi',
                icon: 'pi pi-exclamation-triangle',
                acceptLabel: 'Bəli',
                rejectLabel: 'Xeyr',
                acceptClass: 'p-button-danger',
                accept: async () => {

                    const specialtyId = this.selectedSpecialty.specialtyId;
                    const res = await deleteSpecialty(specialtyId);

                    if (res.isSuccess) {
                        this.$confirm.close();
                        this.$notify('success', 'Xidmət kateqoriyası silindi', '');
                        await this.getSpecialties();
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
        async openCreateSpecialtyModal() {
            this.selectedOptionSpecialty = null;
            this.createSpecialtyDialog = true;
            this.tempServices = [];
            this.newServiceName = '';
            this.newServicePrice = '';
            await this.loadSpecialtyOptions();
        },
        async openInSpecialtyServiceModal(specialty) {
            this.selectedSpecialty = specialty;
            this.inSpecilatytempNewServicestempNewServices = [];
            this.inSpecialtyServicesOptions = [];
            this.inSpecilatynewServiceName = '';
            this.inSpecilatynewServicePrice = '';
            this.inSpecilatynewServiceDurationMinute = 0;
            this.inSpecilatyaddServiceDialog = true;

            const res = await getServicesBySpecialty(this.selectedSpecialty.specialtyId);
            if (res.isSuccess) {
                this.inSpecialtyServicesOptions = res.data;
            } else {
                res.errors.forEach(err => this.$notify('error', err, 'Xidmətlər gətirilə bilmədi'));
            }
        },
        async loadSpecialtyOptions() {

            try {
                const res = await getSpecialtiesByCompanyType();
                if (res.isSuccess && Array.isArray(res.data)) {
                    this.specialtyOptions = res.data;
                } else {
                    this.$notify('error', 'Xidmət kateqoriyaları yüklənmədi', '');
                }
            } catch (error) {
                this.$notify('error', 'Xidmət kateqoriyaları yüklənərkən xəta baş verdi', '');
            }
        },
        addServiceToList() {
            if (!this.newServiceName || !this.newServiceName.trim()) {
                this.$notify('warn', 'Xidmət adı boş ola bilməz', '');
                return;
            }

            const newService = {
                name: this.newServiceName.trim(),
                price: this.newServicePrice != null ? this.newServicePrice : 0,
                duration: this.newServiceDurationMinute != null ? this.newServiceDurationMinute : 30
            };

            this.tempServices.push(newService);
            this.newServiceName = '';
            this.newServicePrice = '';
            this.newServiceDurationMinute = 0;
        },
        inSpecilatyaddTempService() {
            if (!this.inSpecilatynewServiceName || !this.inSpecilatynewServiceName.trim()) {
                this.$notify('warn', 'Xidmət adı boş ola bilməz', '');
                return;
            }

            this.inSpecilatytempNewServices.push({
                name: this.inSpecilatynewServiceName.trim(),
                price: this.inSpecilatynewServicePrice != null ? this.inSpecilatynewServicePrice : 0,
                duration: this.inSpecilatynewServiceDurationMinute != null ? this.inSpecilatynewServiceDurationMinute : 30
            });

            this.inSpecilatynewServiceName = '';
            this.inSpecilatynewServicePrice = '';
            this.inSpecilatynewServiceDurationMinute = 0;
        },
        removeTempService(index) {
            this.tempServices.splice(index, 1);
        },
        async saveSpecialty() {
            if (!this.selectedOptionSpecialty || (typeof this.selectedOptionSpecialty === 'string' && !this.selectedOptionSpecialty.trim())) {
                this.$notify('warn', 'Xidmət kateqoriyası adı boş ola bilməz', '');
                return;
            }

            let nameToSave = typeof this.selectedOptionSpecialty === 'string'
                ? this.selectedOptionSpecialty.trim()
                : this.selectedOptionSpecialty.name;

            if (this.tempServices.length === 0) {
                this.$notify('warn', 'Xidmət kateqoriyasına ən azı bir xidmət əlavə edilməlidir', '');
                return
            }

            const payload = {
                specialtyName: nameToSave,
                services: this.tempServices
            }

            try {
                const res = await saveSpecialtyWithServices(payload);
                if (res.isSuccess) {
                    this.$notify('success', 'Xidmət kateqoriyası və xidmətlər uğurla əlavə edildi', '');
                    this.createSpecialtyDialog = false;
                    this.tempServices = [];
                    this.selectedOptionSpecialty = null;
                    this.newServiceName = '';
                    this.newServicePrice = '';
                    this.getSpecialties();
                } else {
                    if (Array.isArray(res.errors)) {
                        res.errors.forEach(err => this.$notify('error', err, 'Yadda saxlanma zamanı xəta baş verdi'));
                    } else {
                        this.$notify('error', 'Yadda saxlanma zamanı xəta baş verdi', '');
                    }
                }
            }
            catch (error) {
                this.$notify('error', 'Serverə qoşularkən xəta baş verdi', '');
            }
        },
        async saveNewServices() {
            if (this.inSpecilatytempNewServices.length === 0) {
                this.$notify('warn', 'Ən azı bir xidmət əlavə edilməlidir', '');
                return
            }

            const payload = {
                services: this.inSpecilatytempNewServices,
                specialityId: this.selectedSpecialty.specialtyId
            }

            try {
                const res = await saveInSpecialtyWithServices(payload);
                if (res.isSuccess) {
                    this.$notify('success', 'Əlavə edildi', '');
                    this.inSpecilatytempNewServices = [];
                    this.inSpecilatyaddServiceDialog = false;
                    await this.onRowExpand({ data: this.selectedSpecialty });
                }
                else {
                    if (Array.isArray(res.errors)) {
                        res.errors.forEach(err => this.$notify('error', err, 'Yadda saxlanma zamanı xəta baş verdi'));
                    } else {
                        this.$notify('error', 'Yadda saxlanma zamanı xəta baş verdi', '');
                    }
                }
            } catch (error) {
                this.$notify('error', 'Serverə qoşularkən xəta baş verdi', '');
            }

        },
        async confirmDeleteSpecialtyService() {
            this.$confirm.require({
                message: 'Bu xidməti silmək istədiyinizə əminsiniz?',
                header: 'Xidmət silmə təsdiqi',
                icon: 'pi pi-exclamation-triangle',
                acceptLabel: 'Bəli',
                rejectLabel: 'Xeyr',
                acceptClass: 'p-button-danger',
                accept: async () => {
                    const res = await deleteSpecialtyService(this.selectedService.id);

                    if (res.isSuccess) {
                        this.$notify('success', 'Xidmət silindi', '');
                        await this.onRowExpand({ data: this.selectedSpecialty });
                        this.$confirm.close();
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
        async loadServicesBySpecialty() {
            if (this.selectedOptionSpecialty.id) {
                console.log(this.selectedOptionSpecialty)
                const res = await getServicesBySpecialty(this.selectedOptionSpecialty.id);
                if (res.isSuccess) {
                    this.servicesOptions = res.data;
                } else {
                    res.errors.forEach(err => this.$notify('error', err, 'Xidmətlər gətirilə bilmədi'));
                }
            }
            else {
                this.servicesOptions = [];
            }
        },
        async onRowEditSave(event) {

            let { newData } = event;

            const payload = {
                id: newData.specialtyId,
                newSpecialityName: newData.name,
                restminute: newData.restMinute
            };
            try {
                const res = await updateSpeciality(payload);
                if (res.isSuccess) {
                    this.$notify("success", "Xidmət kateqoriyası yeniləndi", "");
                    this.getSpecialties();
                } else {
                    res.errors.forEach(err => this.$notify('error', err, 'Xidmət kateqoriyası yenilənə bilmədi'));
                }
            } catch (error) {
                this.$notify('error', 'Serverə qoşularkən xəta baş verdi', '');
            }
        },
        async onRowEditServiceSave(event) {
            let { newData } = event;

            const payload = {
                serviceId: newData.id,
                serviceName: newData.name,
                price: newData.price,
                duration: newData.duration
            };

            try {
                const res = await updateInSpecialityService(payload);
                if (res.isSuccess) {
                    this.$notify("success", "Xidmət yeniləndi", "");
                    await this.onRowExpand({ data: this.selectedSpecialty });
                } else {
                    res.errors.forEach(err => this.$notify('error', err, 'Xidmət yenilənə bilmədi'));
                }
            } catch (error) {
                this.$notify('error', 'Serverə qoşularkən xəta baş verdi', '');
            }
        },
        onRowEditServiceInit(event, specialty) {
            this.selectedService = event.data;
            this.selectedSpecialty = specialty;
        },
        async onSpecialtyStatusChange(specialty) {
            try {
                const specId = specialty.specialtyId;
                // Update the specialty status
                const res = await updateIsActiveSpecialty(specId);
                const text = specialty.isActive ? 'aktiv' : 'deaktiv';

                if (res.isSuccess) {
                    this.$notify('success', `Xidmət kateqoriyası ${text} edildi`, '');
                    await this.getSpecialties();
                } else {
                    res.errors.forEach(err => this.$notify('error', err, 'Status yenilənə bilmədi'));
                }
            } catch (error) {
                this.$notify('error', 'Serverə qoşularkən xəta baş verdi', '');
            }
        },
        async onServiceStatusChange(service) {
            try {
                const res = await updateIsActiveService(service.id);
                const text = service.isActive ? 'aktiv' : 'deaktiv';

                if (res.isSuccess) {
                    this.$notify('success', `Xidmət ${text} edildi`, '');
                    await this.onRowExpand({ data: this.selectedSpecialty });
                } else {
                    res.errors.forEach(err => this.$notify('error', err, 'Status yenilənə bilmədi'));
                }
            } catch (error) {
                this.$notify('error', 'Serverə qoşularkən xəta baş verdi', '');
            }
        }
    },
};
</script>

<style scoped>
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

.p-inputnumber {
    width: 100%;
}

.p-inputnumber-input {
    border: 1px solid #ced4da;
    border-radius: 0.375rem;
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
}

.p-inputnumber-input:focus {
    border-color: #86b7fe;
    box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
}

.my-compact-table {
    --p-datatable-body-cell-padding: 4px 8px;
    --p-datatable-header-cell-padding: 6px 8px;
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

:deep(.p-datatable-row-editor-init),
:deep(.p-datatable-row-editor-save),
:deep(.p-datatable-row-editor-cancel) {
    width: auto !important;
    height: 28px !important;
    line-height: 28px !important;
    padding: 0 12px !important;
    border-radius: 6px !important;
    border: none;
    cursor: pointer;
    background-color: #e62051;
    transition: background-color 0.2s ease, color 0.2s ease;
    display: inline-flex;
    align-items: center;
    justify-content: center;
}

:deep(.rowedit-btn) {
    font-size: 0.9rem;
    font-weight: 500;
    letter-spacing: 0.3px;
    line-height: normal;
}

:deep(.p-button-text.p-button-danger) {
    height: 65px !important;
    min-height: 65px !important;
    line-height: 65px !important;
    padding: 0.25rem 0.5rem !important;
    font-size: 0.9rem !important;
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

:deep(.small-btn) {
    padding: 0.25rem 0.5rem !important;
    font-size: 0.85rem !important;
    height: 28px !important;
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
</style>
