---
type: "KEY_POINT"
title: "Security Error Messages"
---

**Never reveal whether an email exists in your system:**

```python
# BAD - Information disclosure
if not user_exists(email):
    return "Email not found"  # Attacker now knows this email isn't registered
if not password_matches:
    return "Wrong password"   # Attacker confirms email IS registered

# GOOD - Consistent message
if not user_exists(email) or not password_matches:
    return "Invalid email or password"  # Attacker learns nothing
```

**Same principle applies to:**
- Password reset ("If this email exists, we'll send a reset link")
- Account lockout (Don't say "Account locked", just fail login)
- Rate limiting (Apply to all attempts, not just existing accounts)