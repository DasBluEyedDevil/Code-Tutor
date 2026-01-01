---
type: "THEORY"
title: "Auth Flow Overview"
---


**Understanding Authentication in Serverpod**

Authentication is the foundation of any user-facing application. In this lesson, we'll implement secure authentication endpoints using Serverpod's built-in auth module.

**Authentication vs Authorization**

| Concept | Question | Example |
|---------|----------|--------|
| **Authentication** | Who are you? | Login with email/password |
| **Authorization** | What can you do? | Can you edit this post? |

This lesson focuses on authentication. We'll cover authorization in the next lesson.

**Serverpod Auth Module**

Serverpod provides `serverpod_auth` - a complete authentication solution:

```
serverpod_auth_server/     # Server-side package
├── Email authentication    # Register, login, verify
├── Social providers        # Google, Apple, Firebase
├── Session management      # Tokens, refresh, logout
└── User info storage       # Built-in UserInfo table

serverpod_auth_client/     # Client-side package
├── Session manager         # Token storage
├── Auth controllers        # Login flows
└── Provider widgets        # Sign-in buttons
```

**Authentication Flow**

```
┌─────────────┐     ┌─────────────┐     ┌─────────────┐
│   Client    │     │   Server    │     │  Database   │
└──────┬──────┘     └──────┬──────┘     └──────┬──────┘
       │                   │                   │
       │  1. Register      │                   │
       │──────────────────>│                   │
       │                   │  2. Hash password │
       │                   │  3. Create user   │
       │                   │──────────────────>│
       │                   │<──────────────────│
       │  4. Return token  │                   │
       │<──────────────────│                   │
       │                   │                   │
       │  5. Login         │                   │
       │──────────────────>│                   │
       │                   │  6. Verify creds  │
       │                   │──────────────────>│
       │                   │<──────────────────│
       │  7. Session token │                   │
       │<──────────────────│                   │
       │                   │                   │
       │  8. API request   │                   │
       │  + Bearer token   │                   │
       │──────────────────>│                   │
       │                   │  9. Validate      │
       │  10. Response     │                   │
       │<──────────────────│                   │
```

**Security Principles**

1. **Never store plain passwords** - Always hash with bcrypt or Argon2
2. **Use HTTPS everywhere** - Encrypt all network traffic
3. **Short-lived tokens** - Access tokens expire quickly
4. **Refresh tokens** - Securely stored, used to get new access tokens
5. **Rate limiting** - Prevent brute force attacks

