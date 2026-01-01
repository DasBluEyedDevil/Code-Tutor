---
type: "THEORY"
title: "Breaking Down the Syntax"
---

CORS configuration in Hono explained:

1. **No extra package needed!** Hono has built-in CORS:
   ```javascript
   import { Hono } from 'hono';
   import { cors } from 'hono/cors';  // Built-in!
   ```

2. **Basic CORS (allows all origins)**:
   ```javascript
   const app = new Hono();
   app.use('*', cors());  // Must be BEFORE routes!
   ```

3. **Specific origin only** (recommended for production):
   ```javascript
   app.use('*', cors({
     origin: 'https://myapp.com'
   }));
   ```

4. **Multiple origins**:
   ```javascript
   const allowedOrigins = [
     'http://localhost:3000',
     'https://myapp.com'
   ];
   
   app.use('*', cors({
     origin: (origin) => {
       if (allowedOrigins.includes(origin)) {
         return origin;
       }
       return null;  // Deny
     }
   }));
   ```

5. **With credentials** (cookies, auth headers):
   ```javascript
   app.use('*', cors({
     origin: 'http://localhost:3000',
     credentials: true  // Allow cookies
   }));
   
   // Frontend must also set:
   fetch('http://localhost:4000/api/users', {
     credentials: 'include'  // Send cookies
   });
   ```

6. **Environment-based CORS**:
   ```javascript
   app.use('*', cors({
     origin: process.env.NODE_ENV === 'production'
       ? 'https://myapp.com'
       : 'http://localhost:3000'
   }));
   ```

7. **Preflight requests** (OPTIONS):
   - Browser sends OPTIONS request first for PUT/DELETE/custom headers
   - Hono CORS middleware handles this automatically
   ```javascript
   // This happens automatically with app.use('*', cors())
   // No extra code needed!
   ```

8. **Manual CORS headers** (if not using middleware):
   ```javascript
   app.use('*', async (c, next) => {
     c.header('Access-Control-Allow-Origin', '*');
     c.header('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE');
     c.header('Access-Control-Allow-Headers', 'Content-Type');
     await next();
   });
   ```