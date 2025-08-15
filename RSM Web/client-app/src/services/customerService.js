import api from './api';
import { useAuthStore } from "@/stores/authStore";

const API_BASE = '/Customer';

export const registerCustomer = async (payload) => {
  const response = await api.post(`${API_BASE}/register`, payload);
  return response.data;
};

export const getCustomerDetails = async () => {
  const authStore = useAuthStore();
  const customerId = authStore.user?.id;
  if (!customerId) {
    throw new Error('customer ID not found in authStore');
  }

  const response = await api.get(`${API_BASE}/details/${customerId}`);
  return response.data;
};

export const getCustomerReservations = async () => {
  const authStore = useAuthStore();
  const customerId = authStore.user?.id;
  if (!customerId) {
    throw new Error('customer ID not found in authStore');
  }

  const response = await api.get(`${API_BASE}/reservations/${customerId}`);
  return response.data;
};

export const EditCustomerDetails = async (customerData) => {
  const authStore = useAuthStore();
  const customerId = authStore.user?.id;
  if (!customerId) {
    throw new Error('customer ID not found in authStore');
  }

  const response = await api.put(`${API_BASE}/edit/${customerId}`, customerData);
  return response.data;
}

export const UpdateCustomerPassword = async (passwordData) => {
  const authStore = useAuthStore();
  const customerId = authStore.user?.id;
  if (!customerId) {
    throw new Error('customer ID not found in authStore');
  }

  const response = await api.put(`${API_BASE}/update-password/${customerId}`, passwordData);
  return response.data;
}

export const CancelReservation = async (reservationId) => {
  const authStore = useAuthStore();
  const customerId = authStore.user?.id;
  if (!customerId) {
    throw new Error('customer ID not found in authStore');
  }

  const payload = {
    reservationId: reservationId,
    status: 6
  };

  const response = await api.put(`${API_BASE}/cancel-reservation`, payload);
  return response.data;
}