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

// TODO: Add logging middleware
// app.use('*', async (c, next) => { ... await next(); });

// TODO: Add validation middleware

// TODO: Add POST route

// Test
app.simulateRequest('POST', '/api/posts', { title: 'My Post', content: 'Hello' });
app.simulateRequest('POST', '/api/posts', { content: 'No title!' });