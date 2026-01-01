---
type: "THEORY"
title: "Handling API Errors in the Frontend"
---

Robust error handling separates professional applications from amateur ones. Users should always understand what went wrong and what they can do about it.

Error Response Structure:
Our Spring Boot backend returns consistent error responses:

```json
{
  "timestamp": "2024-01-15T10:30:00Z",
  "status": 400,
  "error": "Bad Request",
  "detail": "Title is required",
  "path": "/api/tasks",
  "errors": {
    "title": "Title is required",
    "dueDate": "Due date must be in the future"
  }
}
```

Centralized Error Handling:

```javascript
// src/utils/errorHandler.js
export function getErrorMessage(error) {
  // Network error (no response from server)
  if (!error.response) {
    return 'Unable to connect to server. Please check your internet connection.';
  }

  const { status, data } = error.response;

  // Handle specific status codes
  switch (status) {
    case 400:
      return data.detail || 'Invalid request. Please check your input.';
    case 401:
      return 'Session expired. Please log in again.';
    case 403:
      return 'You do not have permission to perform this action.';
    case 404:
      return 'The requested resource was not found.';
    case 409:
      return data.detail || 'This operation conflicts with existing data.';
    case 422:
      return data.detail || 'Validation failed. Please check your input.';
    case 500:
      return 'Server error. Please try again later.';
    default:
      return data.detail || 'An unexpected error occurred.';
  }
}

export function getFieldErrors(error) {
  return error.response?.data?.errors || {};
}
```

Using Error Handling in Forms:

```jsx
// In TaskForm.jsx
import { getErrorMessage, getFieldErrors } from '../utils/errorHandler';

async function handleSubmit(e) {
  e.preventDefault();
  setLoading(true);
  setError(null);
  setFieldErrors({});

  try {
    if (isEditMode) {
      await taskService.updateTask(id, formData);
    } else {
      await taskService.createTask(formData);
    }
    navigate('/tasks');
  } catch (err) {
    // Set general error message
    setError(getErrorMessage(err));
    
    // Set field-specific errors for inline display
    setFieldErrors(getFieldErrors(err));
  } finally {
    setLoading(false);
  }
}

// Render field errors inline
<input
  name="title"
  className={fieldErrors.title ? 'border-red-500' : 'border-gray-300'}
  // ...
/>
{fieldErrors.title && (
  <p className="text-red-500 text-sm mt-1">{fieldErrors.title}</p>
)}
```

Global Error Boundary:

```jsx
// src/components/common/ErrorBoundary.jsx
import { Component } from 'react';

export default class ErrorBoundary extends Component {
  state = { hasError: false, error: null };

  static getDerivedStateFromError(error) {
    return { hasError: true, error };
  }

  componentDidCatch(error, errorInfo) {
    console.error('Error caught by boundary:', error, errorInfo);
    // Send to error tracking service (Sentry, etc.)
  }

  render() {
    if (this.state.hasError) {
      return (
        <div className="min-h-screen flex items-center justify-center">
          <div className="text-center">
            <h1 className="text-2xl font-bold text-red-600 mb-4">
              Something went wrong
            </h1>
            <p className="text-gray-600 mb-4">
              We are sorry, but something unexpected happened.
            </p>
            <button
              onClick={() => window.location.reload()}
              className="bg-blue-600 text-white px-4 py-2 rounded"
            >
              Reload Page
            </button>
          </div>
        </div>
      );
    }
    return this.props.children;
  }
}
```