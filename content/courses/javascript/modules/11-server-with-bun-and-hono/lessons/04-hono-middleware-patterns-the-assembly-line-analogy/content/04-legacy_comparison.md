---
type: "LEGACY_COMPARISON"
title: "Node.js/Express Equivalent"
---

Hono middleware is simpler than Express. The key differences are: unified context object, async/await by default, and c.set()/c.get() for data storage instead of mutating req.

```javascript
// Express middleware (older pattern)
app.use((req, res, next) => {
  req.user = { id: 1, name: 'Alice' };  // Mutate req object
  next();  // Not async by default
});

app.get('/profile', (req, res) => {
  res.json(req.user);  // Access from req
});

// Hono middleware (modern pattern)
app.use('*', async (c, next) => {
  c.set('user', { id: 1, name: 'Alice' });  // Use context storage
  await next();  // Async by default
});

app.get('/profile', (c) => {
  const user = c.get('user');  // Access from context
  return c.json(user);
});

// Key differences:
// Express: (req, res, next) with next() callback
// Hono: async (c, next) with await next()
// Express: req.user = data (mutation)
// Hono: c.set('user', data) (storage)
```
