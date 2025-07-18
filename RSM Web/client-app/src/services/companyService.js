import api from './api';

const API_BASE = '/Company';

export const registerCompany = async (payload) => {
    const response = await api.post(`${API_BASE}/register`, payload);
    return response.data;
};

export const getAllCompanies = async () => {
    const response = await api.get(`${API_BASE}/all`);
    return response.data;
};

export const getServicesByCompanyId = async (companyId) => {
    const response = await api.get(`${API_BASE}/${companyId}/services`);
    return response.data;
};