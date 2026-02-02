---
type: "EXAMPLE"
title: "Login Page with Redirect"
---

Create a login page that redirects to the original location after success:

```typescript
import React from 'react';
import { useNavigate, useLocation, Navigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import LoginForm from '../components/LoginForm';

const LoginPage: React.FC = () => {
  const { user, isLoading } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  // If already logged in, redirect to dashboard
  if (!isLoading && user) {
    return <Navigate to="/dashboard" replace />;
  }

  const handleLoginSuccess = () => {
    // Get the location they were trying to access
    const from = location.state?.from?.pathname || '/dashboard';
    
    // Redirect to original location with replace to clear history
    navigate(from, { replace: true });
  };

  return (
    <div className="login-page">
      <div className="login-container">
        <h1>Welcome Back</h1>
        <p className="login-subtitle">Sign in to your Task Manager account</p>
        <LoginForm onSuccess={handleLoginSuccess} />
      </div>
    </div>
  );
};

export default LoginPage;
```
