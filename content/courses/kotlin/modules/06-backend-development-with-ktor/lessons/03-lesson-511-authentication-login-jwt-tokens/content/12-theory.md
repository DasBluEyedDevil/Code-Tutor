---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: C) Header, Payload, Signature**

JWT structure:

Each part is Base64URL encoded (except signature which is encrypted).

---

**Question 2: B) Short access tokens limit exposure if stolen; refresh tokens enable revocation**

The two-token system provides:
- **Security**: Access tokens expire quickly (15 min) limiting damage if stolen
- **UX**: Users don't have to login every 15 minutes (refresh tokens last 7 days)
- **Control**: You can revoke refresh tokens but can't revoke JWTs (they're stateless)

---

**Question 3: B) It prevents attackers from enumerating valid email addresses**

Different messages leak information:


---

**Question 4: C) `sub` (subject)**

Standard JWT claims:
- `sub`: Subject (user identifier)
- `iss`: Issuer (who created token)
- `aud`: Audience (who token is for)
- `exp`: Expiration timestamp
- `iat`: Issued at timestamp

---

**Question 5: C) To protect users if database is breached (like password hashing)**

If refresh tokens are stored in plaintext:

If refresh tokens are hashed:

---



```kotlin
Database breached → Attacker gets hashes →
Can't use them (one-way hashing) → Users are safe!
```
