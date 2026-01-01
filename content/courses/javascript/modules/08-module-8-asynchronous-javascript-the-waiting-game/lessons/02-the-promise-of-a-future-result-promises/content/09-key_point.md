---
type: "KEY_POINT"
title: "Top-Level Await is the Standard"
---

In ES modules (`.mjs` files or `"type": "module"` in package.json), you can use `await` at the top level without wrapping in an async function:

```javascript
// index.js (ESM module)
const config = await Bun.file('config.json').json();
const db = await connectDatabase(config.database);

console.log('Server starting with config:', config.name);

export default {
  port: config.port,
  fetch: app.fetch,
};
```

**This is now the standard pattern for:**
- Server startup sequences
- Loading configuration
- Database connections
- Any async initialization