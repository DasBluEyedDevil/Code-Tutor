---
type: "EXAMPLE"
title: "Protected Routes Pattern"
---

Implementing authentication-protected routes:

```jsx
import { Navigate, Outlet, useLocation } from 'react-router-dom';
import { useAuth } from './hooks/useAuth';  // Your auth hook

// Protected route wrapper
function ProtectedRoute() {
    const { user, loading } = useAuth();
    const location = useLocation();
    
    if (loading) {
        return <div>Loading...</div>;
    }
    
    if (!user) {
        // Redirect to login, save where they tried to go
        return <Navigate to="/login" state={{ from: location }} replace />;
    }
    
    return <Outlet />;  // Render child routes
}

// App with protected routes
function App() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Public routes */}
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                
                {/* Protected routes */}
                <Route element={<ProtectedRoute />}>
                    <Route path="/dashboard" element={<Dashboard />} />
                    <Route path="/profile" element={<Profile />} />
                    <Route path="/settings" element={<Settings />} />
                </Route>
                
                <Route path="*" element={<NotFound />} />
            </Routes>
        </BrowserRouter>
    );
}

// Login component that redirects back
function Login() {
    const navigate = useNavigate();
    const location = useLocation();
    const { login } = useAuth();
    
    async function handleSubmit(credentials) {
        await login(credentials);
        // Redirect to where they tried to go, or dashboard
        const from = location.state?.from?.pathname || '/dashboard';
        navigate(from, { replace: true });
    }
    
    return <LoginForm onSubmit={handleSubmit} />;
}
```
