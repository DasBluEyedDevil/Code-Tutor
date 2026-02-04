---
type: WARNING
---

**OAuth redirect URIs must match exactly.** If the redirect URI registered with the OAuth provider (Google, Apple, etc.) does not match the URI your app sends character-for-character, authentication will silently fail or show a cryptic error. Common mismatches include:

- Trailing slash: `https://example.com/callback` vs `https://example.com/callback/`
- Scheme case: `myapp://` vs `MyApp://`
- Missing port: `http://localhost:3000/callback` vs `http://localhost/callback`

On mobile, use custom URL schemes (e.g., `com.yourapp:/oauth-callback`) and register them in both `AndroidManifest.xml` and `Info.plist`. If the scheme is not registered on the platform side, the browser will not redirect back to your app after the user authenticates, leaving them stuck on a blank page.

Test OAuth flows on a physical device early in development -- emulator redirect behavior sometimes differs from real devices.
