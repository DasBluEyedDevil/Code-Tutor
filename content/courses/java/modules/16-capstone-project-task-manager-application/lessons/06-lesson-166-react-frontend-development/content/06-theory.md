---
type: "THEORY"
title: "React Path: Components, Hooks, and State Management"
---

REACT PATH (continued)

React 19.x builds UIs from reusable components. Each component manages its own state using hooks.

TaskList Component:
```jsx
// src/components/tasks/TaskList.jsx
import { useState, useEffect } from 'react';
import { taskService } from '../../services/taskService';
import TaskCard from './TaskCard';

export default function TaskList() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    loadTasks();
  }, []);

  async function loadTasks() {
    try {
      setLoading(true);
      const response = await taskService.getTasks();
      setTasks(response.content);
    } catch (err) {
      setError('Failed to load tasks.');
    } finally {
      setLoading(false);
    }
  }

  async function handleDelete(taskId) {
    if (!window.confirm('Delete this task?')) return;
    try {
      await taskService.deleteTask(taskId);
      setTasks(tasks.filter(t => t.id !== taskId));
    } catch (err) {
      setError('Failed to delete task.');
    }
  }

  if (loading) return <div>Loading...</div>;

  return (
    <div className="space-y-6">
      {error && <div className="bg-red-100 text-red-700 p-3 rounded">{error}</div>}
      {tasks.length === 0 ? (
        <p className="text-center text-gray-500 py-12">No tasks yet.</p>
      ) : (
        <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
          {tasks.map(task => (
            <TaskCard key={task.id} task={task} onDelete={() => handleDelete(task.id)} onUpdate={loadTasks} />
          ))}
        </div>
      )}
    </div>
  );
}
```

TaskForm Component:
```jsx
// src/components/tasks/TaskForm.jsx
import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { taskService } from '../../services/taskService';

export default function TaskForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const isEditMode = Boolean(id);

  const [formData, setFormData] = useState({
    title: '', description: '', status: 'PENDING',
    priority: 'MEDIUM', dueDate: '', categoryId: '',
  });
  const [errors, setErrors] = useState({});
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (isEditMode) {
      taskService.getTask(id).then(task => setFormData({
        title: task.title, description: task.description || '',
        status: task.status, priority: task.priority,
        dueDate: task.dueDate || '', categoryId: task.category?.id || '',
      }));
    }
  }, [id]);

  function handleChange(e) {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
    if (errors[name]) setErrors(prev => ({ ...prev, [name]: null }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    if (!formData.title.trim()) {
      setErrors({ title: 'Title is required' });
      return;
    }
    setLoading(true);
    try {
      const data = { ...formData, categoryId: formData.categoryId || null, dueDate: formData.dueDate || null };
      if (isEditMode) await taskService.updateTask(id, data);
      else await taskService.createTask(data);
      navigate('/tasks');
    } catch (err) {
      setErrors(err.response?.data?.errors || { general: 'Failed to save task.' });
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className="max-w-2xl mx-auto">
      <h1 className="text-2xl font-bold mb-6">{isEditMode ? 'Edit Task' : 'Create New Task'}</h1>
      <form onSubmit={handleSubmit} className="space-y-6">
        <div>
          <label className="block text-sm font-medium mb-1">Title *</label>
          <input type="text" name="title" value={formData.title} onChange={handleChange}
                 className="w-full border rounded px-3 py-2" />
          {errors.title && <p className="text-red-500 text-sm mt-1">{errors.title}</p>}
        </div>
        <div>
          <label className="block text-sm font-medium mb-1">Description</label>
          <textarea name="description" value={formData.description} onChange={handleChange}
                    rows={4} className="w-full border rounded px-3 py-2" />
        </div>
        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium mb-1">Priority</label>
            <select name="priority" value={formData.priority} onChange={handleChange}
                    className="w-full border rounded px-3 py-2">
              <option value="LOW">Low</option>
              <option value="MEDIUM">Medium</option>
              <option value="HIGH">High</option>
              <option value="URGENT">Urgent</option>
            </select>
          </div>
          <div>
            <label className="block text-sm font-medium mb-1">Due Date</label>
            <input type="date" name="dueDate" value={formData.dueDate} onChange={handleChange}
                   className="w-full border rounded px-3 py-2" />
          </div>
        </div>
        <div className="flex gap-4">
          <button type="submit" disabled={loading}
                  className="bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700 disabled:opacity-50">
            {loading ? 'Saving...' : (isEditMode ? 'Update Task' : 'Create Task')}
          </button>
          <button type="button" onClick={() => navigate('/tasks')}
                  className="bg-gray-200 px-6 py-2 rounded hover:bg-gray-300">Cancel</button>
        </div>
      </form>
    </div>
  );
}
```

Authentication Context and Routing:
```jsx
// src/context/AuthContext.jsx
import { createContext, useState, useEffect, useContext } from 'react';
import { authService } from '../services/authService';

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem('token');
    const saved = localStorage.getItem('user');
    if (token && saved) setUser(JSON.parse(saved));
    setLoading(false);
  }, []);

  async function login(email, password) {
    const data = await authService.login(email, password);
    setUser(data);
    localStorage.setItem('user', JSON.stringify(data));
  }

  function logout() {
    authService.logout();
    setUser(null);
    localStorage.removeItem('user');
  }

  return (
    <AuthContext.Provider value={{ user, loading, isAuthenticated: !!user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error('useAuth must be used within AuthProvider');
  return ctx;
}
```
