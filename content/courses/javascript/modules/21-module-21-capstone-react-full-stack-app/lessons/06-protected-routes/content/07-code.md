---
type: "EXAMPLE"
title: "Dashboard Page with Navigation"
---

Create a protected dashboard page with user info and logout:

```typescript
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import TaskList from '../components/TaskList';

const DashboardPage: React.FC = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();
  const [showLogoutConfirm, setShowLogoutConfirm] = useState<boolean>(false);

  const handleLogout = () => {
    logout();
    navigate('/login', { replace: true });
  };

  if (!user) {
    return <div>Loading...</div>;
  }

  return (
    <div className="dashboard-page">
      <header className="dashboard-header">
        <div className="dashboard-header-content">
          <h1>Dashboard</h1>
          <p className="greeting">Welcome back, {user.name}!</p>
        </div>
        <div className="dashboard-header-actions">
          <span className="user-email">{user.email}</span>
          <button
            onClick={() => setShowLogoutConfirm(true)}
            className="logout-button"
          >
            Logout
          </button>
        </div>
      </header>

      {showLogoutConfirm && (
        <div className="logout-confirm-modal">
          <div className="modal-overlay" onClick={() => setShowLogoutConfirm(false)} />
          <div className="modal-content">
            <h2>Confirm Logout</h2>
            <p>Are you sure you want to log out?</p>
            <div className="modal-actions">
              <button
                onClick={() => setShowLogoutConfirm(false)}
                className="cancel-button"
              >
                Cancel
              </button>
              <button
                onClick={handleLogout}
                className="confirm-button"
              >
                Logout
              </button>
            </div>
          </div>
        </div>
      )}

      <main className="dashboard-main">
        <section className="dashboard-section">
          <div className="section-header">
            <h2>Your Tasks</h2>
            <button className="create-task-button">+ Create New Task</button>
          </div>
          <TaskList userId={user.id} />
        </section>
      </main>
    </div>
  );
};

export default DashboardPage;
```
