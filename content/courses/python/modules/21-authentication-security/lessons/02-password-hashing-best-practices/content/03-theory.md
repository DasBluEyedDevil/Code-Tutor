---
type: "THEORY"
title: "Not All Hashes Are Equal"
---

**NEVER use these for passwords:**
- MD5 - Broken, fast, rainbow tables exist
- SHA-1 - Deprecated, too fast
- SHA-256 - Too fast for passwords!

Wait, SHA-256 is bad? Yes! Speed is the enemy for password hashing.

**Why Fast Hashes Are Dangerous:**
```
SHA-256 can compute ~10 billion hashes/second on GPU
8-character password = ~200 trillion combinations
Time to crack: ~5.5 hours
```

**Password-Specific Algorithms (SLOW by design):**

**bcrypt** (1999, still excellent)
- Configurable work factor (cost)
- Built-in salt
- Time-tested, widely supported
- ~4 hashes/second at cost=12

**Argon2** (2015, Password Hashing Competition winner)
- Memory-hard (resistant to GPU attacks)
- Three variants: Argon2d, Argon2i, Argon2id
- Argon2id recommended for passwords
- Modern choice for new projects

**scrypt** (2009)
- Memory-hard like Argon2
- Used by Litecoin, many crypto projects
- Good but Argon2 is generally preferred now