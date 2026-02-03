---
type: "KEY_POINT"
title: "Refresh Token Rotation"
---

## Key Takeaways

- **Rotate refresh tokens on every use** -- issue a new refresh token each time the old one is exchanged. Immediately invalidate the old token. This detects theft: if both the attacker and user try to use the same token, one fails.

- **Store refresh tokens server-side** -- save the token hash, expiration, and associated user ID in the database. Never rely solely on the client to manage token lifecycle.

- **Implement token families for theft detection** -- track which refresh token descended from which. If a revoked token is reused, invalidate the entire family and force re-authentication.
