---
type: "THEORY"
title: "Shared: Dashboard Page"
---

BOTH PATHS

Regardless of your frontend choice, users need a dashboard that shows task statistics and recent activity.

Thymeleaf Dashboard (templates/dashboard.html):
```html
<main class="max-w-6xl mx-auto p-6">
    <h1 class="text-2xl font-bold mb-6">
        Welcome, <span th:text="${username}">User</span>!
    </h1>

    <div class="grid grid-cols-4 gap-4 mb-8">
        <div class="bg-white rounded-lg shadow p-4 text-center">
            <p class="text-3xl font-bold" th:text="${totalTasks}">0</p>
            <p class="text-gray-500">Total Tasks</p>
        </div>
        <div class="bg-white rounded-lg shadow p-4 text-center">
            <p class="text-3xl font-bold text-yellow-600" th:text="${pendingTasks}">0</p>
            <p class="text-gray-500">Pending</p>
        </div>
        <div class="bg-white rounded-lg shadow p-4 text-center">
            <p class="text-3xl font-bold text-green-600" th:text="${completedTasks}">0</p>
            <p class="text-gray-500">Completed</p>
        </div>
        <div class="bg-white rounded-lg shadow p-4 text-center">
            <p class="text-3xl font-bold text-red-600" th:text="${overdueTasks}">0</p>
            <p class="text-gray-500">Overdue</p>
        </div>
    </div>

    <h2 class="text-xl font-semibold mb-4">Recent Tasks</h2>
    <div th:each="task : ${recentTasks}" class="bg-white rounded shadow p-3 mb-2">
        <a th:href="@{/tasks/{id}/edit(id=${task.id})}"
           th:text="${task.title}" class="text-blue-600 hover:underline">Task</a>
        <span th:text="${task.status}" class="text-sm text-gray-500 ml-2">Status</span>
    </div>
</main>
```

React Dashboard:
```jsx
// src/pages/DashboardPage.jsx
import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { taskService } from '../services/taskService';
import { useAuth } from '../context/AuthContext';

export default function DashboardPage() {
  const { user } = useAuth();
  const [stats, setStats] = useState({ total: 0, pending: 0, completed: 0, overdue: 0 });
  const [recentTasks, setRecentTasks] = useState([]);

  useEffect(() => {
    taskService.getTasks(0, 100).then(data => {
      const tasks = data.content;
      const now = new Date();
      setStats({
        total: tasks.length,
        pending: tasks.filter(t => t.status === 'PENDING').length,
        completed: tasks.filter(t => t.status === 'COMPLETED').length,
        overdue: tasks.filter(t => t.dueDate && new Date(t.dueDate) < now && t.status !== 'COMPLETED').length,
      });
      setRecentTasks(tasks.slice(0, 5));
    });
  }, []);

  return (
    <div>
      <h1 className="text-2xl font-bold mb-6">Welcome, {user?.name}!</h1>
      <div className="grid grid-cols-4 gap-4 mb-8">
        <div className="bg-white rounded-lg shadow p-4 text-center">
          <p className="text-3xl font-bold">{stats.total}</p>
          <p className="text-gray-500">Total Tasks</p>
        </div>
        <div className="bg-white rounded-lg shadow p-4 text-center">
          <p className="text-3xl font-bold text-yellow-600">{stats.pending}</p>
          <p className="text-gray-500">Pending</p>
        </div>
        <div className="bg-white rounded-lg shadow p-4 text-center">
          <p className="text-3xl font-bold text-green-600">{stats.completed}</p>
          <p className="text-gray-500">Completed</p>
        </div>
        <div className="bg-white rounded-lg shadow p-4 text-center">
          <p className="text-3xl font-bold text-red-600">{stats.overdue}</p>
          <p className="text-gray-500">Overdue</p>
        </div>
      </div>
      <h2 className="text-xl font-semibold mb-4">Recent Tasks</h2>
      {recentTasks.map(task => (
        <div key={task.id} className="bg-white rounded shadow p-3 mb-2">
          <Link to={`/tasks/${task.id}/edit`} className="text-blue-600 hover:underline">{task.title}</Link>
          <span className="text-sm text-gray-500 ml-2">{task.status}</span>
        </div>
      ))}
    </div>
  );
}
```

Both versions show the same information -- total tasks, pending count, completed count, and overdue count -- just rendered differently. The Thymeleaf version gets data from the controller's Model, while the React version fetches from the API.
