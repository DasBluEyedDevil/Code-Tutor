---
type: "KEY_POINT"
title: "Access Tokens vs Refresh Tokens"
---

Production JWT systems use TWO tokens:

ACCESS TOKEN:
- Short-lived (15 minutes - 1 hour)
- Contains user info, roles
- Sent with every API request
- If stolen, limited damage window

REFRESH TOKEN:
- Long-lived (7-30 days)
- Used ONLY to get new access tokens
- Stored more securely (httpOnly cookie)
- Can be revoked server-side

FLOW:
1. Login -> Get access token + refresh token
2. Use access token for API calls
3. Access token expires
4. Send refresh token to /api/auth/refresh
5. Get new access token
6. Continue API calls

WHY TWO TOKENS?
- Access tokens are stateless (no DB lookup)
- But can't be revoked once issued
- Short expiration limits damage
- Refresh tokens CAN be revoked (stored in DB)
- User can "logout everywhere" by revoking refresh tokens