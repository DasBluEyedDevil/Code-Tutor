class HonoApp {
  constructor() { this.routes = []; }
  get(path, handler) { this.routes.push({ method: 'GET', path, handler }); }
  simulateRequest(method, url) {
    console.log(`\n${method} ${url}`);
    for (let route of this.routes) {
      if (route.method !== method) continue;
      let params = this.matchRoute(route.path, url);
      if (params !== null) {
        let [path, queryString] = url.split('?');
        let queryObj = this.parseQuery(queryString);
        let c = {
          req: {
            param: (key) => params[key],
            query: (key) => queryObj[key]
          },
          json: function(d, status = 200) {
            console.log(`[${status}]`, JSON.stringify(d, null, 2));
            return { status, body: d };
          }
        };
        route.handler(c);
        return;
      }
    }
    console.log('404 Not Found');
  }
  matchRoute(pattern, url) {
    let [urlPath] = url.split('?');
    let patternParts = pattern.split('/');
    let urlParts = urlPath.split('/');
    if (patternParts.length !== urlParts.length) return null;
    let params = {};
    for (let i = 0; i < patternParts.length; i++) {
      if (patternParts[i].startsWith(':')) {
        params[patternParts[i].slice(1)] = urlParts[i];
      } else if (patternParts[i] !== urlParts[i]) return null;
    }
    return params;
  }
  parseQuery(qs) {
    if (!qs) return {};
    let q = {};
    qs.split('&').forEach(p => { let [k,v] = p.split('='); q[k] = v; });
    return q;
  }
}

let app = new HonoApp();

// Route 1: Get specific post using c.req.param()
app.get('/api/posts/:postId', (c) => {
  const postId = c.req.param('postId');
  
  return c.json({
    postId: postId,
    title: `Post ${postId} Title`,
    content: 'This is the post content...',
    author: 'Alice'
  });
});

// Route 2: Get posts by author
app.get('/api/authors/:authorId/posts', (c) => {
  const authorId = c.req.param('authorId');
  
  return c.json({
    authorId: authorId,
    posts: [
      { id: 1, title: 'First Post', content: 'Content 1' },
      { id: 2, title: 'Second Post', content: 'Content 2' }
    ]
  });
});

// Route 3: Search posts with c.req.query()
app.get('/api/posts', (c) => {
  const search = c.req.query('search');
  const category = c.req.query('category');
  const limit = c.req.query('limit');
  
  return c.json({
    search: search || 'all',
    category: category || 'all',
    limit: limit || '10',
    results: [
      { id: 1, title: 'Hono Tutorial', category: 'tech' },
      { id: 2, title: 'Bun Guide', category: 'tech' }
    ]
  });
});

// Test all routes
app.simulateRequest('GET', '/api/posts/123');
app.simulateRequest('GET', '/api/authors/42/posts');
app.simulateRequest('GET', '/api/posts?search=hono&category=tech&limit=5');
app.simulateRequest('GET', '/api/posts');