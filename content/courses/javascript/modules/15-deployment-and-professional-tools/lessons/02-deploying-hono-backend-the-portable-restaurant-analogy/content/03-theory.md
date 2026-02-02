---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Hono deployment configuration by platform:

1. **Bun on Render** (recommended for beginners):
   ```typescript
   // src/index.ts
   import { Hono } from 'hono'
   import { cors } from 'hono/cors'
   
   const app = new Hono()
   
   app.use('*', cors())
   app.get('/health', (c) => c.json({ status: 'ok' }))
   
   export default app
   
   // package.json
   {
     "scripts": {
       "start": "bun run src/index.ts",
       "dev": "bun --watch src/index.ts"
     }
   }
   ```

2. **Cloudflare Workers** (edge deployment):
   ```typescript
   // src/index.ts - same Hono code!
   import { Hono } from 'hono'
   const app = new Hono()
   app.get('/', (c) => c.text('Hello from Edge!'))
   export default app
   
   // wrangler.toml
   name = "my-api"
   main = "src/index.ts"
   compatibility_date = "2024-01-01"
   
   // Deploy: wrangler deploy
   ```

3. **Deno Deploy** (serverless):
   ```typescript
   // src/index.ts
   import { Hono } from 'npm:hono'
   const app = new Hono()
   app.get('/', (c) => c.text('Hello from Deno!'))
   Deno.serve(app.fetch)
   ```

4. **Environment Variables** (Bun reads .env automatically!):
   ```typescript
   // Bun auto-loads .env - no dotenv needed!
   const dbUrl = process.env.DATABASE_URL
   const secret = Bun.env.JWT_SECRET  // or process.env
   ```

5. **CORS with Hono** (built-in middleware):
   ```typescript
   import { cors } from 'hono/cors'
   
   app.use('*', cors({
     origin: ['https://my-app.vercel.app', 'http://localhost:5173'],
     credentials: true
   }))
   ```

6. **Health Check** (Hono style):
   ```typescript
   app.get('/health', (c) => {
     return c.json({
       status: 'ok',
       timestamp: new Date().toISOString(),
       runtime: typeof Bun !== 'undefined' ? 'bun' : 'other'
     })
   })
   ```

7. **Error Handling** (Hono's onError):
   ```typescript
   app.onError((err, c) => {
     console.error(err)
     if (process.env.NODE_ENV === 'production') {
       return c.json({ error: 'Internal error' }, 500)
     }
     return c.json({ error: err.message }, 500)
   })
   ```

8. **Database with Drizzle** (works everywhere):
   ```typescript
   import { drizzle } from 'drizzle-orm/postgres-js'
   import postgres from 'postgres'
   
   const client = postgres(process.env.DATABASE_URL!)
   const db = drizzle(client)
   ```