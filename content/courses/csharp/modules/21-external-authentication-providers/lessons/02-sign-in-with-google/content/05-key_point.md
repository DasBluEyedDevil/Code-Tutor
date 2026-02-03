---
type: "KEY_POINT"
title: "Google Authentication Setup"
---

## Key Takeaways

- **Store Client ID and Client Secret securely** -- use `dotnet user-secrets` for development: `dotnet user-secrets set "Google:ClientId" "your-id"`. Never commit credentials to source control.

- **Request minimal scopes** -- only ask for `email` and `profile` unless your application genuinely needs more. Users are more likely to approve narrow permission requests.

- **The OAuth callback must match exactly** -- the redirect URI registered in Google Cloud Console must match your application's callback URL character for character. Mismatches cause authentication failures.
