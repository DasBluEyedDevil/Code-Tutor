---
type: "THEORY"
title: "What is OAuth2?"
---

**OAuth2** is an authorization framework that allows third-party applications to access user resources without exposing passwords.

**The Problem OAuth2 Solves:**
```
Without OAuth2:
User gives their Google password to Finance Tracker
→ Finance Tracker can do ANYTHING with user's Google account
→ If Finance Tracker is hacked, password is exposed
→ User must change password everywhere

With OAuth2:
User authorizes Finance Tracker through Google
→ Finance Tracker gets limited access token
→ Token can only read email, not send or delete
→ User can revoke access anytime
→ Password never shared
```

**Key OAuth2 Concepts:**

**Resource Owner** - The user who owns the data
**Client** - Your application (Finance Tracker)
**Authorization Server** - Issues tokens (Google, GitHub)
**Resource Server** - API that holds user data

**OAuth2 is NOT Authentication!**
OAuth2 is for **authorization** (what can you access?).
For authentication (who are you?), use **OpenID Connect** (built on OAuth2).