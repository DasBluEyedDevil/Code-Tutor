---
type: "ANALOGY"
title: "The Concept"
---


### The Concert Ticket Analogy

Think of JWT authentication like getting into a concert:

**Old Way (Sessions)**:
- You show your ID at the door
- Bouncer writes your name on a clipboard (server memory)
- Every time you leave and return, bouncer checks the clipboard
- Problem: Bouncer must remember thousands of people
- If bouncer forgets (server restarts), you're locked out

**New Way (JWT)**:
- You show your ID at the door once
- Bouncer gives you a wristband with your info and a tamper-proof seal
- Every time you return, you just show the wristband
- Anyone can verify the wristband is authentic (check the seal)
- No need to remember who you are—the wristband proves everything
- ✅ Scalable!

JWTs are like tamper-proof wristbands for your API.

### What is a JWT?

A JWT (JSON Web Token) is a compact, self-contained token that securely transmits information between parties.

**Structure**: Three parts separated by dots (`.`)


#### Part 1: Header
Base64URL encoded → `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9`

#### Part 2: Payload (Claims)
Base64URL encoded → `eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4g...`

#### Part 3: Signature

The signature ensures:
- Token hasn't been tampered with
- Token was issued by your server (only you know the secret)

### JWT vs Sessions

| Aspect | JWT (Stateless) | Sessions (Stateful) |
|--------|-----------------|---------------------|
| **Storage** | Client-side (sent with each request) | Server-side memory/database |
| **Scalability** | ✅ Excellent (no server state) | ❌ Requires shared session store |
| **Performance** | ✅ Fast (no DB lookup) | ❌ DB/cache lookup each request |
| **Revocation** | ❌ Hard (token valid until expiration) | ✅ Easy (delete session) |
| **Size** | ❌ Larger (entire token sent) | ✅ Small (just session ID) |
| **Best For** | Distributed systems, microservices | Traditional monolithic apps |

**When to use JWT**:
- RESTful APIs
- Mobile apps
- Microservices architecture
- Cross-domain authentication

---



```kotlin
HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret
)
```
