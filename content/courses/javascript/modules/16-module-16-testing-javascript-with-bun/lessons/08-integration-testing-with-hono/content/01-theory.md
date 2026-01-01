---
type: "THEORY"
title: "Testing Hono Apps with app.request()"
---

Hono provides a built-in way to test your API without starting a server:

```javascript
import { Hono } from 'hono';

const app = new Hono();
app.get('/users', (c) => c.json([{ id: 1, name: 'Alice' }]));

// Test without starting a server!
const res = await app.request('/users');
const data = await res.json();
```

**Why app.request()?**
- No server startup/shutdown overhead
- Tests run faster
- No port conflicts
- Full request/response testing

Test structure for APIs:
```
src/
  routes/
    users.ts
    users.test.ts    <- Co-located tests
  app.ts
tests/
  integration/
    api.test.ts      <- Full API tests
```