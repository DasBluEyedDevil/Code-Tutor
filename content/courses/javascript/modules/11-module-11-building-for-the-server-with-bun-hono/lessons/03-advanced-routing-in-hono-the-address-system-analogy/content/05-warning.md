---
type: "WARNING"
title: "Common Pitfalls"
---

Common Hono routing mistakes:

1. **Using Express object syntax**:
   ```javascript
   // Wrong - Express style!
   app.get('/users/:id', (c) => {
     const id = c.req.params.id;  // undefined!
   });
   
   // Correct - Hono method style!
   app.get('/users/:id', (c) => {
     const id = c.req.param('id');  // Works!
   });
   ```

2. **Parameter type confusion**:
   ```javascript
   app.get('/users/:id', (c) => {
     const id = c.req.param('id');
     if (id > 100) {  // String comparison! '9' > '100' is true!
       // Wrong!
     }
     
     // Correct:
     if (parseInt(id) > 100) {
       // Right!
     }
   });
   ```

3. **Route order conflicts**:
   ```javascript
   // Wrong order!
   app.get('/users/:id', ...);        // Matches EVERYTHING
   app.get('/users/active', ...);     // Never reached!
   
   // Correct order:
   app.get('/users/active', ...);     // Specific first
   app.get('/users/:id', ...);        // Generic last
   ```

4. **Forgetting to return response**:
   ```javascript
   app.get('/search', (c) => {
     const q = c.req.query('q');
     c.json({ query: q });  // Missing return!
   });
   
   // Correct:
   app.get('/search', (c) => {
     const q = c.req.query('q');
     return c.json({ query: q });  // Always return!
   });
   ```

5. **Missing query parameters**:
   ```javascript
   app.get('/search', (c) => {
     const limit = c.req.query('limit');  // undefined if not provided!
     
     // Better:
     const limit = c.req.query('limit') || '10';
     const limitNum = parseInt(c.req.query('limit')) || 10;
   });
   ```