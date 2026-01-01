---
type: "KEY_POINT"
title: "Authentication Best Practices Summary"
---

**Security Best Practices:**

1. Always use HTTPS in production
2. Never log passwords or session tokens
3. Implement rate limiting on auth endpoints
4. Use secure password requirements (min 8 chars, complexity)
5. Implement account lockout after failed attempts
6. Validate email addresses before accepting them

**User Experience Best Practices:**

1. Show clear error messages (but not too specific)
2. Implement 'Remember me' functionality
3. Provide password strength indicators
4. Support password managers (proper input types)
5. Offer multiple sign-in options (email, Google, Apple)
6. Make sign-out easily accessible

**Code Organization Best Practices:**

1. Create a single AuthService that wraps all auth functionality
2. Use a SessionManager listener for reactive UI updates
3. Separate authentication from authorization
4. Create middleware or helper functions for common auth checks
5. Test auth flows thoroughly, including edge cases

**What You Learned in This Lesson:**

- Setting up serverpod_auth module
- Implementing email/password authentication
- Integrating Google and Apple Sign-In
- Managing sessions with SessionManager
- Protecting endpoints with authentication checks
- Storing and updating user profiles
- Common pitfalls and how to avoid them

