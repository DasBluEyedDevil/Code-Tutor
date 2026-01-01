---
type: "ARCHITECTURE"
title: "Security Architecture Decisions"
---

## Defense in Depth

Security is not a single checkpoint - it's multiple layers of protection. Even if one layer fails, others continue to protect your application. Think of it like a medieval castle: walls, moat, guards, locked doors, and a safe for valuables.

**Layer 1 - Network Security**
- HTTPS everywhere (TLS 1.3)
- Web Application Firewall (WAF)
- DDoS protection
- Rate limiting at edge (CDN/load balancer)

**Layer 2 - Application Authentication**
- Strong password policies
- Multi-factor authentication (MFA)
- Brute-force protection (account lockout)
- Secure token generation

**Layer 3 - Authorization & Access Control**
- Principle of least privilege
- Role-based access control (RBAC)
- Resource-based authorization
- API scopes and permissions

**Layer 4 - Data Protection**
- Encryption at rest
- Encryption in transit
- Secrets management (Key Vault)
- Data masking and redaction

## Token Storage Comparison

Where you store tokens on the client matters enormously for security:

| Storage Location | XSS Vulnerable | CSRF Vulnerable | Recommendation |
|-----------------|----------------|-----------------|----------------|
| HttpOnly Cookie | No | Yes (mitigate with SameSite) | Best for web apps |
| localStorage | Yes | No | Avoid for sensitive tokens |
| sessionStorage | Yes | No | Slightly better than localStorage |
| Memory (JS variable) | Yes | No | Good for short-lived tokens |
| Secure HttpOnly Cookie + CSRF Token | No | No | Most secure for web |

**The HttpOnly Cookie Advantage:**
When tokens are in HttpOnly cookies, JavaScript cannot access them. This means even if an attacker injects malicious scripts (XSS), they cannot steal the authentication token. Combined with SameSite=Strict, you're protected from both XSS and CSRF.

**The localStorage Trap:**
Many tutorials show storing JWTs in localStorage for simplicity. This is dangerous! Any XSS vulnerability exposes the token. Attackers can read localStorage from injected scripts and exfiltrate tokens to their server.

## Session Management Best Practices

1. **Token Expiration Strategy**
   - Access tokens: Short-lived (15-60 minutes)
   - Refresh tokens: Longer-lived (hours to days)
   - Sliding expiration for active users
   - Absolute expiration for compliance

2. **Refresh Token Rotation**
   - Issue new refresh token with each use
   - Invalidate old refresh token immediately
   - Detect token reuse (possible theft)
   - Maintain refresh token family tracking

3. **Session Invalidation**
   - Logout clears all tokens
   - Password change invalidates all sessions
   - Admin can revoke user sessions
   - Suspicious activity triggers session termination

4. **Secure Cookie Settings**
   ```csharp
   options.Cookie.HttpOnly = true;     // No JavaScript access
   options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS only
   options.Cookie.SameSite = SameSiteMode.Strict; // Same-origin only
   ```

5. **Token Binding (Advanced)**
   - Bind tokens to client fingerprint
   - Detect token theft through context changes
   - Consider device/IP validation for sensitive operations