---
type: "EXAMPLE"
title: "Wildcards and Catch-All Routes"
---

Wildcards in Hono allow you to match any path segment or capture the remainder of a URL. The asterisk (*) acts as a catch-all pattern that matches everything after a certain point. This is incredibly useful for handling 404 pages, serving static files, or creating proxy routes that forward requests to other services.

```javascript
// Hono Wildcards and Catch-All Routes (2025)

import { Hono } from 'hono';

const app = new Hono();

// WILDCARD PATTERNS

// 1. Catch-all at the end of a path
// Matches: /files/docs/report.pdf, /files/images/photo.jpg, etc.
app.get('/files/*', (c) => {
  // c.req.path gives you the full matched path
  const fullPath = c.req.path;
  
  // Extract the wildcard portion (everything after /files/)
  const filePath = fullPath.replace('/files/', '');
  
  return c.json({
    message: 'File requested',
    path: filePath,
    fullPath: fullPath
  });
});

// 2. API versioning with wildcards
// Forward all v1 API requests
app.all('/api/v1/*', async (c) => {
  const path = c.req.path;
  const method = c.req.method;
  
  return c.json({
    version: 'v1',
    method: method,
    path: path,
    message: 'V1 API - Consider upgrading to V2!'
  });
});

// 3. Proxy pattern - forward requests to another service
app.all('/proxy/*', async (c) => {
  const targetPath = c.req.path.replace('/proxy', '');
  const targetUrl = `https://api.example.com${targetPath}`;
  
  // In real code, you'd forward the request:
  // const response = await fetch(targetUrl, {
  //   method: c.req.method,
  //   headers: c.req.header(),
  //   body: c.req.method !== 'GET' ? await c.req.text() : undefined
  // });
  // return response;
  
  return c.json({
    message: 'Would proxy to:',
    targetUrl: targetUrl
  });
});

// 4. Static file serving pattern
app.get('/static/*', async (c) => {
  const filePath = c.req.path.replace('/static/', '');
  
  // In real Bun, you'd serve the file:
  // const file = Bun.file(`./public/${filePath}`);
  // return new Response(file);
  
  return c.json({
    serving: filePath,
    from: './public/' + filePath
  });
});

// 5. Namespace isolation - different apps under one roof
const blogApp = new Hono();
blogApp.get('/', (c) => c.text('Blog Home'));
blogApp.get('/posts', (c) => c.json({ posts: [] }));
blogApp.get('/posts/:id', (c) => c.json({ id: c.req.param('id') }));

const shopApp = new Hono();
shopApp.get('/', (c) => c.text('Shop Home'));
shopApp.get('/products', (c) => c.json({ products: [] }));

// Mount sub-applications
app.route('/blog', blogApp);
app.route('/shop', shopApp);

// 6. 404 Handler - MUST be last!
// This catches any route not matched above
app.all('*', (c) => {
  return c.json({
    error: 'Not Found',
    message: `Route ${c.req.method} ${c.req.path} does not exist`,
    availableRoutes: [
      'GET /files/*',
      'ALL /api/v1/*',
      'ALL /proxy/*',
      'GET /static/*',
      '/blog/*',
      '/shop/*'
    ]
  }, 404);
});

export default app;

// IMPORTANT: Route order matters!
// 1. Specific routes first: /api/users/active
// 2. Parameterized routes: /api/users/:id
// 3. Wildcard routes: /api/*
// 4. Catch-all 404: * (always last!)
```
