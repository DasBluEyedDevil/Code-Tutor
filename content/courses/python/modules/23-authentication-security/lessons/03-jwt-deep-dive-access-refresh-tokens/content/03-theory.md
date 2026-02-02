---
type: "THEORY"
title: "Access Tokens vs Refresh Tokens"
---

Modern authentication uses a **two-token system** for security:

**Access Token:**
- Short-lived (5-15 minutes)
- Sent with every API request
- Contains user identity and permissions
- If stolen, limited damage window

**Refresh Token:**
- Long-lived (7-30 days)
- Used only to get new access tokens
- Stored securely (HttpOnly cookie)
- Can be revoked on the server

**Why Two Tokens?**

```
Without refresh tokens:
Access token valid for 7 days → If stolen, attacker has 7 days of access

With refresh tokens:
Access token valid for 15 min → If stolen, attacker has 15 min max
Refresh token in HttpOnly cookie → Much harder to steal via XSS
```

**The Flow:**
1. User logs in → Server returns access + refresh tokens
2. Client uses access token for API calls
3. Access token expires → Client uses refresh token to get new access token
4. Refresh token expires → User must log in again