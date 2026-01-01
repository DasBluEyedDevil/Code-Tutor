---
type: "EXAMPLE"
title: "Auth Context and Hook"
---

Managing authentication state across the React app:

```jsx
// src/contexts/AuthContext.jsx
import { createContext, useContext, useState, useEffect } from 'react';

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(localStorage.getItem('token'));
    const [loading, setLoading] = useState(true);
    
    // Check for existing token on mount
    useEffect(() => {
        if (token) {
            validateToken();
        } else {
            setLoading(false);
        }
    }, []);
    
    async function validateToken() {
        try {
            const response = await fetch('/api/auth/me', {
                headers: { 'Authorization': `Bearer ${token}` }
            });
            if (response.ok) {
                const userData = await response.json();
                setUser(userData);
            } else {
                // Token invalid or expired
                logout();
            }
        } catch (error) {
            logout();
        } finally {
            setLoading(false);
        }
    }
    
    async function login(username, password) {
        const response = await fetch('/api/auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password })
        });
        
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || 'Login failed');
        }
        
        const data = await response.json();
        localStorage.setItem('token', data.token);
        setToken(data.token);
        setUser(data.user);
        return data;
    }
    
    function logout() {
        localStorage.removeItem('token');
        setToken(null);
        setUser(null);
    }
    
    const value = {
        user,
        token,
        loading,
        isAuthenticated: !!user,
        login,
        logout
    };
    
    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within AuthProvider');
    }
    return context;
}
```
