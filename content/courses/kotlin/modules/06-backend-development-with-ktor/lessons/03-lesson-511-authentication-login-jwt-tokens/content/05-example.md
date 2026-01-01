---
type: "EXAMPLE"
title: "Code Breakdown"
---


### The Login Flow


### Security Highlights

**1. Generic Error Messages**:

This prevents attackers from enumerating valid email addresses.

**2. Password Verification Timing**:
Even if email doesn't exist, we should still verify the password (against a dummy hash) to prevent timing attacks:


This ensures the function always takes the same time, whether email exists or not.

**3. Token Claims**:

These claims are used to validate the token and identify the user.

---



```kotlin
.withSubject(userId.toString())     // Standard claim: user identifier
.withClaim("email", email)          // Custom claim: user email
.withIssuedAt(Date())               // When token was created
.withExpiresAt(Date(...))           // When token expires
.withIssuer(ISSUER)                 // Who issued the token
.withAudience(AUDIENCE)             // Who token is intended for
```
