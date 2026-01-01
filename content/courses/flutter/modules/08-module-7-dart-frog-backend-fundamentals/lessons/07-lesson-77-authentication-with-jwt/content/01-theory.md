---
type: "THEORY"
title: "What is JWT?"
---


**JSON Web Token (JWT)** is a secure way to pass user information between your Flutter app and Dart Frog backend.

### JWT Structure: Three Parts

A JWT looks like this:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIxMjMiLCJlbWFpbCI6InVzZXJAZXhhbXBsZS5jb20ifQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

Those three parts separated by dots are:

**1. Header** - Token metadata
```json
{"alg": "HS256", "typ": "JWT"}
```
Tells us the encryption algorithm (HS256) and token type (JWT).

**2. Payload** - User data
```json
{"userId": "123", "email": "user@example.com", "exp": 1735689600}
```
Contains user info and expiration time. This is what your backend reads to know WHO is making the request.

**3. Signature** - Verification
A cryptographic signature that proves the token hasn't been tampered with. Only the server with the secret key can create valid signatures.

### Why JWT?

**Stateless Authentication**: The server doesn't need to store session data. Everything needed to verify the user is in the token itself.

**Use Case Flow**:
1. User logs in with email/password
2. Server verifies credentials
3. Server creates JWT with user info
4. Client stores token (in secure storage)
5. Client sends token with each request
6. Server validates token on protected routes

**Benefits**:
- Scales easily (no session storage needed)
- Works across multiple servers
- Mobile-friendly
- Industry standard

