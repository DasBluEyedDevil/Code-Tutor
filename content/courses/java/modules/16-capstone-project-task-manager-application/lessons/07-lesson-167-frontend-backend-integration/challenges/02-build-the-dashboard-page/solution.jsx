// src/pages/DashboardPage.jsx
import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth';
import { taskService } from '../services/taskService';
import LoadingSpinner from '../components/common/LoadingSpinner';
import TaskCard from '../components/tasks/TaskCard';

export default function DashboardPage() {
  const { user } = useAuth();
  const [stats, setStats] = useState({ total: 0, pending: 0, completed: 0, overdue: 0 });
  const [recentTasks, setRecentTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function loadDashboard() {
      try {
        setLoading(true);
        const tasksResponse = await taskService.getTasks({ size: 100 });
        const tasks = tasksResponse.content;

        const now = new Date();
        const statistics = {
          total: tasks.length,
          pending: tasks.filter(t => t.status === 'PENDING').length,
          completed: tasks.filter(t => t.status === 'COMPLETED').length,
          overdue: tasks.filter(t => 
            t.dueDate && 
            new Date(t.dueDate) < now && 
            t.status !== 'COMPLETED'
          ).length,
        };
        setStats(statistics);

        const sorted = [...tasks].sort((a, b) => 
          new Date(b.createdAt) - new Date(a.createdAt)
        );
        setRecentTasks(sorted.slice(0, 5));
        setError(null);
      } catch (err) {
        setError('Failed to load dashboard');
        console.error(err);
      } finally {
        setLoading(false);
      }
    }
    loadDashboard();
  }, []);

  if (loading) return <LoadingSpinner size="large" className="py-12" />;

  return (
    <div className="space-y-8">
      <div>
        <h1 className="text-3xl font-bold">Welcome back, {user?.name || 'User'}!</h1>
        <p className="text-gray-600 mt-1">Here is an overview of your tasks</p>
      </div>

      {error && (
        <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
          {error}
        </div>
      )}

      <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
        <div className="bg-white p-6 rounded-lg shadow">
          <p className="text-gray-500 text-sm">Total Tasks</p>
          <p className="text-3xl font-bold">{stats.total}</p>
        </div>
        <div className="bg-white p-6 rounded-lg shadow">
          <p className="text-gray-500 text-sm">Pending</p>
          <p className="text-3xl font-bold text-yellow-600">{stats.pending}</p>
        </div>
        <div className="bg-white p-6 rounded-lg shadow">
          <p className="text-gray-500 text-sm">Completed</p>
          <p className="text-3xl font-bold text-green-600">{stats.completed}</p>
        </div>
        <div className="bg-white p-6 rounded-lg shadow">
          <p className="text-gray-500 text-sm">Overdue</p>
          <p className="text-3xl font-bold text-red-600">{stats.overdue}</p>
        </div>
      </div>

      <div className="flex gap-4">
        <Link
          to="/tasks/new"
          className="bg-blue-600 text-white px-6 py-3 rounded-md hover:bg-blue-700"
        >
          + Create New Task
        </Link>
        <Link
          to="/tasks"
          className="bg-gray-200 text-gray-700 px-6 py-3 rounded-md hover:bg-gray-300"
        >
          View All Tasks
        </Link>
      </div>

      <div>
        <h2 className="text-xl font-semibold mb-4">Recent Tasks</h2>
        {recentTasks.length === 0 ? (
          <p className="text-gray-500">No tasks yet. Create your first task!</p>
        ) : (
          <div className="grid gap-4">
            {recentTasks.map(task => (
              <TaskCard 
                key={task.id} 
                task={task} 
                onEdit={() => {}}
                onDelete={() => {}}
              />
            ))}
          </div>
        )}
      </div>
    </div>
  );
}