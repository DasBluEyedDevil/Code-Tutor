---
type: "EXAMPLE"
title: "Complete Login Component"
---

Full login form with error handling and redirect:

```jsx
import { useState } from 'react';
import { useNavigate, useLocation, Link } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

function LoginPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    
    const { login } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();
    
    // Where to redirect after login
    const from = location.state?.from?.pathname || '/dashboard';
    
    async function handleSubmit(e) {
        e.preventDefault();
        setError('');
        setLoading(true);
        
        try {
            await login(email, password);
            navigate(from, { replace: true });
        } catch (err) {
            setError(err.message || 'Invalid email or password');
        } finally {
            setLoading(false);
        }
    }
    
    return (
        <div className="login-page">
            <div className="login-card">
                <h1>Welcome Back</h1>
                <p className="subtitle">Sign in to your account</p>
                
                {error && (
                    <div className="error-banner" role="alert">
                        {error}
                    </div>
                )}
                
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input
                            id="email"
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                            disabled={loading}
                            autoComplete="email"
                        />
                    </div>
                    
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input
                            id="password"
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                            disabled={loading}
                            autoComplete="current-password"
                        />
                    </div>
                    
                    <button type="submit" disabled={loading} className="btn-primary">
                        {loading ? 'Signing in...' : 'Sign In'}
                    </button>
                </form>
                
                <p className="signup-link">
                    Don't have an account? <Link to="/register">Sign up</Link>
                </p>
            </div>
        </div>
    );
}

export default LoginPage;
```
