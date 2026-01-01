---
type: "WARNING"
title: "JWT Security Pitfalls"
---

MISTAKE 1: Weak Secret Key
// BAD: Short, predictable secret
jwt.secret=secret123

// GOOD: Long, random, base64-encoded
jwt.secret=dGhpcyBpcyBhIHZlcnkgbG9uZyBhbmQgc2VjdXJlIHNlY3JldA==
// At least 256 bits for HS256, 384 for HS384, 512 for HS512

MISTAKE 2: Storing Sensitive Data in Payload
// BAD: Password, SSN, credit card in token
{"password": "secret", "ssn": "123-45-6789"}

// Payload is BASE64 ENCODED, not encrypted!
// Anyone with token can decode and read it

MISTAKE 3: No Expiration
// BAD: Token valid forever
.expiration(null)  // Never expires!

// GOOD: Short expiration
.expiration(new Date(now + 15 * 60 * 1000))  // 15 minutes

MISTAKE 4: Algorithm Confusion
// Attacker sends: {"alg": "none"}
// Some libraries accept unsigned tokens!
// ALWAYS verify algorithm matches expected

MISTAKE 5: localStorage XSS Vulnerability
// If attacker has XSS, they can steal token:
document.localStorage.getItem('token');

// Consider httpOnly cookies for token storage