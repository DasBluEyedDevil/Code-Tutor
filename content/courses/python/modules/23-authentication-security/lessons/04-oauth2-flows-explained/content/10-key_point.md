---
type: "KEY_POINT"
title: "OAuth2 Best Practices Summary"
---

**Security Checklist for OAuth2 Implementation:**

1. **Always use HTTPS** - Tokens are sensitive
2. **Validate state parameter** - Prevents CSRF attacks
3. **Use PKCE** - Required for mobile/SPA, good for all
4. **Validate redirect URIs exactly** - No partial matches
5. **Verify ID tokens** - Check signature, issuer, audience
6. **Store secrets securely** - Environment variables, not code
7. **Request minimal scopes** - Only what you need
8. **Implement token refresh** - Handle expiration gracefully
9. **Allow account unlinking** - Users should control connections
10. **Log authentication events** - For security monitoring

**For Finance Tracker:**
- Use Authorization Code + PKCE for all clients
- Store refresh tokens securely (HttpOnly cookies)
- Require re-authentication for sensitive operations
- Allow users to see and revoke connected accounts