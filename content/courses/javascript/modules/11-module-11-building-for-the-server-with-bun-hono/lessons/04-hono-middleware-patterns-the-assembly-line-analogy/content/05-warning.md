---
type: "WARNING"
title: "Common Pitfalls"
---

Common Hono middleware mistakes:

1. **Forgetting to await next()**:
   ```javascript
   app.use('*', async (c, next) => {
     console.log('Request received');
     next();  // Missing await! Middleware continues before route finishes
   });
   
   // Correct:
   app.use('*', async (c, next) => {
     console.log('Request received');
     await next();  // Properly waits for chain to complete
   });
   ```

2. **Returning after next()**:
   ```javascript
   app.use('*', async (c, next) => {
     await next();
     return c.json({ data: 'oops' });  // This overwrites route response!
   });
   
   // Correct - don't return after next() unless intentional:
   app.use('*', async (c, next) => {
     await next();
     // Just add logging, don't return a response
     console.log('Request completed');
   });
   ```

3. **Using Express patterns**:
   ```javascript
   // Wrong - Express style!
   app.use((req, res, next) => {
     req.user = { name: 'Alice' };
     next();
   });
   
   // Correct - Hono style!
   app.use('*', async (c, next) => {
     c.set('user', { name: 'Alice' });
     await next();
   });
   ```

4. **Forgetting the path pattern**:
   ```javascript
   // Hono requires a pattern (usually '*')
   app.use('*', async (c, next) => { ... });
   
   // For specific paths:
   app.use('/api/*', async (c, next) => { ... });
   ```

5. **Not handling errors in async middleware**:
   ```javascript
   app.use('*', async (c, next) => {
     try {
       await someAsyncOperation();
       await next();
     } catch (error) {
       return c.json({ error: error.message }, 500);
     }
   });
   ```

6. **Wrong middleware order**:
   - Authentication should come before route handlers
   - Logging should come first (to catch all requests)
   - Error handlers should come last