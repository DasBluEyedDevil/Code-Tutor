---
type: "ANALOGY"
title: "Password Hashing as One-Way Door"
---

**Understanding Hashing Through Physical Analogies**

**Encryption vs Hashing:**

**Encryption = Lockbox with Key**
You put a message in a lockbox, lock it with a key. Anyone with the key can open it and read the message. The process is reversible.

```
Message → [Lock with Key] → Encrypted → [Unlock with Key] → Original Message
```

**Hashing = Paper Shredder**
You feed a document into a shredder. The output is unique to that document, but you can NEVER reconstruct the original from the shreds.

```
Password → [Shredder/Hash] → Hash → IMPOSSIBLE → Original Password
```

**Why Hashing Works for Passwords:**

| Property | How It Helps |
|----------|--------------|
| One-way | Even if hackers get hashes, can't reverse them |
| Deterministic | Same password = same hash (for verification) |
| Unique | Different passwords = different hashes |
| Fixed size | Any length password = same length hash |

**The Verification Process:**

```
Registration:
"password123" → [bcrypt] → "$2b$12$xyz..."
                          ↓
                    Store in database

Login:
"password123" → [bcrypt with same salt] → "$2b$12$xyz..."
                                          ↓
                           Compare with stored hash
                                          ↓
                               Match? ✓ Login success
```

**Why Bcrypt is Special:**

Think of bcrypt as a **slow, secure shredder**:
- Regular shredder: 1 million documents per second (MD5 - easy to brute force)
- Bcrypt shredder: 10 documents per second (intentionally slow)

An attacker trying all possible passwords:
- With MD5: Days to crack
- With bcrypt: Centuries to crack

**The Key Insight:**
You don't need to reverse the hash. You just need to check if a given password produces the same hash. This lets you verify passwords without storing them.
