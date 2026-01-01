---
type: "EXAMPLE"
title: "Rate Limiting Middleware"
---

Rate limiting protects your API from abuse by limiting how many requests a client can make in a given time period. This middleware pattern tracks request counts per client (using IP address or API key) and returns appropriate error responses when limits are exceeded. Rate limiting is essential for preventing DDoS attacks and ensuring fair resource usage.

```javascript
// Hono Rate Limiting Middleware (2025)
// Protect your API from abuse

import { Hono } from 'hono';

const app = new Hono();

// IN-MEMORY RATE LIMITER (for demonstration)
// In production, use Redis or a distributed cache!

class RateLimiter {
  constructor(options = {}) {
    this.windowMs = options.windowMs || 60000;  // 1 minute default
    this.maxRequests = options.max || 100;       // 100 requests default
    this.message = options.message || 'Too many requests, please try again later';
    this.store = new Map();  // IP -> { count, resetTime }
    
    // Clean up expired entries periodically
    setInterval(() => this.cleanup(), this.windowMs);
  }
  
  cleanup() {
    const now = Date.now();
    for (const [key, value] of this.store) {
      if (now > value.resetTime) {
        this.store.delete(key);
      }
    }
  }
  
  // Get client identifier (IP address)
  getClientId(c) {
    // Try various headers for real IP behind proxies
    return c.req.header('X-Forwarded-For')?.split(',')[0]?.trim() ||
           c.req.header('X-Real-IP') ||
           c.req.header('CF-Connecting-IP') ||  // Cloudflare
           'unknown';
  }
  
  // Create middleware function
  middleware() {
    return async (c, next) => {
      const clientId = this.getClientId(c);
      const now = Date.now();
      
      // Get or create rate limit entry
      let entry = this.store.get(clientId);
      
      if (!entry || now > entry.resetTime) {
        entry = {
          count: 0,
          resetTime: now + this.windowMs
        };
        this.store.set(clientId, entry);
      }
      
      // Increment request count
      entry.count++;
      
      // Calculate remaining requests and reset time
      const remaining = Math.max(0, this.maxRequests - entry.count);
      const resetSeconds = Math.ceil((entry.resetTime - now) / 1000);
      
      // Set rate limit headers
      c.header('X-RateLimit-Limit', String(this.maxRequests));
      c.header('X-RateLimit-Remaining', String(remaining));
      c.header('X-RateLimit-Reset', String(Math.ceil(entry.resetTime / 1000)));
      
      // Check if limit exceeded
      if (entry.count > this.maxRequests) {
        c.header('Retry-After', String(resetSeconds));
        
        return c.json({
          error: 'Too Many Requests',
          message: this.message,
          retryAfter: resetSeconds
        }, 429);
      }
      
      await next();
    };
  }
}

// CREATE RATE LIMITERS WITH DIFFERENT CONFIGS

// General API limit: 100 requests per minute
const generalLimiter = new RateLimiter({
  windowMs: 60 * 1000,  // 1 minute
  max: 100,
  message: 'Rate limit exceeded. Please wait before making more requests.'
});

// Strict limit for auth endpoints: 5 per minute
const authLimiter = new RateLimiter({
  windowMs: 60 * 1000,
  max: 5,
  message: 'Too many login attempts. Please try again in a minute.'
});

// Very strict for sensitive operations: 3 per hour
const sensitiveLimit = new RateLimiter({
  windowMs: 60 * 60 * 1000,  // 1 hour
  max: 3,
  message: 'This action is rate limited. Please try again later.'
});

// API key based rate limiter
const apiKeyLimiter = new RateLimiter({
  windowMs: 60 * 1000,
  max: 1000  // Higher limit for API key users
});

// Override getClientId to use API key
apiKeyLimiter.getClientId = (c) => {
  return c.req.header('X-API-Key') || 'anonymous';
};

// APPLY RATE LIMITERS

// General rate limit for all routes
app.use('*', generalLimiter.middleware());

// Stricter limit for auth routes
app.use('/api/auth/*', authLimiter.middleware());

// Very strict for password reset
app.use('/api/auth/reset-password', sensitiveLimit.middleware());

// ROUTES

app.get('/api/data', (c) => {
  return c.json({
    message: 'API data',
    timestamp: new Date().toISOString()
  });
});

app.post('/api/auth/login', async (c) => {
  const body = await c.req.json();
  return c.json({
    message: 'Login processed',
    email: body.email
  });
});

app.post('/api/auth/reset-password', async (c) => {
  const body = await c.req.json();
  return c.json({
    message: 'Password reset email sent',
    email: body.email
  });
});

// SLIDING WINDOW RATE LIMITER (more accurate)
class SlidingWindowLimiter {
  constructor(options = {}) {
    this.windowMs = options.windowMs || 60000;
    this.maxRequests = options.max || 100;
    this.store = new Map();  // IP -> [timestamps]
  }
  
  middleware() {
    return async (c, next) => {
      const clientId = c.req.header('X-Forwarded-For') || 'unknown';
      const now = Date.now();
      const windowStart = now - this.windowMs;
      
      // Get request timestamps for this client
      let timestamps = this.store.get(clientId) || [];
      
      // Remove expired timestamps
      timestamps = timestamps.filter(ts => ts > windowStart);
      
      // Check limit
      if (timestamps.length >= this.maxRequests) {
        const oldestTimestamp = timestamps[0];
        const retryAfter = Math.ceil((oldestTimestamp + this.windowMs - now) / 1000);
        
        return c.json({
          error: 'Rate limit exceeded',
          retryAfter: retryAfter
        }, 429);
      }
      
      // Add current request
      timestamps.push(now);
      this.store.set(clientId, timestamps);
      
      await next();
    };
  }
}

export default app;

// PRODUCTION RECOMMENDATIONS:
// 1. Use Redis for distributed rate limiting
// 2. Consider using 'hono-rate-limiter' package
// 3. Set appropriate limits based on endpoint sensitivity
// 4. Always include Retry-After header in 429 responses
// 5. Log rate limit violations for security monitoring
```
