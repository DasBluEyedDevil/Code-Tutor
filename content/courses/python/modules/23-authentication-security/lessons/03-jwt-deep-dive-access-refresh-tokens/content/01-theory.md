---
type: "THEORY"
title: "Understanding JWT Structure"
---

**JSON Web Tokens (JWT)** are the industry standard for stateless authentication. Every JWT consists of three parts separated by dots:

```
header.payload.signature
```

**1. Header** - Metadata about the token:
```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

**2. Payload** - Claims (data) about the user:
```json
{
  "sub": "1234567890",
  "name": "Alice",
  "iat": 1704067200,
  "exp": 1704070800
}
```

**3. Signature** - Ensures integrity:
```
HMAC-SHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret_key
)
```

**Standard Claims (Registered):**
- `iss` (issuer) - Who created the token
- `sub` (subject) - Who the token is about (user ID)
- `aud` (audience) - Who should accept the token
- `exp` (expiration) - When the token expires (Unix timestamp)
- `iat` (issued at) - When the token was created
- `nbf` (not before) - Token not valid before this time
- `jti` (JWT ID) - Unique token identifier