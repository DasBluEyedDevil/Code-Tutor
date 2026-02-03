---
type: "WARNING"
title: "Common Pitfalls"
---

## Claims-Based Authorization Pitfalls

**Trusting Claims Without Validation**: Claims in a JWT are only as trustworthy as the token's signature. If signature validation is disabled or the signing key is compromised, anyone can forge claims. Always validate the token issuer, audience, and signature before trusting any claim values.

**Stale Claims in Long-Lived Tokens**: If a user's role or permissions change (demotion, account suspension), their JWT still contains the old claims until it expires. Short-lived access tokens (15 minutes) combined with refresh token rotation mitigate this. For immediate revocation, maintain a token deny-list or check permissions server-side.

**Claim Type Mismatch**: ASP.NET Core maps JWT claim types differently depending on configuration. The JWT standard uses `"role"` but .NET's ClaimTypes.Role is `"http://schemas.microsoft.com/ws/2008/06/identity/claims/role"`. Set `RoleClaimType = ClaimTypes.Role` in TokenValidationParameters to ensure `[Authorize(Roles = "Admin")]` works correctly with your JWT claims.

**Too Many Claims Bloating Token Size**: Every claim increases JWT size, and JWTs travel with every HTTP request. A token with 50 custom claims creates noticeable overhead. Keep claims minimal (user ID, role, essential permissions) and look up detailed permissions from the database when needed.
