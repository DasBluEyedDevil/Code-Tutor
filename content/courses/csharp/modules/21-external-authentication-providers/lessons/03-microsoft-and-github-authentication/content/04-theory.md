---
type: "THEORY"
title: "Multi-Provider Strategy"
---

## Building a Provider-Agnostic Authentication System

When your application supports multiple OAuth providers, you need a strategy that treats all providers uniformly while handling their differences gracefully. The goal is a clean abstraction where your business logic does not care whether a user authenticated via Google, Microsoft, or GitHub.

**Common Claims Mapping**

Each provider returns claims in slightly different formats. Google might return email as ClaimTypes.Email, while GitHub returns it from an API call. Your first challenge is normalizing these into a consistent internal format.

Create a standardized set of claims that your application understands:
- `sub` or `user_id`: The unique identifier for the user
- `email`: The user's email address (verified when possible)
- `name`: Display name
- `picture`: Profile picture URL
- `provider`: Which OAuth provider authenticated this user
- `provider_id`: The user's ID at that specific provider

The OnCreatingTicket event in each provider's configuration is your opportunity to transform provider-specific claims into your standardized format. Add custom claims, merge data from API calls, and ensure consistent claim types across providers.

**The External Logins Pattern**

Your database should separate user identity from authentication method. A single ShopFlow user might authenticate via multiple providers. The standard pattern uses two related tables:

```
Users Table:
- Id (your internal user ID)
- Email
- DisplayName
- CreatedAt
- LastLoginAt

ExternalLogins Table:
- UserId (FK to Users)
- Provider ("Google", "Microsoft", "GitHub")
- ProviderKey (the user's ID at that provider)
- CreatedAt
```

When a user authenticates, you look up the ExternalLogins table by (Provider, ProviderKey). If found, you know which local user this is. If not found, either create a new user or prompt them to link to an existing account.

**Handling Email Collisions**

The trickiest scenario: a user signs in with GitHub using the same email as an existing Google user. Are they the same person? Maybe. They could also be an attacker who registered a GitHub account with someone else's email address.

Safe approaches:
1. **Require explicit linking**: If email matches but provider is new, require the user to prove ownership by also signing in with the existing provider
2. **Trust verified emails only**: Only consider auto-linking if both providers report email_verified = true
3. **Never auto-link**: Always create separate accounts; let users manually link them while authenticated

The most secure option is explicit linking, but it creates friction. Choose based on your security requirements and threat model.

**Unified Login Page Design**

Present all authentication options clearly. Common patterns:
- Social login buttons prominently displayed (Sign in with Google, Microsoft, GitHub)
- Clear visual hierarchy - primary providers larger or first
- Consistent button styling following each provider's brand guidelines
- Optional: traditional email/password form for users who prefer it

Avoid overwhelming users with too many options. If you support 10 providers, consider showing the top 3-4 and hiding others under 'More options'.