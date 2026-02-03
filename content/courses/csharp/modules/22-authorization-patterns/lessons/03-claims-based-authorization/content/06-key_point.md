---
type: "KEY_POINT"
title: "Claims-Based Authorization"
---

## Key Takeaways

- **Claims are key-value facts about a user** -- `("department", "Engineering")`, `("subscription", "premium")`, `("clearance_level", "3")`. Unlike binary roles, claims carry data that policies can evaluate.

- **Policies interpret claims** -- `RequireClaim("subscription", "premium", "enterprise")` checks if the user has a subscription claim with either value. The policy defines the authorization rule, not the claim itself.

- **Claims come from the identity provider** -- add claims during login (from your database) or from external OAuth providers. Transform external claims to your internal format using `ClaimsTransformation`.
