---
type: "ARCHITECTURE"
title: "Designing Multi-Provider Authentication"
---

## Provider Abstraction Pattern

When supporting multiple authentication providers (Google, Microsoft, GitHub), you need a clean abstraction that handles their differences while presenting a unified interface to your application. The key insight is that external providers give you external identities, but your application needs internal user records.

**The External Login Table Pattern:**
```
Users                    ExternalLogins
------                   --------------
Id (internal)            UserId (FK to Users)
Email                    Provider (Google, Microsoft, GitHub)
DisplayName              ProviderKey (external user ID)
CreatedAt                CreatedAt
```

One user can have multiple external logins linked. This allows users to sign in with any of their linked providers. A user who originally signed up with Google can later link their Microsoft account.

## User Linking Strategy

When a user authenticates via an external provider, you face a critical decision: is this a new user or an existing one?

**Email-Based Linking (Simple but Risky):**
If an external identity's email matches an existing user, automatically link them. This is convenient but dangerous - if the external provider doesn't verify emails, an attacker could claim any email address.

**Explicit Linking (Secure):**
Never automatically link based on email. Instead, require users to explicitly link accounts while logged in. User signs in with Google, then while authenticated, clicks 'Link Microsoft account' and goes through Microsoft's OAuth flow. Both identities are now linked to the same user record.

**Recommended Hybrid:**
Only auto-link if the external provider guarantees verified emails (Google, Microsoft with verified domains). Even then, consider requiring confirmation: 'We found an existing account with this email. Please verify by...'.

## Handling Provider Failures Gracefully

External providers can fail, go down for maintenance, or revoke your application's access. Your application must handle these scenarios gracefully:

**Temporary Failures:**
- Provider timeout during authentication: Show friendly error, offer retry
- Token refresh failure: Prompt user to re-authenticate
- Network issues: Implement retry with exponential backoff

**Permanent Failures:**
- Provider revokes your app's credentials: Have admin notification system
- User revokes access on provider's site: Handle gracefully on next login
- Provider discontinues OAuth support: Support multiple providers so users have alternatives

**Fallback Strategy:**
Consider offering password-based authentication as a fallback. Users who signed up with Google can 'Add password' to their account. If Google is down, they can still access their account.

## Security Considerations

**State Parameter:** Always use a cryptographically random state parameter to prevent CSRF attacks. Validate it matches when the user returns from the provider.

**PKCE for Public Clients:** If building a mobile app or SPA, use PKCE (Proof Key for Code Exchange) to protect the authorization code.

**Token Storage:** Store refresh tokens encrypted in your database. Never expose access tokens to client-side JavaScript if avoidable.

**Provider Verification:** Verify that ID tokens are actually signed by the provider. Use provider's JWKS (JSON Web Key Set) endpoint to validate signatures.

**Claim Validation:** Don't trust all claims blindly. Validate issuer, audience, expiration, and that the token was intended for your application.