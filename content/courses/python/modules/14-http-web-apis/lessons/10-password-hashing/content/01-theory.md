---
type: "THEORY"
title: "Why Password Hashing?"
---

**Never Store Plain Text Passwords**

One of the most critical security rules in software development: **NEVER store passwords in plain text**. If your database is breached, attackers would have immediate access to every user's password.

**Hashing is One-Way**

Unlike encryption (which can be reversed with a key), hashing is a **one-way function**:
- Input: `"secretpassword123"` → Output: `"$2b$12$LQv3c1y..."` (irreversible)
- You cannot convert the hash back to the original password
- To verify a password, you hash the attempt and compare hashes

**Why Bcrypt?**

Bcrypt is the **industry standard** for password hashing:

1. **Intentionally Slow** - Takes ~100ms to hash, making brute-force attacks impractical
2. **Built-in Salt** - Each password gets a unique random salt
3. **Adjustable Cost** - Can increase computation as hardware gets faster
4. **Battle-Tested** - Used by major companies for decades

**Salt Prevents Rainbow Table Attacks**

A **rainbow table** is a precomputed table of common password hashes. Without salt:
- `hash("password123")` always produces the same hash
- Attackers can look up hashes in rainbow tables

With salt (random data added before hashing):
- `hash("password123" + random_salt)` produces a unique hash every time
- Rainbow tables become useless
- Bcrypt handles salting automatically

**Security Comparison:**

| Method | Security | Use Case |
|--------|----------|----------|
| Plain text | None | Never! |
| MD5/SHA1 | Weak | Never for passwords |
| SHA256 | Medium | Not for passwords |
| Bcrypt | Strong | Passwords |
| Argon2 | Strongest | Passwords (newer) |