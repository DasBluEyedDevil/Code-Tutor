---
type: "WARNING"
title: "Security Pitfalls"
---

## Critical Security Issues to Avoid!

**Wrong middleware order**: UseAuthorization() before UseAuthentication() = EVERY request fails authorization! Remember: 'Who are you?' BEFORE 'What can you do?'

**JWT key too short**: Keys under 32 characters throw 'IDX10653: The encryption key is too small'. HmacSha256 requires 256 bits minimum!

**Storing JWT in localStorage**: JavaScript can read localStorage! XSS attack = stolen tokens. Use httpOnly cookies or short-lived tokens with refresh tokens.

**Hardcoding secrets**: NEVER put JWT keys in source code! Use Configuration, Environment Variables, or Azure Key Vault/AWS Secrets Manager.

**Not validating token claims**: Always set ValidateIssuer, ValidateAudience, ValidateIssuerSigningKey to TRUE. Attackers can modify tokens if signature isn't verified!

**MapIdentityApi exposes all endpoints**: Consider using 'app.MapGroup("/auth").MapIdentityApi<User>()' to prefix routes and add rate limiting.