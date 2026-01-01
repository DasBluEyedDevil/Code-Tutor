---
type: "CONCEPT"
title: "Token Storage & Security"
---

Tokens must be stored securely and sent with API requests.

**localStorage vs sessionStorage**
- `localStorage`: Persists until explicitly cleared (survives page refresh)
- `sessionStorage`: Cleared when tab closes
- `Memory`: Lost on refresh (not persistent)

For authentication, use `localStorage` so users stay logged in, but understand the tradeoff:

**Security Considerations**
```typescript
// Store token after login
const storeToken = (token: string) => {
  localStorage.setItem('token', token);
};

// Retrieve token for API requests
const getToken = (): string | null => {
  return localStorage.getItem('token');
};

// Clear token on logout
const clearToken = () => {
  localStorage.removeItem('token');
};

// Add token to every API request
const fetchWithAuth = async (url: string, options: RequestInit = {}) => {
  const token = getToken();
  return fetch(url, {
    ...options,
    headers: {
      ...options.headers,
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  });
};
```

**Important**: Never store passwords. Only store JWT tokens. The token expires (typically in hours) so periodic re-authentication is required.