---
type: "ANALOGY"
title: "Different Providers, Same Pattern"
---

Imagine walking down a street with multiple coffee shops, each with their own loyalty program. Starbucks has a green card with stars for points. Dunkin has an orange card with DD Perks. Peet's has a brown card with Peetnik Rewards. Each card looks completely different - different colors, different logos, different point systems - but they all follow the same fundamental pattern: you present the card, they scan it, and you get credit for your purchase.

OAuth providers work exactly the same way. Google, Microsoft, and GitHub are like different coffee shop chains. Each has its own branding, its own developer console, its own specific configuration quirks. Google has the Cloud Console with its project-based organization. Microsoft has the Azure Portal with app registrations and complex tenant configurations. GitHub has a simple OAuth Apps page under Developer Settings.

But underneath these superficial differences, they all implement the same OAuth 2.0 and OpenID Connect protocols - the same authorization code flow, the same token exchange, the same basic claims about user identity. Just as you know that every loyalty card will be scanned at checkout regardless of which coffee shop you are in, every OAuth provider will redirect users to an authorization page, collect consent, and return an authorization code to your callback URL.

This means that once you truly understand how Google authentication works, adding Microsoft and GitHub is remarkably straightforward. You are not learning three completely different systems - you are learning one system with three different branding skins. The configuration properties are nearly identical: ClientId, ClientSecret, scopes, callback URL. The claims you receive are the same types: name, email, unique identifier. The security considerations are universal: protect your client secret, verify state parameters, validate tokens.

The real skill is recognizing this pattern and building your application to treat all providers uniformly. Your user database should not care whether someone authenticated via Google or Microsoft - it should see a verified identity with an email and name. Your authorization logic should not have special cases for GitHub users versus Google users. Build a provider-agnostic system, and adding the fourth, fifth, or tenth OAuth provider becomes a configuration change rather than an architectural overhaul.