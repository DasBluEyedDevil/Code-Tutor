---
type: "KEY_POINT"
title: "Auth Security Checklist"
---


**Production Authentication Checklist**

**Password Security**
- [ ] Minimum 8 characters, require complexity
- [ ] Use bcrypt/Argon2 for hashing (Serverpod does this)
- [ ] Never log or store plain passwords
- [ ] Implement account lockout after failed attempts

**Token Security**
- [ ] Short-lived access tokens (15-60 minutes)
- [ ] Secure storage for refresh tokens
- [ ] Token rotation on refresh
- [ ] Ability to revoke all sessions

**OAuth Security**
- [ ] Verify tokens server-side
- [ ] Validate audience/client ID
- [ ] Check token expiration
- [ ] Use state parameter to prevent CSRF

**Session Management**
- [ ] Invalidate session on password change
- [ ] Track active sessions per user
- [ ] Allow users to view/revoke sessions
- [ ] Automatic session cleanup

**Email Security**
- [ ] Verify email ownership
- [ ] Rate limit verification emails
- [ ] Secure password reset flow
- [ ] Notify on email change

**Account Protection**
- [ ] Rate limit login attempts
- [ ] Log security events
- [ ] Notify on new device login
- [ ] Support account recovery

**Data Protection**
- [ ] HTTPS only
- [ ] Don't expose user IDs in URLs
- [ ] Implement GDPR export/delete
- [ ] Audit log for sensitive operations

