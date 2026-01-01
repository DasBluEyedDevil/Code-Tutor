---
type: "LEGACY_COMPARISON"
title: "Node.js/Express Equivalent"
---

Hono uses method calls instead of object properties for accessing parameters. This is slightly different from Express but provides a cleaner API.

```javascript
// Express way - object properties
app.get('/users/:id', (req, res) => {
  const id = req.params.id;         // Object property
  const search = req.query.search;  // Object property
  res.json({ id, search });
});

// Hono way - method calls
app.get('/users/:id', (c) => {
  const id = c.req.param('id');     // Method call
  const search = c.req.query('search'); // Method call
  return c.json({ id, search });
});

// Key differences:
// Express: req.params.id, req.query.search, req.body
// Hono: c.req.param('id'), c.req.query('search'), await c.req.json()
```
