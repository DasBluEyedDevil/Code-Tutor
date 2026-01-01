---
type: "THEORY"
title: "OAuth 2.0 Flow Explained"
---

## The Authorization Code Flow

OAuth 2.0 defines several flows for different scenarios. For web applications, the Authorization Code flow is the most secure and commonly used. Let's walk through exactly what happens when a user clicks 'Sign in with Google':

**Step 1: Authorization Request**
Your application redirects the user to Google's authorization endpoint. The redirect URL includes your client ID, the scopes you are requesting (email, profile), a redirect URI where Google should send the user back, and a random state value to prevent CSRF attacks.

**Step 2: User Authentication**
The user sees Google's login page (if not already logged in) and then a consent screen showing what your application is requesting access to. The user can approve or deny the request. This happens entirely on Google's domain - your application never sees the user's Google password.

**Step 3: Authorization Code**
If the user approves, Google redirects back to your application's callback URL with an authorization code. This code is short-lived (typically 10 minutes) and can only be used once. The state parameter is also returned so you can verify it matches what you sent.

**Step 4: Token Exchange**
Your server sends the authorization code to Google's token endpoint, along with your client secret. This happens server-to-server, so the client secret is never exposed to the browser. Google validates the code and returns an access token (and optionally a refresh token and ID token).

**Step 5: Access Protected Resources**
Your application uses the access token to call Google's APIs on behalf of the user. The access token proves the user authorized this access. Access tokens are typically short-lived (1 hour) for security.

## Access Tokens vs ID Tokens

**Access Tokens** are credentials used to access protected resources (APIs). They are like permission slips that say 'the bearer of this token is allowed to access these specific resources.' Access tokens are opaque to your application - you don't need to understand their contents, just pass them to APIs.

**ID Tokens** are part of OpenID Connect (built on OAuth 2.0). They are JWTs containing verified claims about the user's identity: who they are, when they authenticated, and how. ID tokens are meant to be read by your application to establish the user's session.

## Scopes Control Access

Scopes define what your application can access. Common scopes include:
- `openid` - Required for OpenID Connect, returns ID token
- `profile` - User's name, picture, locale
- `email` - User's email address
- `offline_access` - Request refresh token for long-term access

Always request the minimum scopes needed. Users are more likely to trust applications that request less access.