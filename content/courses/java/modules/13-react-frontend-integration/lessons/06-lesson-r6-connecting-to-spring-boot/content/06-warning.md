---
type: "WARNING"
title: "Security Best Practices"
---

TOKEN STORAGE:

localStorage:
+ Persists across sessions
- Vulnerable to XSS attacks

sessionStorage:
+ Clears when tab closes
- Lost on refresh (bad UX)

HttpOnly Cookie (most secure):
+ Not accessible via JavaScript
+ Automatic on every request
- Requires backend to set cookie
- CSRF protection needed

FOR LEARNING: localStorage is fine
FOR PRODUCTION: Use HttpOnly cookies with CSRF protection

NEVER DO:

// Never log tokens
console.log('Token:', token);

// Never put tokens in URLs
fetch(`/api/users?token=${token}`);

// Never store in global variables
window.authToken = token;

ALWAYS DO:

// Check token expiration
function isTokenExpired(token) {
    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload.exp * 1000 < Date.now();
}

// Clear token on logout
function logout() {
    localStorage.removeItem('token');
    // Navigate to login
}

// Handle 401 responses globally
if (response.status === 401) {
    logout();
}