---
type: "EXAMPLE"
title: "Code Example"
---

Hono works identically across platforms. Here's a complete API that deploys anywhere.

```javascript
// Deploying Hono API - Multi-Platform

console.log('=== Hono Multi-Platform Deployment ===\n');

// CONCEPT: One codebase, many platforms
const PORT = process.env.PORT || 3000;
const PLATFORM = process.env.PLATFORM || 'bun';

console.log('Target Platform:', PLATFORM);
console.log('Port:', PORT);

// Simulating Hono app structure
const app = {
  routes: [
    { method: 'GET', path: '/', handler: 'home' },
    { method: 'GET', path: '/api/users', handler: 'getUsers' },
    { method: 'POST', path: '/api/users', handler: 'createUser' },
    { method: 'GET', path: '/health', handler: 'healthCheck' }
  ],
  
  // Hono's elegant response helpers
  responseExamples: {
    text: "c.text('Hello!')",
    json: "c.json({ users: [] })",
    html: "c.html('<h1>Hello</h1>')"
  }
};

console.log('\nHono App Routes:');
app.routes.forEach(r => console.log(`  ${r.method} ${r.path}`));

// PLATFORM-SPECIFIC DEPLOYMENT
console.log('\n\n=== Deployment by Platform ===\n');

const platforms = {
  'bun-render': {
    name: 'Bun on Render',
    buildCmd: 'bun install',
    startCmd: 'bun run src/index.ts',
    envFile: '.env',
    pros: ['Free tier', 'Fast builds', 'Easy setup'],
    entryPoint: `// src/index.ts
import { Hono } from 'hono'

const app = new Hono()

app.get('/', (c) => c.text('Hello from Hono!'))
app.get('/health', (c) => c.json({ status: 'ok' }))

export default app  // Bun serves this automatically`
  },
  
  'cloudflare-workers': {
    name: 'Cloudflare Workers',
    buildCmd: 'wrangler deploy',
    startCmd: 'N/A (serverless)',
    envFile: 'wrangler.toml',
    pros: ['Edge locations worldwide', 'Instant cold starts', 'Free tier'],
    entryPoint: `// src/index.ts
import { Hono } from 'hono'

const app = new Hono()

app.get('/', (c) => c.text('Hello from the Edge!'))
app.get('/health', (c) => c.json({ status: 'ok' }))

export default app  // Workers picks this up`
  },
  
  'deno-deploy': {
    name: 'Deno Deploy',
    buildCmd: 'N/A (deploys from GitHub)',
    startCmd: 'deno run --allow-net src/index.ts',
    envFile: 'Dashboard',
    pros: ['Git-based deploys', 'TypeScript native', 'Global edge'],
    entryPoint: `// src/index.ts
import { Hono } from 'npm:hono'
import { serve } from 'https://deno.land/std/http/server.ts'

const app = new Hono()

app.get('/', (c) => c.text('Hello from Deno!'))
app.get('/health', (c) => c.json({ status: 'ok' }))

serve(app.fetch)  // Deno's serve function`
  }
};

Object.entries(platforms).forEach(([key, platform]) => {
  console.log(`--- ${platform.name} ---`);
  console.log(`Build: ${platform.buildCmd}`);
  console.log(`Start: ${platform.startCmd}`);
  console.log(`Pros: ${platform.pros.join(', ')}`);
  console.log('');
});

// HONO CODE PORTABILITY
console.log('\n=== The Magic: Same Code Everywhere ===\n');

const honoApp = `// This EXACT code works on Bun, Deno, CF Workers, and Node!
import { Hono } from 'hono'
import { cors } from 'hono/cors'
import { logger } from 'hono/logger'

const app = new Hono()

// Middleware (same everywhere)
app.use('*', cors())
app.use('*', logger())

// Routes (same everywhere)
app.get('/', (c) => c.text('Hello Hono!'))

app.get('/api/users', (c) => {
  return c.json([{ id: 1, name: 'Alice' }])
})

app.post('/api/users', async (c) => {
  const body = await c.req.json()
  return c.json({ id: 2, ...body }, 201)
})

app.get('/health', (c) => {
  return c.json({ 
    status: 'ok', 
    timestamp: new Date().toISOString() 
  })
})

export default app`;

console.log(honoApp);

// DEPLOYMENT STEPS FOR BUN ON RENDER
console.log('\n\n=== Deploy to Render (Bun) ===\n');

const renderSteps = [
  { step: 1, title: 'Prepare', tasks: ['bun init', 'bun add hono', 'Create src/index.ts'] },
  { step: 2, title: 'package.json', tasks: ['Add: "start": "bun run src/index.ts"'] },
  { step: 3, title: 'Render Setup', tasks: ['New Web Service', 'Connect GitHub', 'Runtime: Node (Bun works here!)'] },
  { step: 4, title: 'Configure', tasks: ['Build: bun install', 'Start: bun run src/index.ts'] },
  { step: 5, title: 'Deploy', tasks: ['Add env vars', 'Click Deploy', 'Get URL!'] }
];

renderSteps.forEach(({ step, title, tasks }) => {
  console.log(`Step ${step}: ${title}`);
  tasks.forEach(t => console.log(`  - ${t}`));
});

console.log('\n=== Why Hono + Bun? ===');
console.log('- 3x faster than Express');
console.log('- TypeScript native');
console.log('- Tiny bundle size');
console.log('- Deploy anywhere with same code');
```
