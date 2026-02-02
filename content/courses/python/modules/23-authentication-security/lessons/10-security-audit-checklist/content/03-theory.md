---
type: "THEORY"
title: "Penetration Testing Basics"
---

**Penetration testing** (pentesting) simulates real attacks to find vulnerabilities before malicious actors do.

**Types of Penetration Tests:**

**1. Black Box Testing**
- Tester has no prior knowledge of the system
- Simulates external attacker
- Most realistic but time-consuming

**2. White Box Testing**
- Tester has full access to code and documentation
- More thorough coverage
- Can find logic flaws

**3. Gray Box Testing**
- Partial knowledge (like an authenticated user)
- Balance of coverage and realism

**OWASP Testing Methodology:**

1. **Information Gathering**
   - Enumerate endpoints and parameters
   - Identify technologies used
   - Map application functionality

2. **Configuration Testing**
   - Check for default credentials
   - Review security headers
   - Test TLS configuration

3. **Authentication Testing**
   - Test password policies
   - Check for brute force protection
   - Test password reset flow
   - Look for credential stuffing vulnerabilities

4. **Authorization Testing**
   - Test access controls (can user A access user B's data?)
   - Check role-based permissions
   - Test privilege escalation

5. **Session Management Testing**
   - Test session token randomness
   - Check session expiration
   - Test concurrent session handling

6. **Input Validation Testing**
   - SQL injection
   - XSS (Cross-Site Scripting)
   - Command injection
   - Path traversal

7. **Error Handling Testing**
   - Do errors leak sensitive information?
   - Are stack traces exposed?

8. **Business Logic Testing**
   - Can steps be skipped?
   - Can limits be bypassed?
   - Race conditions?

**Common Tools:**
- **Burp Suite**: Web security testing platform
- **OWASP ZAP**: Open-source alternative to Burp
- **sqlmap**: Automated SQL injection testing
- **Nikto**: Web server scanner
- **nmap**: Network discovery and security auditing

**For Finance Tracker, test:**
- Can users access other users' transactions?
- Can transaction amounts be manipulated?
- Is the password reset secure?
- Can rate limits be bypassed?
- Are JWT tokens properly validated?