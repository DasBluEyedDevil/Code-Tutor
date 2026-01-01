---
type: "THEORY"
title: "React Router v6 Setup with Routes"
---

React Router provides client-side routing for single-page applications. Version 6 introduces simplified syntax and improved features.

```jsx
// src/App.jsx
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/common/ProtectedRoute';
import Layout from './components/layout/Layout';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import DashboardPage from './pages/DashboardPage';
import TasksPage from './pages/TasksPage';
import TaskForm from './components/tasks/TaskForm';

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Routes>
          {/* Public routes */}
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />

          {/* Protected routes with layout */}
          <Route element={<ProtectedRoute><Layout /></ProtectedRoute>}>
            <Route path="/" element={<Navigate to="/dashboard" replace />} />
            <Route path="/dashboard" element={<DashboardPage />} />
            <Route path="/tasks" element={<TasksPage />} />
            <Route path="/tasks/new" element={<TaskForm />} />
            <Route path="/tasks/:id/edit" element={<TaskForm />} />
          </Route>

          {/* 404 fallback */}
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  );
}

// src/components/common/ProtectedRoute.jsx
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../../hooks/useAuth';

export default function ProtectedRoute({ children }) {
  const { isAuthenticated, loading } = useAuth();
  const location = useLocation();

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!isAuthenticated) {
    // Redirect to login, saving the attempted URL
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return children;
}

// src/components/layout/Layout.jsx
import { Outlet } from 'react-router-dom';
import Header from './Header';
import Sidebar from './Sidebar';

export default function Layout() {
  return (
    <div className="min-h-screen bg-gray-100">
      <Header />
      <div className="flex">
        <Sidebar />
        <main className="flex-1 p-6">
          <Outlet /> {/* Child routes render here */}
        </main>
      </div>
    </div>
  );
}

// Navigation with useNavigate hook
import { useNavigate, Link } from 'react-router-dom';

function TaskCard({ task }) {
  const navigate = useNavigate();

  return (
    <div>
      <h3>{task.title}</h3>
      {/* Declarative navigation */}
      <Link to={`/tasks/${task.id}/edit`}>Edit</Link>
      
      {/* Programmatic navigation */}
      <button onClick={() => navigate(`/tasks/${task.id}/edit`)}>
        Edit
      </button>
      
      {/* Navigate back */}
      <button onClick={() => navigate(-1)}>Go Back</button>
    </div>
  );
}
```

Router Features:
- Nested routes with Layout pattern
- Protected routes with redirect
- Dynamic route params (:id)
- Programmatic navigation with useNavigate
- URL state preservation with useLocation