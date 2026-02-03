---
type: "KEY_POINT"
title: "Multi-Provider Authentication Strategy"
---

## Key Takeaways

- **Normalize claims across providers** -- Google, Microsoft, and GitHub return different claim formats. Map them to a consistent internal format (`email`, `name`, `provider`, `provider_id`) so your business logic is provider-agnostic.

- **Each provider has unique setup** -- Google uses Cloud Console, Microsoft uses Azure AD App Registration, GitHub uses Developer Settings. The OAuth flow is the same, but registration and scope names differ.

- **Design for adding new providers** -- structure your authentication code so adding a fourth or fifth provider requires only configuration changes, not architectural modifications.
