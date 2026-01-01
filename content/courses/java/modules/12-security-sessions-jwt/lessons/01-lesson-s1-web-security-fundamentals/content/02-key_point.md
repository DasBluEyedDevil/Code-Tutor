---
type: "KEY_POINT"
title: "Authentication vs Authorization"
---

These terms are often confused but are fundamentally different:

AUTHENTICATION (AuthN):
- WHO are you?
- Proving identity
- Login process
- Examples: username/password, fingerprint, OAuth

AUTHORIZATION (AuthZ):
- WHAT can you do?
- Checking permissions
- After authentication
- Examples: admin vs user roles, read vs write access

Real-world analogy:

AUTHENTICATION: Showing your ID at the airport.
'Yes, you are John Smith.'

AUTHORIZATION: Checking your boarding pass.
'John Smith can board Flight 123, seat 14A.'

Both are required for secure applications:
1. First, verify WHO the user is (authentication)
2. Then, determine WHAT they can access (authorization)