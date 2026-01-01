---
type: "KEY_POINT"
title: "The Role of Salt"
---

**Salt** is random data added to each password before hashing:

```
Without salt:
Hash('password123') = abc123...  # Same for ALL users with this password

With salt:
Hash('password123' + 'randomsalt1') = xyz789...
Hash('password123' + 'randomsalt2') = def456...  # Different!
```

**Why Salt Matters:**
1. **Prevents rainbow table attacks** - Pre-computed hash tables become useless
2. **Identical passwords have different hashes** - Can't tell if two users have the same password
3. **Must crack each password individually** - No bulk attacks

**Salt Best Practices:**
- Generate cryptographically random salt (16+ bytes)
- Unique salt for every password
- Store salt with the hash (bcrypt/Argon2 do this automatically)
- Salt is NOT secret - it's stored in plain text