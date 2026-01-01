---
type: "THEORY"
title: "Authentication vs Authorization"
---

AUTHENTICATION: "Who are you?"
- Verifying identity
- Username + password
- Login process
- Result: User is identified

Example:
POST /login
Body: { "username": "alice", "password": "secret123" }
→ Spring verifies credentials
→ If valid, user is "authenticated"

AUTHORIZATION: "What can you do?"
- Checking permissions
- Based on roles (USER, ADMIN, etc.)
- Access control
- Result: User can/cannot access resource

Example:
GET /admin/users (authenticated as USER role)
→ Spring checks: Does USER role have permission?
→ No! Return 403 Forbidden

GET /admin/users (authenticated as ADMIN role)
→ Spring checks: Does ADMIN role have permission?
→ Yes! Return user list

BOTH ARE NEEDED:
1. First: Authenticate (prove who you are)
2. Then: Authorize (check what you can do)