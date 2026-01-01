---
type: "THEORY"
title: "Email Registration"
---


**Implementing User Registration**

Serverpod's auth module provides built-in email registration, but we'll extend it with custom validation and our UserProfile creation.

**Registration Flow**

```
1. User submits email + password
         ↓
2. Validate email format
         ↓
3. Check password strength
         ↓
4. Check if email exists
         ↓
5. Hash password (bcrypt)
         ↓
6. Create UserInfo record
         ↓
7. Create UserProfile record
         ↓
8. Send verification email
         ↓
9. Return success + user data
```

**Password Requirements**

A strong password policy protects users:

| Rule | Minimum |
|------|--------|
| Length | 8 characters |
| Uppercase | 1 letter |
| Lowercase | 1 letter |
| Number | 1 digit |
| Special | 1 character (!@#$%^&*) |

**Email Verification**

Why require email verification?

1. **Proves ownership** - User controls the email address
2. **Enables recovery** - Password reset requires verified email
3. **Reduces spam** - Harder to create fake accounts
4. **Improves deliverability** - Verified emails are less likely to bounce

**Rate Limiting**

Protect against abuse:

- Max 5 registration attempts per IP per hour
- Max 3 verification code resends per email per hour
- Exponential backoff on failed attempts

