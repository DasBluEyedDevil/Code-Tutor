---
type: "THEORY"
title: "What is JWT?"
---

**JSON Web Token (JWT) - Stateless Authentication**

JWT is a compact, URL-safe token format for securely transmitting information between parties. It's the industry standard for API authentication.

**JWT Structure: header.payload.signature**

A JWT consists of three parts separated by dots:

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

1. **Header** (red): Algorithm and token type
   ```json
   { "alg": "HS256", "typ": "JWT" }
   ```

2. **Payload** (purple): Claims (user data)
   ```json
   { "sub": "user@example.com", "exp": 1234567890 }
   ```

3. **Signature** (blue): Verification hash
   ```
   HMACSHA256(base64(header) + "." + base64(payload), secret)
   ```

**Stateless Authentication**

Unlike sessions (stored on server), JWTs are **stateless**:
- Server doesn't store session data
- Token contains all needed info
- Easily scales horizontally
- No database lookup for each request

**Access Tokens vs Refresh Tokens**

| Token Type | Lifetime | Purpose |
|------------|----------|----------|
| Access Token | Short (15-60 min) | API authorization |
| Refresh Token | Long (days/weeks) | Get new access tokens |

**Why short-lived access tokens?**
- If stolen, limited damage window
- Refresh tokens stored securely (httpOnly cookies)
- Revocation possible via refresh token invalidation

**Library Choice: PyJWT**

We use `pyjwt` (NOT `python-jose` which is deprecated):
```bash
uv add pyjwt
```

**Security Considerations:**
- Never store secrets in JWT payload (it's only base64 encoded, not encrypted)
- Always use HTTPS
- Set appropriate expiration times
- Use strong secret keys (256+ bits)