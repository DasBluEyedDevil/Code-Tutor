---
type: "WARNING"
title: "JWT Security Pitfalls"
---

## Common JWT Security Mistakes

### Weak or Exposed Secret Keys

**The Mistake:** Using short, simple, or hardcoded secret keys in source code.

**Examples of Bad Keys:**
```csharp
// DON'T DO THESE!
var key = "secret";              // Too short, too simple
var key = "my-jwt-secret-key";   // Predictable pattern
var key = "shopflow123";         // Contains app name, simple
```

**The Risk:** Short keys can be brute-forced. Predictable keys can be guessed. Hardcoded keys get committed to source control and exposed in code reviews, logs, or breaches.

**The Fix:**
```csharp
// Generate a strong key (run once, store securely)
var randomBytes = new byte[64];
using var rng = RandomNumberGenerator.Create();
rng.GetBytes(randomBytes);
var strongKey = Convert.ToBase64String(randomBytes);
// Store in User Secrets (development) or Key Vault (production)
```

Minimum key length: 256 bits (32 bytes) for HS256. Use 512 bits (64 bytes) for extra security.

### Storing Sensitive Data in Token Payload

**The Mistake:** Including passwords, SSN, credit cards, or internal system details in JWT claims.

**The Risk:** JWT payloads are Base64 encoded, NOT encrypted. Anyone can decode and read them:
```bash
echo 'eyJzdWIiOiIxMjM0NTY3ODkwIiwiY3JlZGl0X2NhcmQiOiI0MTExLTExMTEtMTExMS0xMTExIn0' | base64 -d
# Output: {"sub":"1234567890","credit_card":"4111-1111-1111-1111"}
```

**The Fix:** Only include non-sensitive, necessary claims:
```csharp
// SAFE claims
new Claim("sub", user.Id),
new Claim("name", user.DisplayName),
new Claim("role", "Customer"),
new Claim("email_verified", "true")

// NEVER include
// - Passwords or hashes
// - Financial data
// - Social security numbers
// - Medical information
// - Internal IDs that expose system architecture
```

### Long Token Expiration Times

**The Mistake:** Setting access tokens to expire in days or weeks for 'user convenience'.

**The Risk:** If a token is stolen, the attacker has a long window to use it. Unlike cookies, JWTs cannot be invalidated without additional infrastructure.

**The Fix:** Keep access tokens short-lived:
```csharp
// Access tokens: 15-60 minutes
expires: DateTime.UtcNow.AddMinutes(15)

// Use refresh tokens for longer sessions
// Refresh tokens: 7-30 days, stored securely, rotated on use
```

### Disabling Signature Validation

**The Mistake:** Setting `ValidateIssuerSigningKey = false` to 'simplify development'.

**The Risk:** Anyone can create a valid-looking token with any claims they want. Complete bypass of authentication.

**The Fix:** Never disable validation in any environment:
```csharp
options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,  // ALWAYS TRUE!
    // ...
};
```

### Trusting the 'alg' Header Without Validation

**The Mistake:** Older JWT libraries allowed the token to specify its own signing algorithm, including 'none' for no signature.

**The Risk:** Attacker changes alg to 'none', removes the signature, and the token is accepted as valid.

**The Fix:** Modern libraries like Microsoft.IdentityModel.Tokens handle this correctly by default, but always specify your expected algorithm:
```csharp
var credentials = new SigningCredentials(
    _signingKey,
    SecurityAlgorithms.HmacSha256);  // Explicitly use HS256
```

### Not Validating ClockSkew

**The Mistake:** Leaving the default ClockSkew of 5 minutes.

**The Risk:** Tokens continue to work for 5 minutes after they technically expire. In a 15-minute token, that is 33% extra lifetime for stolen tokens.

**The Fix:**
```csharp
options.TokenValidationParameters = new TokenValidationParameters
{
    // ...
    ClockSkew = TimeSpan.Zero  // No tolerance
};
```

If you have distributed systems with clock drift, fix the clocks (use NTP) rather than loosening security.

### Storing Tokens in localStorage

**The Mistake:** Storing JWTs in browser localStorage because it is convenient.

**The Risk:** Any JavaScript on the page can read localStorage, including XSS-injected scripts. One XSS vulnerability exposes all your tokens.

**The Fix:** Store tokens in HttpOnly cookies (for web apps) or secure device storage (for mobile apps). For SPAs, consider the Backend-For-Frontend (BFF) pattern where the server holds the tokens.