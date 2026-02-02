class HonoApp {
  constructor() { this.routes = []; this.middleware = []; }
  use(pattern, handler) {
    if (typeof pattern === 'function') { handler = pattern; pattern = '*'; }
    this.middleware.push({ pattern, handler });
  }
  post(path, handler) { this.routes.push({ method: 'POST', path, handler }); }
  async simulateRequest(method, path, body) {
    console.log(`\n${method} ${path}`);
    let c = {
      req: { method, path, json: async () => body || {} },
      _store: {},
      set: function(k, v) { this._store[k] = v; },
      get: function(k) { return this._store[k]; },
      json: function(d, s = 200) {
        console.log(`[${s}]`, JSON.stringify(d));
        return { status: s, body: d };
      }
    };
    let i = 0;
    let next = async () => {
      if (i < this.middleware.length) {
        await this.middleware[i++].handler(c, next);
      } else {
        let route = this.routes.find(r => r.method === method && r.path === path);
        if (route) return route.handler(c);
      }
    };
    await next();
  }
}

let app = new HonoApp();

// Middleware 1: Logging
app.use('*', async (c, next) => {
  console.log(`[LOG] ${c.req.method} ${c.req.path}`);
  await next();
});

// Middleware 2: Validation for POST requests
app.use('*', async (c, next) => {
  if (c.req.method === 'POST') {
    const body = await c.req.json();
    
    if (!body || !body.title) {
      console.log('[VALIDATION] Missing title!');
      return c.json({ 
        error: 'Validation failed',
        message: 'Title is required'
      }, 400);
      // No next() - stop here!
    }
    
    console.log('[VALIDATION] Title found:', body.title);
    c.set('body', body);  // Store for route handler
  }
  await next();
});

// Route: Create post
app.post('/api/posts', (c) => {
  const body = c.get('body');  // Get from middleware
  
  const post = {
    id: Math.floor(Math.random() * 1000),
    title: body.title,
    content: body.content || '',
    createdAt: new Date().toISOString()
  };
  
  return c.json({
    message: 'Post created successfully',
    post: post
  }, 201);
});

// Test with valid request
app.simulateRequest('POST', '/api/posts', { 
  title: 'My First Post', 
  content: 'Hello World!' 
});

// Test with invalid request (missing title)
app.simulateRequest('POST', '/api/posts', { 
  content: 'No title here!' 
});