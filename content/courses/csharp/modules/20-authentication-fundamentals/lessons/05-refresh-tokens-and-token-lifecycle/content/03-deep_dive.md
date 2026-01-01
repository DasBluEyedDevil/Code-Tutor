---
type: "DEEP_DIVE"
title: "Token Rotation and Security"
---

## Why Rotate Refresh Tokens?

Token rotation means issuing a new refresh token every time the old one is used, immediately invalidating the old token. This seemingly simple practice provides powerful security benefits.

### The Attack Scenario Without Rotation

Imagine an attacker intercepts a user's refresh token through a man-in-the-middle attack, malware, or a compromised network. With a non-rotating token that is valid for 30 days:

1. **Day 1:** Attacker obtains the refresh token
2. **Days 1-30:** Attacker can silently access the user's account
3. The user notices nothing wrong - their token still works too
4. You have no way to detect this unless the user manually notices unauthorized activity

### The Same Scenario With Rotation

1. **Day 1:** Attacker obtains the refresh token
2. **Day 2:** User refreshes their token - gets Token B, Token A is invalidated
3. **Day 3:** Attacker tries to use Token A - DETECTED!
4. System revokes all tokens in the family
5. Both user and attacker are logged out
6. User re-authenticates; attacker cannot

The key insight: **token reuse becomes detectable**. A refresh token should only ever be used once. If it is used twice, someone has a copy they should not have.

### Token Families for Reuse Detection

A token family is a chain of rotated tokens that all trace back to an original authentication. When a user logs in, they start a new family. Each rotation creates a new token in the same family.

```
Login -> Token A (Family: abc-123)
         |
         v Rotation
       Token B (Family: abc-123, replaced A)
         |
         v Rotation  
       Token C (Family: abc-123, replaced B)
```

When reuse is detected (Token B used after Token C was issued), we revoke the ENTIRE family. This terminates both the legitimate session and the attacker's session. Yes, the user is inconvenienced, but the attacker is stopped, and the user is alerted to the security incident.

### Implementation Considerations

**Race Conditions:** What if a user has multiple browser tabs that simultaneously try to refresh? One will succeed, and the others will fail with 'reuse detected'. Solution: implement a short grace period (10-30 seconds) where the previous token remains valid, or handle the error client-side by retrying with the new token.

**Offline Clients:** Mobile apps that go offline may have stale refresh tokens. When they come back online and their token has been rotated, they will be logged out. This is correct security behavior - if you cannot verify the chain of custody, require re-authentication.

**Token Storage:** Refresh tokens must be stored securely. Unlike access tokens (which can be validated cryptographically), refresh tokens require a database lookup. This lookup should use a hashed version of the token, never the plaintext.

### Cleanup and Maintenance

Refresh tokens accumulate in the database as users log in and out. Implement periodic cleanup:

```csharp
// Run daily via background service
public async Task CleanupExpiredTokensAsync()
{
    var cutoff = DateTime.UtcNow.AddDays(-30);
    
    // Delete tokens that expired more than 30 days ago
    await _context.RefreshTokens
        .Where(t => t.ExpiresAt < cutoff)
        .ExecuteDeleteAsync();
}
```

Keep revoked tokens for some time (30 days) for security auditing, then delete them. This provides an audit trail for investigating security incidents.