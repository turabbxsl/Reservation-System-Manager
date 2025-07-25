<template>
    <div class="card">
        <DataTable :value="users" v-bind:filters="filters" @update:filters="filters = $event" removableSort
            filterDisplay="row" paginator :rows="5" :rowsPerPageOptions="[5, 10, 20, 50]" stripedRows
            filterMode="lenient" tableStyle="min-width: 50rem" selectionMode="single" v-model:selection="selectedUser">
            <template #header>
                <div class="flex justify-between">
                    <Button type="button" icon="pi pi-filter-slash" label="Filtr Təmizlə" outlined
                        @click="clearFilter()" />
                </div>
            </template>
            <template #empty> Məlumat boşdur </template>
            <template #loading> Məlumatlar yüklənir, gözləyin </template>
            <Column field="fullname" sortable filter header="Əməkdaş">
                <template #body="{ data }">
                    {{ data.fullname }}
                </template>
                <template #filter="{ filterModel, filterCallback }">
                    <InputText v-model="filterModel.value" type="text" @input="onDebouncedFilter(filterCallback)"
                        placeholder="əməkdaş adına görə axtar" />
                </template>
            </Column>
            <Column field="username" sortable header="İstifadəçi adı">
                <template #body="{ data }">
                    {{ data.username }}
                </template>
                <template #filter="{ filterModel, filterCallback }">
                    <InputText v-model="filterModel.value" type="text" @input="onDebouncedFilter(filterCallback)"
                        placeholder="istifadəçi adına görə axtar" />
                </template>
            </Column>
            <Column field="email" sortable header="Email">
                <template #body="{ data }">
                    {{ data.email }}
                </template>
                <template #filter="{ filterModel, filterCallback }">
                    <InputText v-model="filterModel.value" type="text" @input="onDebouncedFilter(filterCallback)"
                        placeholder="email-ə görə axtar" />
                </template>
            </Column>
            <Column field="phoneNumber" header="Telefon nömrəsi">
                <template #body="{ data }">
                    {{ data.phoneNumber }}
                </template>
                <template #filter="{ filterModel, filterCallback }">
                    <InputText v-model="filterModel.value" type="text" @input="onDebouncedFilter(filterCallback)"
                        placeholder="telefon nömrəsinə görə axtar" />
                </template>
            </Column>
            <Column field="createdDate" header="Yaradılma"></Column>
            <Column field="updateDate" header="Son dəyişiklik"></Column>
        </DataTable>
    </div>
</template>

<script>
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';
import { getUsersByCompanyId } from '@/services/userService';

export default {
    name: 'RegisterPage',
    components: {
        DataTable,
        Column,
        InputText,
        Button
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
        };
    },
    mounted() {
        this.getUsers();
    },
    methods: {
        async getUsers() {
            try {
                const res = await getUsersByCompanyId();
                if (!res.isSuccess && Array.isArray(res.errors)) {
                    res.errors.forEach((error) => {
                        this.$notify('error', error, '');
                    });
                } else if (res.isSuccess && Array.isArray(res.data)) {
                    this.users = res.data;
                }
            } catch (err) {
                console.error('Xəta baş verdi:', err);
            }
        },
        editUser(user) {
            this.$notify('info', `${user.fullname} redaktə olunur`, '');
        },
        deleteUser() {
            this.$confirm('Bu istifadəçini silmək istədiyinizə əminsiniz?', 'Sil', {
                confirmButtonText: 'Bəli',
                cancelButtonText: 'Xeyr',
                type: 'warning'
            }).then(() => {
                this.$notify('success', 'İstifadəçi uğurla silindi', '');
            }).catch(() => {
                this.$notify('info', 'İstifadəçi silinmədi', '');
            });
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
        rowClass(data) {
            return data === this.selectedUser ? 'selected-row' : '';
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
</style>