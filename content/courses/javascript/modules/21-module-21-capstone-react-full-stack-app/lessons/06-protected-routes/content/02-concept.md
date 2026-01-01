---
type: "CONCEPT"
title: "ProtectedRoute Component Pattern"
---

A ProtectedRoute (also called PrivateRoute) is a wrapper component that:
1. Checks if user is authenticated
2. Allows access to the protected page if authenticated
3. Redirects to login if not authenticated
4. Optionally shows a loading state while checking auth

**Implementation Patterns**

**Pattern 1: Route-level Protection**
```typescript
<Route
  path="/dashboard"
  element={<ProtectedRoute><DashboardPage /></ProtectedRoute>}
/>
```

**Pattern 2: Conditional Element**
```typescript
<Route
  path="/dashboard"
  element={user ? <DashboardPage /> : <Navigate to="/login" />}
/>
```

**Pattern 3: Custom Component**
```typescript
<Route
  path="/dashboard"
  element={<ProtectedRoute component={DashboardPage} />}
/>
```

**State Checking**
Use the AuthContext to determine if user is authenticated:
```typescript
const { user, isLoading } = useAuth();

if (isLoading) return <LoadingPage />;
if (!user) return <Navigate to="/login" replace />;
return <YourPage />;
```