---
type: "THEORY"
title: "Token Storage: HttpOnly Cookies vs localStorage"
---

**Where you store tokens determines your security posture:**

**localStorage/sessionStorage:**
```javascript
localStorage.setItem('access_token', token);
```
- Accessible via JavaScript
- Vulnerable to XSS attacks
- Persists across browser sessions (localStorage)
- Easy to implement

**HttpOnly Cookies:**
```python
response.set_cookie(
    key="refresh_token",
    value=token,
    httponly=True,    # Not accessible via JavaScript
    secure=True,      # HTTPS only
    samesite="Lax"    # CSRF protection
)
```
- NOT accessible via JavaScript
- Protected from XSS attacks
- Automatically sent with requests
- Requires CSRF protection

**Recommended Strategy for Finance Tracker:**

| Token | Storage | Why |
|-------|---------|-----|
| Access Token | Memory (JS variable) | Short-lived, needed for API calls |
| Refresh Token | HttpOnly Cookie | Long-lived, must be protected from XSS |

**The key insight:** Access tokens are short-lived (15 min), so even if stolen via XSS, damage is limited. Refresh tokens are long-lived and more valuable, so they need HttpOnly cookie protection.