---
type: "THEORY"
title: "Middleware Patterns Summary"
---

Here is a comprehensive summary of essential Hono middleware patterns for building production-ready APIs:

1. **Error Handling Middleware**:
   - Use `app.onError((err, c) => {...})` for centralized error handling
   - Use `app.notFound((c) => {...})` for 404 responses
   - Create custom error classes with statusCode property
   - Never leak stack traces in production
   - Log all errors for debugging and security monitoring

2. **JWT Authentication Middleware**:
   - Extract token from Authorization header (Bearer scheme)
   - Verify token signature and check expiration
   - Store decoded payload in context with `c.set('user', payload)`
   - Chain with role-based access control middleware
   - Use Hono's built-in `jwt()` middleware for simpler setup (requires `alg` option, e.g. `jwt({ secret, alg: 'HS256' })`)

3. **CORS Middleware**:
   - Use `cors()` from 'hono/cors' for easy setup
   - Whitelist specific origins in production (never use '*' with credentials)
   - Configure allowed methods, headers, and max age
   - Handle preflight OPTIONS requests automatically
   - Use different CORS configs for public vs private endpoints

4. **Rate Limiting Middleware**:
   - Track requests per client (IP or API key) in a time window
   - Return 429 Too Many Requests with Retry-After header
   - Set X-RateLimit-* headers for client visibility
   - Use different limits for different endpoints (stricter for auth)
   - Use Redis for distributed rate limiting in production

5. **Logging Middleware**:
   - Log requests at start and responses at end
   - Measure duration between `await next()` calls
   - Include request ID for distributed tracing
   - Redact sensitive data from logs
   - Use structured JSON logs in production
   - Detect and alert on slow requests

6. **Middleware Ordering Best Practices**:
   ```
   1. Logger (first - catches everything)
   2. Error handler (app.onError)
   3. CORS (before auth to handle preflight)
   4. Rate limiter (before expensive operations)
   5. Authentication (verify identity)
   6. Authorization (check permissions)
   7. Validation (check input)
   8. Route handlers (business logic)
   ```

7. **Factory Pattern for Reusable Middleware**:
   ```javascript
   const requireRole = (...roles) => async (c, next) => {
     const user = c.get('user');
     if (!roles.includes(user.role)) {
       return c.json({ error: 'Forbidden' }, 403);
     }
     await next();
   };
   
   app.use('/admin/*', requireRole('admin'));
   app.use('/api/*', requireRole('user', 'admin'));
   ```