---
type: "CONCEPT"
title: "Redirect & Auth State Persistence"
---

When routing with authentication, you need to:
1. **Redirect unauthenticated users** to login
2. **Redirect authenticated users away from login** (e.g., if they visit /login while logged in)
3. **Persist auth state** across page refreshes
4. **Save the original URL** to redirect after login ("return to where you were")

**Redirect After Login Pattern**
Save the user's original location before redirecting to login:
```typescript
import { useNavigate, useLocation } from 'react-router-dom';

function LoginPage() {
  const navigate = useNavigate();
  const location = useLocation();

  const handleLoginSuccess = () => {
    // Redirect to where they came from, or dashboard
    const from = location.state?.from?.pathname || '/dashboard';
    navigate(from, { replace: true });
  };

  return <LoginForm onSuccess={handleLoginSuccess} />;
}

// In ProtectedRoute, save the location when redirecting
if (!user) {
  return <Navigate to="/login" state={{ from: location }} replace />;
}
```

**Auth Persistence Strategy**
1. On app startup, check localStorage for token
2. If token exists, verify it's still valid (call /api/auth/me)
3. If valid, restore user state from server response
4. Show loading page during this check (prevent flashing login)
5. Then render the app with correct initial state