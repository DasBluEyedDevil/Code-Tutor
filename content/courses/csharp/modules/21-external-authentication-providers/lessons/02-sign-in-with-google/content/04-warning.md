---
type: "WARNING"
title: "OAuth Security Considerations"
---

## Critical Security Practices for OAuth Implementation

OAuth is secure when implemented correctly, but small mistakes can create serious vulnerabilities. These are the most important security considerations when integrating Google authentication into ShopFlow.

**Always Verify the State Parameter**
The state parameter prevents Cross-Site Request Forgery (CSRF) attacks. When initiating OAuth, generate a cryptographically random state value, store it in the user's session, and include it in the authorization URL. When Google redirects back, verify the returned state matches exactly. ASP.NET Core's OAuth middleware handles this automatically, but if you ever implement custom OAuth flows, this verification is mandatory. An attacker who tricks a user into clicking a crafted callback URL could otherwise hijack authentication.

**Use PKCE for Public Clients**
If you're building a mobile app, desktop application, or single-page application (SPA), you MUST implement PKCE (Proof Key for Code Exchange, pronounced 'pixie'). PKCE adds an additional layer of protection by generating a code_verifier and code_challenge. Without PKCE, authorization codes could be intercepted and used by attackers. For server-rendered web applications like ShopFlow's backend, the CLIENT_SECRET provides equivalent protection, but PKCE is still recommended as defense in depth.

**Never Exchange Tokens on the Client**
The token exchange (step 4) MUST happen server-to-server. Your CLIENT_SECRET is called a 'secret' for a reason - if it is ever exposed to browsers, mobile apps, or client-side JavaScript, attackers can impersonate your application entirely. They could create a phishing site that looks legitimate to users because it has valid OAuth credentials. There is no recovery from a leaked CLIENT_SECRET except rotating credentials and hoping users have not been compromised.

**Check email_verified Before Trusting Email Claims**
Not all Google accounts have verified email addresses. While Google typically requires email verification, there are edge cases (accounts created through specific flows, old accounts, organizational accounts) where email_verified might be false. If you automatically link accounts based on email address, an attacker could create a Google account with someone else's unverified email and gain access to their ShopFlow account. Always check the email_verified claim and handle unverified emails appropriately - either require additional verification or refuse to link the account.

**Validate Token Signatures**
ID tokens are JWTs (JSON Web Tokens) signed by Google's private key. Before trusting any claims in the token, verify the signature using Google's public keys (available at their JWKS endpoint). Also validate the token's audience matches your CLIENT_ID (preventing tokens meant for other applications from being accepted), the issuer is Google, and the token has not expired. ASP.NET Core's Google authentication middleware handles this automatically, but understanding it helps you avoid security issues when working with tokens directly.

**Store Tokens Securely**
If you save access tokens or refresh tokens (for calling Google APIs later), encrypt them in your database. Refresh tokens are particularly sensitive because they provide long-term access. A database breach that exposes unencrypted tokens would allow attackers to access users' Google resources even after you rotate your OAuth credentials.

**Implement Token Revocation Checking**
Users can revoke your application's access through their Google Account settings at any time. Consider periodically validating that saved tokens are still valid, especially before performing sensitive operations on behalf of users.