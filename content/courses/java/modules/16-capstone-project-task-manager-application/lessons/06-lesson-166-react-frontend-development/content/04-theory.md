---
type: "THEORY"
title: "TaskList Component with Filtering UI"
---

The TaskList component displays tasks and provides filtering options. It demonstrates state management, conditional rendering, and component composition.

```jsx
// src/components/tasks/TaskList.jsx
import { useState, useEffect } from 'react';
import { taskService } from '../../services/taskService';
import TaskCard from './TaskCard';
import TaskFilters from './TaskFilters';
import LoadingSpinner from '../common/LoadingSpinner';

export default function TaskList() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [filters, setFilters] = useState({
    status: 'ALL',
    priority: 'ALL',
    search: '',
  });

  useEffect(() => {
    loadTasks();
  }, []);

  async function loadTasks() {
    try {
      setLoading(true);
      const response = await taskService.getTasks();
      setTasks(response.content);
      setError(null);
    } catch (err) {
      setError('Failed to load tasks. Please try again.');
      console.error('Error loading tasks:', err);
    } finally {
      setLoading(false);
    }
  }

  async function handleDelete(taskId) {
    if (!window.confirm('Are you sure you want to delete this task?')) {
      return;
    }
    try {
      await taskService.deleteTask(taskId);
      setTasks(tasks.filter(t => t.id !== taskId));
    } catch (err) {
      setError('Failed to delete task.');
    }
  }

  // Apply filters client-side
  const filteredTasks = tasks.filter(task => {
    if (filters.status !== 'ALL' && task.status !== filters.status) {
      return false;
    }
    if (filters.priority !== 'ALL' && task.priority !== filters.priority) {
      return false;
    }
    if (filters.search && !task.title.toLowerCase().includes(filters.search.toLowerCase())) {
      return false;
    }
    return true;
  });

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <div className="space-y-6">
      <TaskFilters filters={filters} onFilterChange={setFilters} />
      
      {error && (
        <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
          {error}
        </div>
      )}

      {filteredTasks.length === 0 ? (
        <div className="text-center py-12 text-gray-500">
          <p>No tasks found.</p>
          <p className="text-sm">Create your first task to get started!</p>
        </div>
      ) : (
        <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
          {filteredTasks.map(task => (
            <TaskCard 
              key={task.id} 
              task={task} 
              onDelete={() => handleDelete(task.id)}
              onUpdate={loadTasks}
            />
          ))}
        </div>
      )}
    </div>
  );
}

// src/components/tasks/TaskFilters.jsx
export default function TaskFilters({ filters, onFilterChange }) {
  return (
    <div className="flex flex-wrap gap-4 p-4 bg-gray-50 rounded-lg">
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Status
        </label>
        <select
          value={filters.status}
          onChange={(e) => onFilterChange({ ...filters, status: e.target.value })}
          className="border rounded-md px-3 py-2"
        >
          <option value="ALL">All Statuses</option>
          <option value="PENDING">Pending</option>
          <option value="IN_PROGRESS">In Progress</option>
          <option value="COMPLETED">Completed</option>
        </select>
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Priority
        </label>
        <select
          value={filters.priority}
          onChange={(e) => onFilterChange({ ...filters, priority: e.target.value })}
          className="border rounded-md px-3 py-2"
        >
          <option value="ALL">All Priorities</option>
          <option value="LOW">Low</option>
          <option value="MEDIUM">Medium</option>
          <option value="HIGH">High</option>
          <option value="URGENT">Urgent</option>
        </select>
      </div>

      <div className="flex-1 min-w-[200px]">
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Search
        </label>
        <input
          type="text"
          value={filters.search}
          onChange={(e) => onFilterChange({ ...filters, search: e.target.value })}
          placeholder="Search tasks..."
          className="w-full border rounded-md px-3 py-2"
        />
      </div>
    </div>
  );
}
```