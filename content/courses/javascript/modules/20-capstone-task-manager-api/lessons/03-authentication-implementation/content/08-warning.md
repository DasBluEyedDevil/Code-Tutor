---
type: "WARNING"
title: "Security Best Practices"
---

**Critical Security Points:**

1. **Same error message for login failures**
   - Don't reveal whether email exists: 'Invalid email or password'
   - Prevents email enumeration attacks

2. **Password hashing is mandatory**
   - Always use `Bun.password.hash()` with argon2id
   - Never store, log, or transmit plain passwords

3. **Token security**
   - Use strong secret (32+ characters) in production
   - Set reasonable expiration (7 days is common)
   - Never expose token in URLs

4. **Rate limiting (add in production)**
```typescript
import { rateLimiter } from 'hono-rate-limiter';

app.use('/api/auth/*', rateLimiter({
  windowMs: 15 * 60 * 1000, // 15 minutes
  limit: 10, // 10 requests per window
}));
```

5. **HTTPS only in production**
   - Tokens sent over HTTP can be intercepted