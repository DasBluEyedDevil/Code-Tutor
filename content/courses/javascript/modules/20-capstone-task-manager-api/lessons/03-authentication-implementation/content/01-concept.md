---
type: "THEORY"
title: "JWT Authentication Flow"
---

Our API uses JWT (JSON Web Tokens) for stateless authentication:

**Registration Flow:**
1. User sends email + password
2. Server validates input (Zod)
3. Server checks email isn't taken
4. Server hashes password (Bun.password)
5. Server creates user in database
6. Server returns JWT token

**Login Flow:**
1. User sends email + password
2. Server finds user by email
3. Server verifies password against hash
4. If valid, server returns JWT token
5. If invalid, server returns 401

**Protected Route Flow:**
1. Client sends request with `Authorization: Bearer <token>`
2. Middleware extracts and verifies token
3. If valid, request continues with user context
4. If invalid, middleware returns 401

**Why JWT?**
- Stateless: No server-side session storage needed
- Scalable: Works across multiple servers
- Self-contained: Token carries user info