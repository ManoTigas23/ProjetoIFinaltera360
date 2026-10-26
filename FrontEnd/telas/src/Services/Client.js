import axios from 'axios';

export const client = axios.create({
    baseURL: 'https://localhost:7185/api',
});

client.interceptors.request.use((config) => {
    const token = localStorage.getItem('@App:token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});