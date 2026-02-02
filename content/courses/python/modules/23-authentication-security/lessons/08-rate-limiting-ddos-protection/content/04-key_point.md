---
type: "KEY_POINT"
title: "Rate Limiting Best Practices"
---

**1. Choose Limits Wisely**
- Analyze legitimate usage patterns first
- Set limits that protect without impacting users
- Different limits for different endpoints:
  - Login: 5/minute (prevent brute force)
  - API: 100/minute (normal usage)
  - Export: 5/hour (expensive operation)

**2. Return Proper Headers**
```
X-RateLimit-Limit: 100
X-RateLimit-Remaining: 45
X-RateLimit-Reset: 1704067800
Retry-After: 30  (when blocked)
```

**3. Use Multiple Identifiers**
- IP address (basic)
- API key (authenticated)
- User ID (logged in)
- Combination for defense in depth

**4. Handle Edge Cases**
- Shared IPs (NAT, corporate networks)
- Mobile networks (IP changes)
- Legitimate high-volume users
- Webhook endpoints

**5. Monitor and Alert**
- Track rate limit hits
- Alert on unusual patterns
- Log blocked requests for analysis