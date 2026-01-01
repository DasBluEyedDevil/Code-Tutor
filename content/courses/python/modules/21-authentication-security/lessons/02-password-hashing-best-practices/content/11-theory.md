---
type: "THEORY"
title: "Password Storage Checklist"
---

**Before deploying to production, verify:**

1. **Algorithm**: Using bcrypt, Argon2, or scrypt (NEVER MD5/SHA)
2. **Cost Factor**: Appropriate for your hardware (~250-500ms)
3. **Salt**: Unique per password (automatic with passlib)
4. **Validation**: Minimum 12 characters, complexity requirements
5. **Comparison**: Constant-time (automatic with passlib)
6. **Transmission**: Passwords sent over HTTPS only
7. **Logging**: NEVER log passwords (even hashed ones are sensitive)
8. **Memory**: Clear password from memory after hashing
9. **Upgrade Path**: Can rehash when algorithms improve
10. **Rate Limiting**: Protect login endpoint from brute force