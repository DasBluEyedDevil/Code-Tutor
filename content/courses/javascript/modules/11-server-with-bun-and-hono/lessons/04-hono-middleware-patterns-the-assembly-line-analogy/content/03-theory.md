---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding Hono middleware:

1. **Middleware Signature**: `async (c, next) => { ... }`
   - `c`: Context object (has req, response helpers, storage)
   - `next`: Async function to call the next middleware
   - MUST await `next()` to continue the chain!

2. **Registering Middleware**: `app.use('*', middlewareFunction)`
   - `'*'` means run on ALL routes
   - `'/api/*'` runs only on /api routes
   - Order matters - middleware runs in order you add it

3. **Middleware Flow**:
   ```
   Request -> Middleware 1 -> Middleware 2 -> Route Handler
                   |                |              |
                   v                v              v
             (before next)    (before next)   (handler)
                   |                |              |
                   v                v              v
             (after next)     (after next)    Response
   ```

4. **Context Storage** (c.set/c.get):
   ```javascript
   // In middleware
   app.use('*', async (c, next) => {
     c.set('user', { id: 1, name: 'Alice' });
     await next();
   });
   
   // In route handler
   app.get('/profile', (c) => {
     const user = c.get('user');  // Access stored data!
     return c.json(user);
   });
   ```

5. **Before and After next()**:
   ```javascript
   app.use('*', async (c, next) => {
     console.log('Before route handler');  // Runs first
     const start = Date.now();
     
     await next();  // Route handler runs here
     
     const ms = Date.now() - start;  // Runs after
     console.log(`Request took ${ms}ms`);
   });
   ```

6. **Stopping the Chain**:
   ```javascript
   app.use('*', async (c, next) => {
     if (!isAuthorized(c)) {
       return c.json({ error: 'Unauthorized' }, 401);
       // No next() - request stops here!
     }
     await next();
   });
   ```

7. **Built-in Hono Middleware**:
   ```javascript
   import { cors } from 'hono/cors';
   import { logger } from 'hono/logger';
   import { prettyJSON } from 'hono/pretty-json';
   
   app.use('*', logger());
   app.use('*', cors());
   app.use('*', prettyJSON());
   ```