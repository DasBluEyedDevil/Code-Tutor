---
type: "KEY_POINT"
title: "OAuth2 vs OpenID Connect"
---

**OAuth2:** Authorization framework
- "Can this app access my photos?"
- Returns an **access token**
- Token grants API access
- Does NOT prove user identity

**OpenID Connect (OIDC):** Authentication layer on OAuth2
- "Who is this user?"
- Returns an **ID token** (JWT with user info)
- Proves user identity
- Used for "Login with Google/GitHub"

**For Social Login in Finance Tracker:**
Use OpenID Connect (Google Sign-In, GitHub Login) which gives you:
1. **ID Token** - Contains user info (email, name)
2. **Access Token** - For calling provider APIs if needed

**Common OIDC Claims in ID Token:**
```json
{
  "sub": "1234567890",
  "email": "alice@gmail.com",
  "email_verified": true,
  "name": "Alice Smith",
  "picture": "https://..."
}
```