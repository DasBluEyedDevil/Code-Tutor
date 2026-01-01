---
type: "THEORY"
title: "Advanced Routing Concepts Summary"
---

Here is a comprehensive summary of Hono's advanced routing capabilities that help you build scalable, well-organized APIs:

1. **Route Groups with app.route(prefix, subApp)**:
   - Create modular route collections as separate Hono instances
   - Mount them with a prefix: `app.route('/api/users', usersRoutes)`
   - Each group can have its own middleware
   - Great for organizing large applications into feature modules
   - Example file structure: `routes/users.ts`, `routes/products.ts`

2. **Wildcard Routes (*)**:
   - Match any path segment: `/files/*` matches `/files/a/b/c`
   - Use `c.req.path` to get the full matched path
   - Perfect for: static files, proxies, catch-all 404 handlers
   - Order matters: wildcards should come after specific routes
   - `app.all('*', handler)` as the last route catches unmatched requests

3. **Request Body Parsing Methods**:
   - `await c.req.json()` - Parse JSON (application/json)
   - `await c.req.formData()` - Parse form data (multipart/form-data, x-www-form-urlencoded)
   - `await c.req.text()` - Get raw text body
   - `await c.req.blob()` - Get body as Blob
   - `await c.req.arrayBuffer()` - Get raw binary data
   - FormData.get('field') for single values, getAll('field') for arrays/multiple files

4. **Multiple Handlers Per Route**:
   - Chain handlers: `app.get('/path', handler1, handler2, handler3)`
   - Each handler receives (c, next) and must call `await next()` to continue
   - Stop the chain by returning a response without calling next()
   - Use c.set('key', value) to pass data between handlers
   - Great for: validation, auth checks, logging, rate limiting

5. **Handler Execution Order**:
   - Handlers run left-to-right in the order specified
   - Each handler can: continue (await next()), stop (return response), or throw
   - Code after `await next()` runs in reverse order (like middleware)

6. **Best Practices**:
   - Define specific routes before parameterized routes
   - Define parameterized routes before wildcards
   - Put catch-all 404 handler last
   - Use route groups to organize related endpoints
   - Extract common handler chains into reusable arrays