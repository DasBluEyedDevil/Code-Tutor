---
type: "THEORY"
title: "Why Hash, Not Encrypt?"
---

This is one of the most important distinctions in security:

**Encryption is Reversible:**
```
Plaintext --[key]--> Ciphertext --[key]--> Plaintext
```
- Same key encrypts and decrypts
- If an attacker gets the key, ALL passwords are exposed
- You must store the key somewhere (risk!)

**Hashing is One-Way:**
```
Plaintext --> Hash --> (No way back!)
```
- Cannot reverse a hash to get the original password
- To verify: hash the input and compare hashes
- No key to steal

**The Critical Insight:**
You don't need to know the user's password. You only need to verify they know it. Hashing allows verification without storage.

**What If Your Database Is Stolen?**
- Encrypted passwords: Attacker finds key, decrypts all passwords instantly
- Hashed passwords: Attacker must crack each hash individually (very slow with modern algorithms)