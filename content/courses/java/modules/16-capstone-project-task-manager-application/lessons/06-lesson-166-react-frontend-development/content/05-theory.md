---
type: "THEORY"
title: "React Path: Project Setup with Vite and React 19"
---

REACT PATH -- If you chose Thymeleaf, you can skip the React sections.

We will use Vite as our build tool for the React frontend. Vite offers lightning-fast hot module replacement (HMR) and optimized builds.

Project Creation:
```bash
npm create vite@latest frontend -- --template react
cd frontend
npm install
```

Install Additional Dependencies:
```bash
npm install react-router-dom axios
```

Environment Variables (.env):
```
VITE_API_URL=http://localhost:8080/api
```

Access in code:
```javascript
const API_URL = import.meta.env.VITE_API_URL;
```

API Service Layer with Axios:
Create a centralized API layer that handles authentication and error handling.

```javascript
// src/services/api.js
import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:8080/api';

const api = axios.create({
  baseURL: API_URL,
  headers: { 'Content-Type': 'application/json' },
});

// Request interceptor - add auth token
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Response interceptor - handle 401
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default api;

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

Start the development server with npm run dev. The app runs at http://localhost:5173. Your Spring Boot backend must be running on port 8080 for API calls to work.
