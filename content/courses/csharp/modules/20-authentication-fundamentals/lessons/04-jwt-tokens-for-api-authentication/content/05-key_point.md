---
type: "KEY_POINT"
title: "JWT Token Configuration"
---

## Key Takeaways

- **Keep access tokens short-lived (15 minutes)** -- short expiration limits the damage if a token is stolen. Pair with refresh tokens for seamless user experience without frequent re-authentication.

- **Store the secret key in User Secrets or Azure Key Vault** -- never hardcode JWT signing keys in appsettings.json. Use `dotnet user-secrets` for development and Key Vault for production.

- **Validate issuer, audience, and lifetime** -- configure `TokenValidationParameters` to check all three. Without these checks, tokens from other applications or expired tokens would be accepted.
