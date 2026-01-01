---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Full-Stack architecture explained:

1. **Frontend (Client-Side)**:
   - Runs in user's browser
   - React components, HTML, CSS, JavaScript
   - Makes HTTP requests to backend
   - Port 3000 (development)

2. **Backend (Server-Side)**:
   - Runs on server (Node.js)
   - Hono routes handle requests
   - Connects to database
   - Port 4000 or 5000 (development)

3. **Database**:
   - PostgreSQL server
   - Stores all application data
   - Accessed via Prisma ORM
   - Port 5432 (default)

4. **Communication (HTTP/REST)**:
   ```javascript
   // Frontend makes request
   fetch('http://localhost:4000/api/users')
     .then(res => res.json())
     .then(users => setUsers(users));
   
   // Backend handles request (Hono)
   app.get('/api/users', async (c) => {
     const users = await prisma.user.findMany();
     return c.json(users);
   });
   ```

5. **CORS** (Cross-Origin Resource Sharing):
   - Frontend and backend on different ports = different origins
   - Need to enable CORS on backend:
   ```javascript
   import { Hono } from 'hono';
   import { cors } from 'hono/cors';
   const app = new Hono();
   app.use('*', cors());
   ```

6. **Environment Variables**:
   - Frontend: VITE_API_URL=http://localhost:4000
   - Backend: DATABASE_URL=postgresql://...
   - Never commit secrets to Git!