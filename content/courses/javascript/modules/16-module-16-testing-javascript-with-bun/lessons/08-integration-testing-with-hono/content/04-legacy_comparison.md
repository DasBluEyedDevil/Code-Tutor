---
type: "LEGACY_COMPARISON"
title: "Vitest Equivalent"
---

Testing Hono works the same in Vitest - app.request() is framework-agnostic. Only the test imports differ.

```javascript
// Vitest version
import { describe, it, expect, beforeEach } from 'vitest';
import { Hono } from 'hono';

// Same app setup...

describe('Todo API', () => {
  let app;

  beforeEach(() => {
    app = createApp();
  });

  it('GET /todos returns empty array', async () => {
    const res = await app.request('/todos');
    expect(res.status).toBe(200);
    // Works exactly the same!
  });
});
```
