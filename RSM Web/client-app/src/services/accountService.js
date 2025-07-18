import api from './api';

export const loginAccount = async (payload) => {
  const response = await api.post('/Auth/login', payload);
  return response.data;
};