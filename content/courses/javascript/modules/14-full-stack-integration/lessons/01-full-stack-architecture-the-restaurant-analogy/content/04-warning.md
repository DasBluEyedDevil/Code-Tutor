---
type: "WARNING"
title: "Common Pitfalls"
---

Common full-stack mistakes:

1. **CORS errors**:
   ```
   Error: Access to fetch at 'http://localhost:4000/api/users' from origin 'http://localhost:3000' 
   has been blocked by CORS policy
   ```
   Solution: Enable CORS on backend (Hono):
   ```javascript
   import { Hono } from 'hono';
   import { cors } from 'hono/cors';
   const app = new Hono();
   app.use('*', cors());
   ```

2. **Wrong API URLs**:
   ```javascript
   // Wrong! (missing http://)
   fetch('localhost:4000/api/users')
   
   // Correct!
   fetch('http://localhost:4000/api/users')
   
   // Better! (use environment variable)
   fetch(`${import.meta.env.VITE_API_URL}/api/users`)
   ```

3. **Frontend and backend not running**:
   - Need TWO terminal windows:
     * Terminal 1: `cd frontend && npm run dev` (port 3000)
     * Terminal 2: `cd backend && npm run dev` (port 4000)

4. **Not handling async properly**:
   ```javascript
   // Wrong!
   let users = fetch('/api/users'); // Returns Promise!
   console.log(users); // Promise, not data
   
   // Correct!
   let response = await fetch('/api/users');
   let users = await response.json();
   console.log(users); // Actual data
   ```

5. **Hardcoded URLs**:
   - Don't hardcode: `http://localhost:4000`
   - Use env variables: `process.env.API_URL`
   - Different in dev vs production!

6. **Not validating data**:
   - Validate on frontend (UX)
   - ALSO validate on backend (security)
   - Never trust client data!

7. **Accessing request data in Hono**:
   ```javascript
   // Hono route
   app.post('/api/users/:id', async (c) => {
     c.req.param('id')           // From URL: /api/users/123
     c.req.query('name')         // From query: ?name=alice
     const body = await c.req.json()  // From POST body
     body.email                  // { email: '...' }
   });
   ```

8. **Storing JWT tokens in localStorage** (security risk!):
   ```javascript
   // RISKY! localStorage is vulnerable to XSS attacks
   localStorage.setItem('token', jwtToken);
   
   // BETTER: Use httpOnly cookies (set by backend)
   // Backend sets cookie (Hono):
   import { setCookie } from 'hono/cookie';
   setCookie(c, 'token', jwtToken, {
     httpOnly: true,  // Can't be accessed by JavaScript!
     secure: true,    // Only sent over HTTPS
     sameSite: 'Strict'
   });
   
   // Frontend automatically sends cookies with credentials:
   fetch('/api/protected', { credentials: 'include' });
   ```
   - httpOnly cookies can't be stolen via XSS
   - For SPAs, consider short-lived tokens + refresh tokens