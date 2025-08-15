import api from './api';
import { useAuthStore } from "@/stores/authStore";

const API_BASE = '/Specialties';

export function getSpecialtiesByCompanyType() {
    const authStore = useAuthStore();
    const companyType = authStore.user?.companyType;
    if (!companyType) {
        throw new Error('Company Type ID not found in authStore');
    }
    return api.get(`/Specialties/by-company-type/${companyType}`)
        .then(res => res.data);
}

export function getServicesBySpecialty(specialtyId) {

    const authStore = useAuthStore();
    const companyId = authStore.companyId;
    if (!companyId) {
        throw new Error('Company ID not found in authStore');
    }


    return api.get(`/Specialties/by-specialty/${companyId}/${specialtyId}`)
        .then(res => res.data);
}

export function getDetailsBySpecialty(specialtyId) {

    const authStore = useAuthStore();
    const companyId = authStore.companyId;
    if (!companyId) {
        throw new Error('Company ID not found in authStore');
    }


    return api.get(`/Specialties/details-by-specialty/${companyId}/${specialtyId}`)
        .then(res => res.data);
}


export function getAllSpecialties() {
    const authStore = useAuthStore();
    const companyId = authStore.companyId;
    if (!companyId) {
        throw new Error('Company ID not found in authStore');
    }

    return api.get(`${API_BASE}/${companyId}`)
        .then(res => res.data);
}

export function deleteSpecialty(companySpecialtyId) {
    const authStore = useAuthStore();
    const companyId = authStore.companyId;
    if (!companyId) {
        throw new Error('Company ID not found in authStore');
    }

    return api.delete(`/Specialties/${companyId}/${companySpecialtyId}`)
        .then(res => res.data);
}

export function updateSpeciality(payload) {
    return api.put(`/Specialties/update-specialty`, payload)
        .then(res => res.data);
}

export function updateInSpecialityService(payload) {
    return api.put(`/Specialties/update-inspecialty-service`, payload)
        .then(res => res.data);
}

export function saveSpecialtyWithServices(payload) {
    const authStore = useAuthStore();
    const companyId = authStore.companyId;
    if (!companyId) {
        throw new Error('Company ID not found in authStore');
    }

    const dataToSend = {
        ...payload,
        companyId
    }

    return api.post(`/Specialties/create-with-services`, dataToSend)
        .then(res => res.data);
}

export function deleteSpecialtyService(serviceId) {
    const authStore = useAuthStore();
    const companyId = authStore.companyId;
    if (!companyId) {
        throw new Error('Company ID not found in authStore');
    }

    return api.delete(`/Specialties/delete-service/${companyId}/${serviceId}`)
        .then(res => res.data);

}

export function saveInSpecialtyWithServices(payload) {
    const authStore = useAuthStore();
    const companyId = authStore.companyId;
    if (!companyId) {
        throw new Error('Company ID not found in authStore');
    }

    const dataToSend = {
        ...payload,
        companyId
    }

    return api.post(`/Specialties/inspeciality-create-services`, dataToSend)
        .then(res => res.data);
}

export function updateIsActiveSpecialty(specialtyId) {
    const authStore = useAuthStore();
    const companyId = authStore.companyId;
    if (!companyId) {
        throw new Error('Company ID not found in authStore');
    }

    return api.put(`/Specialties/update-status/${companyId}/${specialtyId}`)
        .then(res => res.data);
}

export function updateIsActiveService(serviceId) {
    const authStore = useAuthStore();
    const companyId = authStore.companyId;
    if (!companyId) {
        throw new Error('Company ID not found in authStore');
    }

    return api.put(`/Specialties/update-service-status/${companyId}/${serviceId}`)
        .then(res => res.data);
}