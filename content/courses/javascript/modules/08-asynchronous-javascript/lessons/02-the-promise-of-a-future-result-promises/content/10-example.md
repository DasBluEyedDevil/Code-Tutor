---
type: "EXAMPLE"
title: "Top-Level Await in Practice"
---

No more IIFE (Immediately Invoked Function Expression) hacks!

```javascript
// OLD WAY (pre-ESM): Wrap everything in async IIFE
(async () => {
  const data = await fetchData();
  console.log(data);
})();

// NEW WAY (ESM): Just use await directly!
const data = await fetchData();
console.log(data);

// Real example: Server with Bun + Hono
import { Hono } from 'hono';

const config = await Bun.file('config.json').json();
const app = new Hono();

app.get('/', (c) => c.text(`Running on port ${config.port}`));

export default {
  port: config.port,
  fetch: app.fetch,
};
```
