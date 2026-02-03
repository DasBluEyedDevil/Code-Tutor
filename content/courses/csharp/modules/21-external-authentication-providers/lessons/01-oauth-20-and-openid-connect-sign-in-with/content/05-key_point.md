---
type: "KEY_POINT"
title: "OAuth 2.0 Authorization Code Flow"
---

## Key Takeaways

- **The authorization code flow has four steps** -- redirect to provider, user consents, provider returns code to your callback, your server exchanges code for tokens. The client secret never reaches the browser.

- **Always verify the `state` parameter** -- send a random value in the authorization request and verify it matches in the callback. This prevents CSRF attacks where an attacker tricks a user into linking their account.

- **The ID token (OpenID Connect) contains user identity** -- the access token calls provider APIs. The ID token (JWT) contains claims like email and name. Validate its signature before trusting its contents.
