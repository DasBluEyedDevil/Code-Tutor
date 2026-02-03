---
type: "KEY_POINT"
title: "Registration and Login Endpoints"
---

## Key Takeaways

- **Validate input before creating users** -- check for duplicate emails, enforce password complexity, and validate required fields. Return `BadRequest` with specific error messages for each validation failure.

- **Never return different errors for "user not found" vs "wrong password"** -- use a generic "Invalid credentials" message for both. Specific errors let attackers enumerate valid email addresses.

- **Hash passwords automatically** -- Identity's `CreateAsync(user, password)` hashes the password using PBKDF2 by default. Never store plain-text passwords or implement your own hashing.
