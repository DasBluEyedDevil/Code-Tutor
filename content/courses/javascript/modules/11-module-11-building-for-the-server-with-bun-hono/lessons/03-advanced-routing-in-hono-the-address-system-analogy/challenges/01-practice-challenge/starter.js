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
            console.log(`[${status}]`, JSON.stringify(d));
            return { status, body: d };
          }
        };
        route.handler(c);
        return;
      }
    }
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

// TODO: Add your routes here using Hono patterns
// Remember: c.req.param('key') and c.req.query('key')

// Test routes
app.simulateRequest('GET', '/api/posts/123');
app.simulateRequest('GET', '/api/authors/42/posts');
app.simulateRequest('GET', '/api/posts?search=hono&category=tech&limit=5');