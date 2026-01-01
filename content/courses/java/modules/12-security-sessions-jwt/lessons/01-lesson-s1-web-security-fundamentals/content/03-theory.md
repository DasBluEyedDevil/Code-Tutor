---
type: "THEORY"
title: "OWASP Top 10: Critical Security Risks"
---

OWASP (Open Web Application Security Project) tracks the most critical risks:

1. BROKEN ACCESS CONTROL (A01:2021)
- Users access unauthorized functions/data
- Example: Changing URL from /user/profile to /admin/panel

2. CRYPTOGRAPHIC FAILURES (A02:2021)
- Weak encryption, exposed sensitive data
- Example: Storing passwords in plain text

3. INJECTION (A03:2021)
- SQL, NoSQL, OS command injection
- Example: SELECT * FROM users WHERE id = '1; DROP TABLE users;--'

4. INSECURE DESIGN (A04:2021)
- Missing security controls by design
- Example: No rate limiting on login attempts

5. SECURITY MISCONFIGURATION (A05:2021)
- Default credentials, unnecessary features enabled
- Example: Debug mode left on in production

6. VULNERABLE COMPONENTS (A06:2021)
- Using libraries with known vulnerabilities
- Example: Log4j vulnerability (CVE-2021-44228)

7. AUTHENTICATION FAILURES (A07:2021)
- Weak passwords, session fixation
- Example: Allowing 'password123'

8. DATA INTEGRITY FAILURES (A08:2021)
- Untrusted deserialization, CI/CD compromise

9. LOGGING FAILURES (A09:2021)
- Not detecting breaches due to poor logging

10. SSRF (A10:2021)
- Server-side request forgery