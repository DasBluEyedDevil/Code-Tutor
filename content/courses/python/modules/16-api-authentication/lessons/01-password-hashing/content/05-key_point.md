---
type: "KEY_POINT"
title: "Password Hashing Takeaways"
---

**Core Principles:**

1. **Use bcrypt via passlib** - Battle-tested, industry standard
   ```python
   from passlib.context import CryptContext
   pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")
   ```

2. **Never log or expose password hashes** - Even hashes are sensitive
   - Don't include in API responses
   - Don't log in application logs
   - Don't store in client-side storage

3. **Use EmailStr for email validation** - Automatic format checking
   ```python
   from pydantic import EmailStr
   email: EmailStr  # Validates email format
   ```

4. **Generic error messages** - Prevent information disclosure
   - Bad: "Email not found" / "Wrong password"
   - Good: "Invalid credentials"

**Quick Reference:**

```python
# Installation
uv add "passlib[bcrypt]"

# Setup
from passlib.context import CryptContext
pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

# Hash (for registration)
hashed = pwd_context.hash(password)

# Verify (for login)
if pwd_context.verify(provided_password, stored_hash):
    # Login success
```

**Security Checklist:**
- [ ] Never store plain text passwords
- [ ] Use bcrypt (not MD5, SHA1, or SHA256)
- [ ] Hash on the server, not client
- [ ] Use generic authentication errors
- [ ] Never log passwords or hashes