---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Hono - Middleware (2025)

// Simulated Hono with middleware support
class HonoApp {
  constructor() {
    this.routes = [];
    this.middleware = [];
  }
  
  use(pattern, handler) {
    // Hono middleware signature: app.use('*', handler)
    // or app.use('/api/*', handler) for path-specific
    if (typeof pattern === 'function') {
      handler = pattern;
      pattern = '*';
    }
    this.middleware.push({ pattern, handler });
    console.log(`Middleware registered for: ${pattern}`);
  }
  
  get(path, handler) {
    this.routes.push({ method: 'GET', path, handler });
  }
  
  post(path, handler) {
    this.routes.push({ method: 'POST', path, handler });
  }
  
  async simulateRequest(method, path, body = null) {
    console.log(`\n--- ${method} ${path} ---`);
    
    // Create Hono-style context
    let c = {
      req: {
        method,
        path,
        header: (key) => key === 'Authorization' ? 'Bearer valid-token' : null,
        json: async () => body || {}
      },
      // Context can store data via c.set() and c.get()
      _store: {},
      set: function(key, value) { this._store[key] = value; },
      get: function(key) { return this._store[key]; },
      json: function(data, status = 200) {
        console.log(`Response [${status}]:`, JSON.stringify(data));
        return { status, body: data };
      },
      text: function(data, status = 200) {
        console.log(`Response [${status}]:`, data);
        return { status, body: data };
      }
    };
    
    // Run middleware chain with async/await
    let middlewareIndex = 0;
    
    let next = async () => {
      if (middlewareIndex < this.middleware.length) {
        let currentMiddleware = this.middleware[middlewareIndex++];
        await currentMiddleware.handler(c, next);
      } else {
        // Middleware done, find and run route handler
        let route = this.routes.find(r => r.method === method && r.path === path);
        if (route) {
          return route.handler(c);
        } else {
          return c.json({ error: 'Not found' }, 404);
        }
      }
    };
    
    await next();
  }
}

let app = new HonoApp();

// HONO MIDDLEWARE EXAMPLES
// Note: Hono middleware uses (c, next) pattern with async/await

// 1. Logging Middleware - Runs on EVERY request
app.use('*', async (c, next) => {
  console.log(`[LOG] ${c.req.method} ${c.req.path}`);
  const start = Date.now();
  
  await next();  // MUST await next() to continue!
  
  // Code AFTER next() runs after route handler
  const duration = Date.now() - start;
  console.log(`[TIMING] Request took ${duration}ms`);
});

// 2. Authentication Middleware
app.use('*', async (c, next) => {
  const token = c.req.header('Authorization');
  
  if (token === 'Bearer valid-token') {
    // Store user in context using c.set()
    c.set('user', { id: 1, name: 'Alice', role: 'admin' });
    console.log('[AUTH] User authenticated:', c.get('user').name);
    await next();
  } else {
    console.log('[AUTH] Invalid token!');
    return c.json({ error: 'Unauthorized' }, 401);
    // Note: No next() call - request stops here!
  }
});

// 3. Request Validator Middleware (for POST)
app.use('*', async (c, next) => {
  if (c.req.method === 'POST') {
    const body = await c.req.json();
    c.set('body', body);  // Store parsed body for route
    console.log('[PARSER] Parsed JSON body');
  }
  await next();
});

// ROUTES (run AFTER middleware)

app.get('/api/public', (c) => {
  const user = c.get('user');
  return c.json({ 
    message: 'Public endpoint',
    user: user ? user.name : 'anonymous'
  });
});

app.get('/api/protected', (c) => {
  // Get user from context (set by auth middleware)
  const user = c.get('user');
  return c.json({ 
    message: 'Protected data',
    user: user.name,
    role: user.role
  });
});

app.post('/api/data', (c) => {
  const body = c.get('body');  // Get parsed body from middleware
  const user = c.get('user');
  return c.json({ 
    message: 'Data received',
    data: body,
    processedBy: user.name
  });
});

// TEST MIDDLEWARE CHAIN

app.simulateRequest('GET', '/api/public');
app.simulateRequest('GET', '/api/protected');
app.simulateRequest('POST', '/api/data', { title: 'Test', value: 42 });
```
