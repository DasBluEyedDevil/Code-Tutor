---
type: "WARNING"
title: "Security Mindset"
---

NEVER trust user input. EVER.

Everything from the client is potentially malicious:
- Form fields
- URL parameters
- HTTP headers
- Cookies
- File uploads

Validate on the SERVER, not just client-side:

// Client validation (easily bypassed)
<input type="email" required>  // Attacker ignores this

// Server validation (required)
if (!isValidEmail(email)) {
    throw new ValidationException("Invalid email");
}

Principle of LEAST PRIVILEGE:
- Give minimum permissions needed
- Admin access only when required
- Time-limited elevated access

FAIL SECURELY:
- On error, deny access (not grant)
- Don't expose error details to users
- Log security events for investigation