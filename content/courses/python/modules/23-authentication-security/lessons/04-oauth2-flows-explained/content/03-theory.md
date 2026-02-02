---
type: "THEORY"
title: "OAuth2 Grant Types (Flows)"
---

OAuth2 defines several flows for different use cases:

**1. Authorization Code Flow (Most Secure)**
- For server-side web apps
- Secret stays on server
- User redirected to authorize
- Best for Finance Tracker

**2. Authorization Code + PKCE**
- For mobile apps and SPAs
- No client secret needed
- Uses code verifier/challenge
- Recommended for all public clients

**3. Client Credentials**
- For server-to-server (no user)
- App authenticates as itself
- Used for backend services

**4. Implicit Flow (Deprecated)**
- Token in URL fragment
- Security issues, don't use
- Replaced by PKCE

**5. Resource Owner Password (Deprecated)**
- User gives password to app
- Defeats OAuth purpose
- Only for legacy migration