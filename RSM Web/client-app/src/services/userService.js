import api from './api';
import { useAuthStore } from "@/stores/authStore";

const API_BASE = '/Users';

export const getUsersByCompanyId = async () => {
    const authStore = useAuthStore();
    const companyId = authStore.user?.companyId;

    if (!companyId)
        throw new Error('Company ID not found in authStore');


    const response = await api.get(`${API_BASE}/${companyId}/users`);
    return response.data;
};