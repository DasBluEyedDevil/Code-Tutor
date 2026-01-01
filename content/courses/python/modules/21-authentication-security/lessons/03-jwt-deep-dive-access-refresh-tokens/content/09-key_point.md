---
type: "KEY_POINT"
title: "Token Expiration Strategy"
---

**Choosing the right expiration times:**

**Access Token (15 minutes):**
- Short enough to limit damage if stolen
- Long enough to not annoy users with constant refreshes
- Stateless - server doesn't track active sessions

**Refresh Token (7-30 days):**
- Long enough for good UX (stay logged in)
- Short enough to limit persistent access
- Stored server-side for revocation capability

**Token Rotation:**
Each refresh issues a NEW refresh token and revokes the old one:
- Limits refresh token lifetime
- Detects token theft (reuse attempt fails)
- Forces regular token updates

**For Finance Tracker (financial data = higher security):**
- Access token: 15 minutes
- Refresh token: 7 days
- Require re-authentication for sensitive operations (transfers, password changes)