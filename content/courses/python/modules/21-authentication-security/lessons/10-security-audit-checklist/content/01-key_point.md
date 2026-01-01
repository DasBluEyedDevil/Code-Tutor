---
type: "KEY_POINT"
title: "Pre-Deployment Security Checklist"
---

**Before any production deployment, verify these security controls:**

**Authentication & Authorization**
- [ ] All endpoints require authentication (except public routes)
- [ ] Password hashing uses bcrypt/Argon2 with appropriate cost
- [ ] JWT tokens have short expiry (15-30 min for access tokens)
- [ ] Refresh token rotation is implemented
- [ ] Role-based access control is enforced
- [ ] Users can only access their own data (IDOR prevention)

**Input Validation & Injection**
- [ ] All user input is validated (type, length, format)
- [ ] SQL queries use parameterized statements
- [ ] HTML output is properly escaped (XSS prevention)
- [ ] File uploads are validated and sandboxed
- [ ] JSON parsing has depth limits

**Session & Secrets**
- [ ] Session cookies are HttpOnly and Secure
- [ ] CSRF protection is enabled
- [ ] No secrets in code or version control
- [ ] Environment variables for all sensitive config
- [ ] Secrets can be rotated without code changes

**Transport & Encryption**
- [ ] TLS 1.2+ required for all connections
- [ ] HSTS header is set (force HTTPS)
- [ ] Sensitive data encrypted at rest
- [ ] Database connections use SSL

**Rate Limiting & DoS Protection**
- [ ] Rate limiting on all endpoints
- [ ] Stricter limits on auth endpoints (login, register)
- [ ] Request size limits configured
- [ ] Timeout limits for long-running operations

**Logging & Monitoring**
- [ ] Security events are logged
- [ ] Logs don't contain sensitive data
- [ ] Alerting for suspicious patterns
- [ ] Audit trail for data modifications

**Error Handling**
- [ ] Error messages don't reveal internal details
- [ ] Stack traces hidden in production
- [ ] Consistent error format (doesn't leak info)

**Dependencies**
- [ ] All dependencies updated to latest patch versions
- [ ] No known vulnerabilities (CVEs) in dependencies
- [ ] Dependency audit passes (`pip-audit`, `npm audit`)

**Infrastructure**
- [ ] Firewall rules are restrictive
- [ ] Database not directly exposed to internet
- [ ] Admin interfaces protected/VPN only
- [ ] Backup and recovery tested