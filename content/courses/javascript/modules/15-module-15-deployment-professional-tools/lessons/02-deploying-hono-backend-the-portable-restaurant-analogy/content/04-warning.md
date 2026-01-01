---
type: "WARNING"
title: "Common Pitfalls"
---

Common Hono deployment mistakes:

1. **Forgetting to export default** (Bun/Workers need it!):
   ```typescript
   // WRONG! Bun won't serve this
   const app = new Hono()
   app.get('/', (c) => c.text('Hello'))
   // Missing export!
   
   // CORRECT!
   const app = new Hono()
   app.get('/', (c) => c.text('Hello'))
   export default app  // Required for Bun!
   ```

2. **Wrong import for Deno**:
   ```typescript
   // WRONG for Deno!
   import { Hono } from 'hono'
   
   // CORRECT for Deno!
   import { Hono } from 'npm:hono'
   ```

3. **Committed .env file** (security risk!):
   ```bash
   # .gitignore MUST include:
   .env
   .env.local
   node_modules/
   
   # Bun lockfile is fine to commit:
   # bun.lockb
   ```

4. **Missing start script for Render**:
   ```json
   // package.json for Bun on Render
   {
     "scripts": {
       "start": "bun run src/index.ts",
       "dev": "bun --watch src/index.ts"
     }
   }
   ```

5. **CORS not configured**:
   ```typescript
   // WRONG! No CORS = frontend blocked
   const app = new Hono()
   
   // CORRECT!
   import { cors } from 'hono/cors'
   const app = new Hono()
   app.use('*', cors({
     origin: ['https://my-app.vercel.app', 'http://localhost:5173']
   }))
   ```

6. **Missing health check**:
   ```typescript
   // Always add this for monitoring!
   app.get('/health', (c) => c.json({ status: 'ok' }))
   ```

7. **Platform-specific code without checks**:
   ```typescript
   // WRONG! Bun.env doesn't exist in Cloudflare Workers
   const secret = Bun.env.SECRET
   
   // CORRECT! Works everywhere
   const secret = process.env.SECRET
   ```

8. **Not handling errors**:
   ```typescript
   // Add global error handler
   app.onError((err, c) => {
     console.error(err)
     return c.json({ error: 'Internal error' }, 500)
   })
   
   // Add 404 handler
   app.notFound((c) => c.json({ error: 'Not found' }, 404))
   ```