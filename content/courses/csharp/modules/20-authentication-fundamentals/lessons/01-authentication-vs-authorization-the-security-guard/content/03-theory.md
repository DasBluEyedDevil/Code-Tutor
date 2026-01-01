---
type: "THEORY"
title: "Authentication Schemes"
---

## Authentication Schemes in ASP.NET Core

ASP.NET Core supports multiple authentication schemes, each designed for different scenarios. Understanding when to use each is crucial for building secure applications.

### Cookie Authentication (Web Applications)

Cookies are the traditional choice for web applications where users interact through browsers. After successful login, the server creates an encrypted cookie containing the user's identity. The browser automatically sends this cookie with every request.

**Pros:**
- Automatic - browsers handle cookie management
- Secure when configured correctly (HttpOnly, Secure, SameSite)
- Works seamlessly with server-rendered pages
- Built-in sliding expiration support

**Cons:**
- Vulnerable to CSRF attacks if SameSite not configured
- Tied to single domain (cross-domain issues)
- Not ideal for mobile apps or SPAs making API calls
- Stateful (server may need to track sessions)

### JWT (JSON Web Token) Authentication (APIs)

JWTs are self-contained tokens that encode user claims. They're perfect for APIs consumed by mobile apps, SPAs, or microservices. The token contains everything needed to verify the user's identity.

**Pros:**
- Stateless - server doesn't store session data
- Works across domains and services
- Perfect for mobile apps and microservices
- Can contain custom claims (roles, permissions)

**Cons:**
- Token size can grow with many claims
- Cannot be invalidated before expiration (without extra infrastructure)
- Must implement refresh token logic for long sessions
- Requires careful storage on client (not localStorage for sensitive apps)

### OAuth 2.0 and OpenID Connect (Social Login & Enterprise SSO)

OAuth 2.0 is a delegation protocol - "Let Google verify who I am." OpenID Connect adds identity layer on top. Use these for social login (Google, Microsoft, GitHub) or enterprise single sign-on.

**Pros:**
- Delegate authentication to trusted providers
- Users don't need another password
- Enterprise SSO with Azure AD, Okta
- Standardized, well-tested protocols

**Cons:**
- More complex to implement
- Dependency on external providers
- Requires redirect flows
- Token refresh and validation complexity

### Choosing the Right Scheme

| Scenario | Recommended Scheme |
|----------|-------------------|
| Traditional MVC web app | Cookie |
| REST API for mobile app | JWT Bearer |
| SPA with backend API | JWT Bearer (or BFF with cookies) |
| Microservices communication | JWT Bearer |
| Social login (Google, GitHub) | OAuth 2.0 / OIDC |
| Enterprise SSO | OIDC with Azure AD/Okta |