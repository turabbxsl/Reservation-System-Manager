import api from './api';
import { useAuthStore } from "@/stores/authStore";

const API_BASE = '/Users';

export const getUsersByCompanyId = async () => {
    const authStore = useAuthStore();
    const companyId = authStore.companyId;

    if (!companyId)
        throw new Error('Company ID not found in authStore');

    const response = await api.get(`${API_BASE}/${companyId}/users`);
    return response.data;
};

export const deleteUserServices = async (userId, serviceIds) => {
    if (!userId || !Array.isArray(serviceIds) || serviceIds.length === 0) {
        throw new Error('User ID və xidmət siyahısı düzgün deyil');
    }
    const response = await api.post(`${API_BASE}/users/${userId}/services/delete`, serviceIds);
    return response.data;
};

export const addUserServices = async (userId, specialtyId, serviceIds) => {
    if (!userId || !Array.isArray(serviceIds) || serviceIds.length === 0) {
        throw new Error('User ID və xidmət siyahısı düzgün deyil');
    }
    const response = await api.post(`${API_BASE}/users/${userId}/${specialtyId}/services/add`, serviceIds);
    return response.data;
};

export const createUserPost = async (payload) => {
    const response = await api.post(`${API_BASE}/create`, payload);
    return response.data;
}

export const deleteStaffMember = async (companyId, userId) => {
    const response = await api.delete(`${API_BASE}/staffmembers/${companyId}/${userId}`);
    return response.data;
}

export const updateStaffMember = async (userId, payload) => {

    const authStore = useAuthStore();
    const companyId = authStore.companyId;

    if (!companyId)
        throw new Error('Company ID not found in authStore');
    console.log('Updating staff member:', payload);
    const response = await api.put(
        `${API_BASE}/${companyId}/${userId}`,
        payload
    );
    return response.data;
};

export const deleteServiceByStaffMemberId = async (userId, serviceId) => {
    if (!userId || !serviceId) {
        throw new Error('User ID və xidmət ID düzgün deyil');
    }
    const response = await api.delete(`${API_BASE}/staffmembers/${userId}/services/${serviceId}`);
    return response.data;
}

export const deleteSpecialtyByStaffMemberId = async (userId, specialtyId) => {
    if (!userId || !specialtyId) {
        throw new Error('User ID və xidmət kateqoriyası ID düzgün deyil');
    }
    const response = await api.delete(`${API_BASE}/staffmembers/${userId}/specialties/${specialtyId}`);
    return response.data;
}

export const addUserSpecialities = async (userId, specialtyIds) => {
    if (!userId || !Array.isArray(specialtyIds) || specialtyIds.length === 0) {
        throw new Error('User ID və xidmət kateqoriyası siyahısı düzgün deyil');
    }
    const response = await api.post(`${API_BASE}/users/${userId}/specialties/add`, specialtyIds);
    return response.data;
}