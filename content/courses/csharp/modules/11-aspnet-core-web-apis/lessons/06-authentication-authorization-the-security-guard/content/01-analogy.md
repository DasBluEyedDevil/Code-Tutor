---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a concert venue:

AUTHENTICATION = 'Who are you?'
• Checking your ID at the entrance
• Proving your identity (username + password, or ticket)
• Result: You ARE who you claim to be

AUTHORIZATION = 'What can you do?'
• VIP pass gets backstage access
• General admission stays in crowd
• Result: You have PERMISSION for specific areas

ASP.NET Core 9 offers:
1. MapIdentityApi<TUser>() - NEW! Built-in Identity endpoints (/register, /login, /logout)
2. JWT Bearer Tokens - Stateless API authentication
3. Cookie Authentication - Session-based for web apps
4. External Providers - Google, Microsoft, GitHub login (OAuth/OpenID Connect)

MODERN APPROACH (.NET 8/9):
• MapIdentityApi for quick setup (handles registration, login, 2FA!)
• Bearer tokens for API clients
• Cookies for browser-based apps
• .RequireAuthorization() for endpoint protection

Think: 'Authentication = Login check, Authorization = Permission check!'