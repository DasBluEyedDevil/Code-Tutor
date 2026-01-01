---
type: "THEORY"
title: "The Complete Auth Flow"
---

Connecting React to Spring Boot with JWT authentication:

1. USER ENTERS CREDENTIALS
   React form collects username/password

2. LOGIN REQUEST
   POST /api/auth/login
   Body: { username, password }

3. SPRING BOOT VALIDATES
   - Finds user in database
   - Verifies password with BCrypt
   - Generates JWT token

4. RESPONSE WITH TOKEN
   { token: "eyJhbG...", user: { id, name, roles } }

5. STORE TOKEN IN REACT
   - Save to localStorage (persists)
   - Or sessionStorage (clears on close)
   - Update auth context

6. SUBSEQUENT REQUESTS
   All API calls include header:
   Authorization: Bearer eyJhbG...

7. SPRING BOOT VALIDATES TOKEN
   - Extract token from header
   - Verify signature
   - Check expiration
   - Load user from token

8. LOGOUT
   - Clear stored token
   - Reset auth state
   - Redirect to login