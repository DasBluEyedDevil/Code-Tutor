---
type: "KEY_POINT"
title: "Authentication Context and Protected Routes"
---

React Context provides global state without prop drilling. We use it to manage authentication state across the entire application.

```jsx
// src/context/AuthContext.jsx
import { createContext, useState, useEffect, useContext } from 'react';
import { authService } from '../services/authService';

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Check for existing session on mount
    const token = localStorage.getItem('token');
    const savedUser = localStorage.getItem('user');
    
    if (token && savedUser) {
      setUser(JSON.parse(savedUser));
    }
    setLoading(false);
  }, []);

  async function login(email, password) {
    const userData = await authService.login(email, password);
    setUser(userData);
    localStorage.setItem('user', JSON.stringify(userData));
    return userData;
  }

  async function register(email, password, name) {
    const userData = await authService.register(email, password, name);
    setUser(userData);
    localStorage.setItem('user', JSON.stringify(userData));
    return userData;
  }

  function logout() {
    authService.logout();
    setUser(null);
    localStorage.removeItem('user');
  }

  const value = {
    user,
    loading,
    isAuthenticated: !!user,
    login,
    register,
    logout,
  };

  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>
  );
}

// src/hooks/useAuth.js
import { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within AuthProvider');
  }
  return context;
}

// Usage in components:
import { useAuth } from '../hooks/useAuth';

function Header() {
  const { user, logout, isAuthenticated } = useAuth();

  return (
    <header>
      {isAuthenticated ? (
        <>
          <span>Welcome, {user.name}</span>
          <button onClick={logout}>Logout</button>
        </>
      ) : (
        <Link to="/login">Login</Link>
      )}
    </header>
  );
}
```

Context Benefits:
- Single source of truth for auth state
- Any component can access auth without prop drilling
- Centralized login/logout logic
- Automatic re-render on auth state change