---
type: "WARNING"
title: "Password Hashing Timing Attacks"
---

**The Problem:**
String comparison stops at first difference:
```python
# BAD: Vulnerable to timing attack
if stored_hash == computed_hash:  # Returns early if first chars differ
    return True
```

**The Attack:**
Attacker measures response time to learn hash character by character.

**The Solution:**
```python
import secrets

# GOOD: Constant-time comparison
secrets.compare_digest(stored_hash, computed_hash)
```

**Good News:** passlib handles this automatically. But remember this when comparing any sensitive values yourself.