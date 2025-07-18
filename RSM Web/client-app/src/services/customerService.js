import api from './api';

export const registerCustomer = async (payload) => {
  const response = await api.post('/Customer/register', payload);
  return response.data;
};