import api from './api';

const API_BASE = '/Reservation';

export const getAvailableTimes = async (companyId, specialtyId, date) => {
    const response = await api.get(`${API_BASE}/available-times`, {
        params: {
            companyId,
            specialtyId,
            date,
        }
    },);
    return response.data;
};

export const createReservation = async (reservationData) => {
    console.log('Creating reservation with data:', reservationData);
    const response = await api.post(`${API_BASE}/create`, reservationData);
    return response.data;
}

export function getServicesBySpecialtyWithCompanyId(companyId, specialtyId) {

    if (!companyId) {
        throw new Error('Company ID not found in authStore');
    }

    return api.get(`/Specialties/by-specialty/${companyId}/${specialtyId}`)
        .then(res => res.data);
}