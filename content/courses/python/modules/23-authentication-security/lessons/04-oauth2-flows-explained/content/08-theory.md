---
type: "THEORY"
title: "Implementing Social Login in Finance Tracker"
---

**Adding "Login with Google/GitHub" to your application:**

**Why Social Login?**
- Better UX (no new password to remember)
- Stronger security (leverage provider's 2FA)
- Verified email addresses
- Less password reset support

**Implementation Strategy:**

**1. User clicks "Login with Google"**
→ Redirect to Google's authorization endpoint

**2. User authorizes on Google**
→ Google redirects back with authorization code

**3. Exchange code for tokens**
→ Receive access_token and id_token

**4. Validate ID token and extract user info**
→ Get email, name, picture from ID token

**5. Find or create user in your database**
→ Match by email or provider user ID

**6. Create your own session/JWT**
→ User is now logged into Finance Tracker

**Database Schema for Social Login:**
```sql
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    email VARCHAR(255) UNIQUE,
    name VARCHAR(255),
    picture_url VARCHAR(500),
    created_at TIMESTAMP DEFAULT NOW()
);

CREATE TABLE social_accounts (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES users(id),
    provider VARCHAR(50),  -- 'google', 'github'
    provider_user_id VARCHAR(255),
    UNIQUE(provider, provider_user_id)
);
```