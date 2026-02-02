---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding Hono routing:

1. **Route Parameters** (`:paramName`):
   ```javascript
   app.get('/users/:id', (c) => {
     const id = c.req.param('id');  // Extract from URL
     return c.json({ id });
   });
   ```
   - Colon `:` marks a parameter
   - Use `c.req.param('name')` to get the value
   - `/users/42` -> `c.req.param('id')` returns `'42'`

2. **Multiple Parameters**:
   ```javascript
   app.get('/posts/:year/:month/:day', (c) => {
     const year = c.req.param('year');
     const month = c.req.param('month');
     const day = c.req.param('day');
     return c.json({ year, month, day });
   });
   ```

3. **Query Parameters** (after `?`):
   ```javascript
   // URL: /search?q=hono&page=2
   app.get('/search', (c) => {
     const searchTerm = c.req.query('q');     // 'hono'
     const page = c.req.query('page');        // '2'
     return c.json({ searchTerm, page });
   });
   ```
   - Not part of the route pattern
   - Optional by default
   - Use `c.req.query('key')` to access

4. **Parameter vs Query - When to Use**:
   - **Route parameters** (`:id`):
     * Required parts of the URL
     * Identifying resources
     * `/users/:userId/posts/:postId`
   
   - **Query parameters** (`?key=value`):
     * Optional filters
     * Search terms
     * Pagination, sorting
     * `/products?category=books&sort=price&page=2`

5. **Accessing Data in Hono**:
   - `c.req.param('key')` -> Single URL parameter
   - `c.req.query('key')` -> Single query parameter
   - `await c.req.json()` -> POST/PUT body (async!)
   - `c.req.header('key')` -> Request headers

6. **Type Conversion**:
   ```javascript
   app.get('/api/users/:id', (c) => {
     // c.req.param() always returns STRING
     const userId = parseInt(c.req.param('id'));
     const page = parseInt(c.req.query('page')) || 1;
     return c.json({ userId, page });
   });
   ```
   All parameters come as strings - convert as needed!