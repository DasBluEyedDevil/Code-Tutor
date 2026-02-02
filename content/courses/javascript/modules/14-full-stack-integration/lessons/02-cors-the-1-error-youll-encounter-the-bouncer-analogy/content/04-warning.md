---
type: "WARNING"
title: "Common Pitfalls"
---

Common CORS mistakes:

1. **CORS middleware placed AFTER routes**:
   ```javascript
   // WRONG!
   app.get('/api/users', (c) => { ... });
   app.use('*', cors());  // Too late! Routes already defined
   
   // CORRECT!
   app.use('*', cors());  // BEFORE routes!
   app.get('/api/users', (c) => { ... });
   ```

2. **With Hono, CORS is built-in** - no package to install:
   ```javascript
   // Hono - built-in!
   import { cors } from 'hono/cors';
   ```

3. **Using wrong origin in production**:
   ```javascript
   // WRONG in production!
   app.use('*', cors({ origin: 'http://localhost:3000' }));
   
   // CORRECT - use environment variable
   app.use('*', cors({ 
     origin: process.env.FRONTEND_URL  // https://myapp.com
   }));
   ```

4. **Allowing all origins in production** (security risk!):
   ```javascript
   // WRONG in production!
   app.use('*', cors());  // Any site can call your API!
   
   // CORRECT - be specific
   app.use('*', cors({ origin: 'https://myapp.com' }));
   ```

5. **Credentials without specific origin**:
   ```javascript
   // WRONG!
   app.use('*', cors({
     origin: '*',
     credentials: true  // Error! Can't use * with credentials
   }));
   
   // CORRECT!
   app.use('*', cors({
     origin: 'http://localhost:3000',  // Specific origin required
     credentials: true
   }));
   ```

6. **Frontend not sending credentials**:
   ```javascript
   // Backend allows credentials
   app.use('*', cors({ origin: '...', credentials: true }));
   
   // But frontend doesn't send them (cookies won't work!)
   fetch('/api/users');  // WRONG!
   
   // CORRECT!
   fetch('/api/users', { credentials: 'include' });
   ```

7. **Testing with curl/Postman and thinking CORS works**:
   - curl and Postman BYPASS CORS (they're not browsers)
   - Must test in actual browser!
   ```bash
   # This works even without CORS (not a browser!)
   curl http://localhost:4000/api/users
   
   # But browser will still block it
   ```

8. **Different protocols (http vs https)**:
   ```javascript
   // Frontend: https://myapp.com
   // Backend:  http://api.myapp.com
   // DIFFERENT protocols -> CORS error!
   
   // Both must be https in production
   ```