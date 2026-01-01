---
type: "CODE"
title: "ProtectedRoute Component"
---

Create a reusable ProtectedRoute component that wraps protected pages:

```typescript
import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

interface ProtectedRouteProps {
  children: React.ReactNode;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
  const { user, isLoading } = useAuth();
  const location = useLocation();

  // Show loading state while checking authentication
  if (isLoading) {
    return (
      <div className="protected-route-loading">
        <div className="loading-spinner"></div>
        <p>Loading...</p>
      </div>
    );
  }

  // User is authenticated, render the protected component
  if (user) {
    return <>{children}</>;
  }

  // User is not authenticated, redirect to login
  // Save the location so we can redirect back after login
  return (
    <Navigate
      to="/login"
      state={{ from: location }}
      replace
    />
  );
};

export default ProtectedRoute;
```
