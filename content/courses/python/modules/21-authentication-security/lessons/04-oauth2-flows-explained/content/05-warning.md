---
type: "WARNING"
title: "OAuth2 Security Requirements"
---

**Critical security measures for OAuth2 implementation:**

**1. Always Validate State Parameter:**
```python
# CSRF attack without state validation:
# Attacker crafts link: /callback?code=ATTACKER_CODE
# Victim clicks, their account gets linked to attacker's!

if callback_state != session_state:
    raise SecurityError("Invalid state - CSRF detected")
```

**2. Use PKCE for Public Clients:**
Mobile apps and SPAs cannot keep secrets. PKCE adds security.

**3. Validate Redirect URI Exactly:**
```python
# BAD: Partial match allows attacker.com/legit.com
if redirect_uri.contains("legit.com"):
    
# GOOD: Exact match
if redirect_uri == registered_redirect_uri:
```

**4. Never Expose Client Secret:**
- Keep in environment variables
- Never commit to version control
- Never send to frontend

**5. Validate ID Token:**
- Check signature
- Verify issuer (iss)
- Verify audience (aud) matches your client_id
- Check expiration (exp)