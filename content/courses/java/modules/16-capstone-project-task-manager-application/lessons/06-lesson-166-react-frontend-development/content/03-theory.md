---
type: "THEORY"
title: "API Service Layer with Axios"
---

Create a centralized API layer that handles authentication, error handling, and provides clean methods for components to use.

```javascript
// src/services/api.js
import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:8080/api';

const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor - add auth token
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Response interceptor - handle errors globally
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      // Token expired or invalid
      localStorage.removeItem('token');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default api;

// src/services/authService.js
import api from './api';

export const authService = {
  async login(email, password) {
    const response = await api.post('/auth/login', { email, password });
    const { token, email: userEmail, name } = response.data;
    localStorage.setItem('token', token);
    return { email: userEmail, name };
  },

  async register(email, password, name) {
    const response = await api.post('/auth/register', { email, password, name });
    const { token, email: userEmail, name: userName } = response.data;
    localStorage.setItem('token', token);
    return { email: userEmail, name: userName };
  },

  logout() {
    localStorage.removeItem('token');
  },

  isAuthenticated() {
    return !!localStorage.getItem('token');
  },
};

// src/services/taskService.js
import api from './api';

export const taskService = {
  async getTasks(page = 0, size = 20) {
    const response = await api.get(`/tasks?page=${page}&size=${size}`);
    return response.data;
  },

  async getTask(id) {
    const response = await api.get(`/tasks/${id}`);
    return response.data;
  },

  async createTask(taskData) {
    const response = await api.post('/tasks', taskData);
    return response.data;
  },

  async updateTask(id, taskData) {
    const response = await api.put(`/tasks/${id}`, taskData);
    return response.data;
  },

  async deleteTask(id) {
    await api.delete(`/tasks/${id}`);
  },
};
```

Benefits of this approach:
- Token management is automatic via interceptors
- Consistent error handling across all API calls
- Components stay clean - they just call service methods
- Easy to add loading states, caching, or retry logic centrally