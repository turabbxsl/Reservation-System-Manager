import api from './api';

const API_BASE = '/Reservation';

export const getAvailableTimes = async (companyId, serviceId, date) => {
    const response = await api.get(`${API_BASE}/available-times`, {
        params: {
            companyId,
            serviceId,
            date,
        }
    },);
    return response.data;
};