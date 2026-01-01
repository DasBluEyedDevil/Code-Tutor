---
type: "THEORY"
title: "How JWT Authentication Works"
---

1. LOGIN REQUEST:
POST /api/auth/login
{"username": "john", "password": "secret"}

2. SERVER VALIDATES & CREATES TOKEN:
- Check credentials against database
- Create JWT with user info + expiration
- Sign with server's secret key
- Return token to client

{"token": "eyJhbGci...", "expiresIn": 3600}

3. CLIENT STORES TOKEN:
- localStorage (convenient but XSS vulnerable)
- httpOnly cookie (more secure)
- Memory (most secure, lost on refresh)

4. AUTHENTICATED REQUESTS:
GET /api/profile
Authorization: Bearer eyJhbGci...

5. SERVER VALIDATES TOKEN:
- Extract token from header
- Verify signature (proves it wasn't tampered)
- Check expiration
- Extract user info from payload
- Process request

NO DATABASE LOOKUP needed to validate!
Token contains everything needed to authenticate.