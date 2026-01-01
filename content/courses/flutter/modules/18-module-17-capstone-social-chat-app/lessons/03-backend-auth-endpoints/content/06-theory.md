---
type: "THEORY"
title: "Login and Session Management"
---


**Implementing Secure Login**

Login is more than just checking credentials. A complete login system handles:

**Login Methods**

| Method | Use Case | Security Level |
|--------|----------|---------------|
| Email + Password | Traditional login | Medium |
| Magic Link | Passwordless | High |
| OAuth (Google/Apple) | Social login | High |
| Biometric | Mobile only | Very High |
| 2FA | Additional security | Very High |

**Session Token Architecture**

Serverpod uses a dual-token system:

```
┌─────────────────────┐
│    Access Token     │  Short-lived (15-60 min)
│  Used for API calls │  Stored in memory
└─────────────────────┘
           │
           │ When expired
           ▼
┌─────────────────────┐
│   Refresh Token     │  Long-lived (7-30 days)
│ Used to get new AT  │  Stored securely
└─────────────────────┘
```

**Why Two Tokens?**

1. **Security**: If access token is stolen, limited damage (short lifetime)
2. **Performance**: Don't need to hit database on every request
3. **User Experience**: Users stay logged in across sessions
4. **Revocation**: Refresh tokens can be revoked server-side

**Token Storage (Client-Side)**

| Platform | Storage Method |
|----------|---------------|
| iOS | Keychain |
| Android | Encrypted SharedPreferences |
| Web | HttpOnly Cookies (preferred) |
| Desktop | Secure credential storage |

**Session Lifecycle**

```
1. Login successful
         ↓
2. Generate access token (15 min)
         ↓
3. Generate refresh token (7 days)
         ↓
4. Store refresh token in DB
         ↓
5. Return both tokens to client
         ↓
6. Client uses access token for API
         ↓
7. Access token expires
         ↓
8. Client sends refresh token
         ↓
9. Server validates & issues new tokens
         ↓
10. Repeat from step 6
```

