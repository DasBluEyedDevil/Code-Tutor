---
type: "THEORY"
title: "PKCE: Proof Key for Code Exchange"
---

**PKCE** (pronounced "pixie") protects the authorization code flow for public clients (mobile apps, SPAs).

**The Problem:**
Mobile apps cannot securely store client secrets. An attacker could intercept the authorization code and exchange it for tokens.

**PKCE Solution:**
```
1. Client generates random code_verifier (43-128 chars)
2. Client creates code_challenge = SHA256(code_verifier)
3. Client sends code_challenge with authorization request
4. Provider stores code_challenge with the code
5. Client sends code_verifier with token request
6. Provider verifies: SHA256(code_verifier) == stored challenge
```

**Why It Works:**
- Attacker can intercept the code
- But attacker doesn't have the code_verifier
- Only the original client can complete the exchange

**PKCE Methods:**
- `S256` (recommended): challenge = BASE64URL(SHA256(verifier))
- `plain` (not recommended): challenge = verifier