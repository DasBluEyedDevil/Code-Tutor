---
type: "WARNING"
title: "NEVER Store Passwords in Plain Text"
---

**This is an unforgivable security sin:**

```python
# NEVER DO THIS - Career-ending mistake
user = {
    'email': 'alice@example.com',
    'password': 'MySecret123'  # DISASTER!
}
db.insert(user)
```

When (not if) your database is breached:
- All user passwords are immediately exposed
- Users often reuse passwords across sites
- You've enabled attackers to access users' bank accounts, emails, etc.
- Legal liability, regulatory fines (GDPR, etc.)
- Complete destruction of user trust