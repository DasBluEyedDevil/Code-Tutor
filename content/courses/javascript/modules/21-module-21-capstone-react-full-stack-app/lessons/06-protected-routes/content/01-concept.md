---
type: "CONCEPT"
title: "React Router Setup"
---

React Router is the standard library for client-side routing in React. It lets you:
- Map URLs to components
- Navigate without page reloads
- Handle nested routes
- Manage history

**Core Concepts**
```typescript
// BrowserRouter: Wraps your app with routing context
// Routes: Contains all route definitions
// Route: Maps a path to a component
// Navigate: Programmatically navigate to a new URL
// useNavigate: Hook to get navigate function
// useParams: Hook to get URL parameters
// useLocation: Hook to get current location
```

**Basic Setup**
```typescript
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import DashboardPage from './pages/DashboardPage';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/dashboard" element={<DashboardPage />} />
        <Route path="/" element={<Navigate to="/dashboard" replace />} />
      </Routes>
    </BrowserRouter>
  );
}
```

**Dynamic Routes**
Use `:parameter` to create dynamic segments:
```typescript
// Route definition
<Route path="/tasks/:id" element={<TaskDetailPage />} />

// Component accessing the parameter
import { useParams } from 'react-router-dom';

function TaskDetailPage() {
  const { id } = useParams<{ id: string }>();
  return <div>Task {id}</div>;
}
```