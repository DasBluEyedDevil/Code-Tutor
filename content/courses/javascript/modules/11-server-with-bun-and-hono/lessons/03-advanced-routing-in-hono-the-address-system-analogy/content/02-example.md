---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Hono - Advanced Routing (2025)

// Simulated Hono with routing features
class HonoApp {
  constructor() {
    this.routes = [];
  }
  
  get(path, handler) {
    this.routes.push({ method: 'GET', path, handler });
  }
  
  simulateRequest(method, url) {
    console.log(`\n${method} ${url}`);
    
    for (let route of this.routes) {
      if (route.method !== method) continue;
      
      let params = this.matchRoute(route.path, url);
      if (params !== null) {
        let [path, queryString] = url.split('?');
        let queryObj = this.parseQuery(queryString);
        
        // Hono uses context object (c) with methods
        let c = {
          req: {
            param: (key) => params[key],
            query: (key) => queryObj[key],
            url: url
          },
          json: function(data, status = 200) {
            console.log(`Response [${status}]:`, JSON.stringify(data));
            return { status, body: data };
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
        let paramName = patternParts[i].slice(1);
        params[paramName] = urlParts[i];
      } else if (patternParts[i] !== urlParts[i]) {
        return null;
      }
    }
    return params;
  }
  
  parseQuery(queryString) {
    if (!queryString) return {};
    let query = {};
    queryString.split('&').forEach(pair => {
      let [key, value] = pair.split('=');
      query[key] = value;
    });
    return query;
  }
}

let app = new HonoApp();

// ROUTE PARAMETERS (Dynamic segments in URL)

// 1. Single parameter - Get user by ID
// In Hono: use c.req.param('id') to get the parameter
app.get('/api/users/:id', (c) => {
  let userId = c.req.param('id');
  
  // Simulate database lookup
  let user = {
    id: userId,
    name: 'Alice',
    email: 'alice@example.com'
  };
  
  return c.json(user);
});

// 2. Multiple parameters - Get specific product
app.get('/api/products/:category/:productId', (c) => {
  let category = c.req.param('category');
  let productId = c.req.param('productId');
  
  return c.json({
    category: category,
    productId: productId,
    name: 'Sample Product',
    price: 29.99
  });
});

// 3. Query parameters - Search and filter
// In Hono: use c.req.query('key') to get query params
app.get('/api/search', (c) => {
  let q = c.req.query('q');
  let category = c.req.query('category');
  let minPrice = c.req.query('minPrice');
  let maxPrice = c.req.query('maxPrice');
  
  return c.json({
    searchTerm: q || 'none',
    category: category || 'all',
    priceRange: {
      min: minPrice || 0,
      max: maxPrice || 'unlimited'
    },
    results: [
      { id: 1, name: 'Product A' },
      { id: 2, name: 'Product B' }
    ]
  });
});

// 4. Combining params and query
app.get('/api/categories/:category/products', (c) => {
  let category = c.req.param('category');
  let sort = c.req.query('sort');
  let limit = c.req.query('limit');
  
  return c.json({
    category: category,
    sortBy: sort || 'name',
    limit: limit || 10,
    products: ['Product 1', 'Product 2']
  });
});

// TEST THE ROUTES

// Parameter examples
app.simulateRequest('GET', '/api/users/42');
app.simulateRequest('GET', '/api/users/999');

app.simulateRequest('GET', '/api/products/electronics/laptop-123');
app.simulateRequest('GET', '/api/products/books/novel-456');

// Query parameter examples  
app.simulateRequest('GET', '/api/search?q=laptop&category=electronics&minPrice=500&maxPrice=2000');
app.simulateRequest('GET', '/api/search?q=headphones');

// Combined params and query
app.simulateRequest('GET', '/api/categories/electronics/products?sort=price&limit=20');
app.simulateRequest('GET', '/api/categories/books/products');
```
