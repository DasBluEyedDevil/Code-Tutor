---
type: "WARNING"
title: "OAuth2 Implementation Pitfalls"
---

**OAuth2 Security Mistakes to Avoid:**

❌ **NEVER expose client secrets in frontend code**

```javascript
// WRONG: Secret in browser JavaScript (visible to everyone!)
const response = await fetch('/oauth/token', {
  body: JSON.stringify({
    client_secret: 'abc123'  // EXPOSED!
  })
});

// RIGHT: Server-side only
# Backend handles client_secret
# Frontend never sees it
```

❌ **NEVER skip state parameter validation**

```python
# WRONG: Missing state check (CSRF vulnerability!)
@app.get("/callback")
async def oauth_callback(code: str):
    # Attacker can redirect user here with malicious code!
    token = exchange_code_for_token(code)

# RIGHT: Validate state parameter
@app.get("/callback")
async def oauth_callback(code: str, state: str):
    if state != session.get("oauth_state"):
        raise HTTPException(status_code=400, detail="Invalid state")
    token = exchange_code_for_token(code)
```

❌ **NEVER use implicit flow for confidential clients**

```
# WRONG: Token in URL fragment (logged, cached, referrer leaked)
https://app.com/callback#access_token=xyz

# RIGHT: Authorization Code Flow with PKCE
https://app.com/callback?code=abc
# Then exchange code for token server-side
```

❌ **NEVER request more scopes than needed**

```python
# WRONG: Requesting all permissions
scopes = ["read:all", "write:all", "admin"]  # Overkill!

# RIGHT: Minimum necessary scopes
scopes = ["read:user_profile"]  # Only what you need
```

❌ **NEVER store tokens insecurely**

```javascript
// WRONG: Accessible to XSS attacks
localStorage.setItem("access_token", token);

// BETTER: HttpOnly cookie (not accessible to JavaScript)
Set-Cookie: access_token=xyz; HttpOnly; Secure; SameSite=Strict
```

**OAuth2 Security Checklist:**
- [ ] Client secrets server-side only
- [ ] State parameter for CSRF protection
- [ ] PKCE for public clients (mobile/SPA)
- [ ] Minimum necessary scopes
- [ ] Tokens in HttpOnly cookies
- [ ] Short token lifetimes
- [ ] Refresh token rotation
