---
type: "THEORY"
title: "Full CRUD Operations from UI"
---

Let us implement all CRUD (Create, Read, Update, Delete) operations for tasks, connecting each UI action to our Spring Boot API.

The Complete Task Service:

```javascript
// src/services/taskService.js
import api from './api';

export const taskService = {
  // READ - Get all tasks with pagination and filtering
  async getTasks({ page = 0, size = 20, status, priority, categoryId, search } = {}) {
    const params = new URLSearchParams();
    params.append('page', page);
    params.append('size', size);
    if (status) params.append('status', status);
    if (priority) params.append('priority', priority);
    if (categoryId) params.append('categoryId', categoryId);
    if (search) params.append('search', search);
    
    const response = await api.get(`/tasks?${params}`);
    return response.data;
  },

  // READ - Get single task by ID
  async getTask(id) {
    const response = await api.get(`/tasks/${id}`);
    return response.data;
  },

  // CREATE - Create new task
  async createTask(taskData) {
    const response = await api.post('/tasks', taskData);
    return response.data;
  },

  // UPDATE - Update existing task
  async updateTask(id, taskData) {
    const response = await api.put(`/tasks/${id}`, taskData);
    return response.data;
  },

  // UPDATE - Quick status toggle
  async toggleComplete(id, currentStatus) {
    const newStatus = currentStatus === 'COMPLETED' ? 'PENDING' : 'COMPLETED';
    const response = await api.patch(`/tasks/${id}/status`, { status: newStatus });
    return response.data;
  },

  // DELETE - Remove task
  async deleteTask(id) {
    await api.delete(`/tasks/${id}`);
  },
};
```

Using the Service in Components:

```jsx
// src/pages/TasksPage.jsx
import { useState, useEffect, useCallback } from 'react';
import { Link } from 'react-router-dom';
import { taskService } from '../services/taskService';
import TaskList from '../components/tasks/TaskList';
import TaskFilters from '../components/tasks/TaskFilters';
import LoadingSpinner from '../components/common/LoadingSpinner';

export default function TasksPage() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [pagination, setPagination] = useState({ page: 0, totalPages: 0 });
  const [filters, setFilters] = useState({});

  const loadTasks = useCallback(async () => {
    try {
      setLoading(true);
      const data = await taskService.getTasks({ 
        page: pagination.page, 
        ...filters 
      });
      setTasks(data.content);
      setPagination(prev => ({
        ...prev,
        totalPages: data.totalPages,
        totalElements: data.totalElements,
      }));
      setError(null);
    } catch (err) {
      setError('Failed to load tasks');
      console.error(err);
    } finally {
      setLoading(false);
    }
  }, [pagination.page, filters]);

  useEffect(() => {
    loadTasks();
  }, [loadTasks]);

  async function handleDelete(taskId) {
    if (!window.confirm('Delete this task?')) return;
    try {
      await taskService.deleteTask(taskId);
      setTasks(prev => prev.filter(t => t.id !== taskId));
    } catch (err) {
      setError('Failed to delete task');
    }
  }

  async function handleToggleComplete(task) {
    try {
      const updated = await taskService.toggleComplete(task.id, task.status);
      setTasks(prev => prev.map(t => t.id === task.id ? updated : t));
    } catch (err) {
      setError('Failed to update task');
    }
  }

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold">My Tasks</h1>
        <Link
          to="/tasks/new"
          className="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700"
        >
          + New Task
        </Link>
      </div>

      <TaskFilters filters={filters} onFilterChange={setFilters} />

      {error && (
        <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
          {error}
        </div>
      )}

      {loading ? (
        <LoadingSpinner />
      ) : (
        <TaskList
          tasks={tasks}
          onDelete={handleDelete}
          onToggleComplete={handleToggleComplete}
        />
      )}
    </div>
  );
}
```

This pattern keeps components focused on UI while the service handles API communication.