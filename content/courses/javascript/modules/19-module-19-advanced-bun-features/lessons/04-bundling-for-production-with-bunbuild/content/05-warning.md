---
type: "WARNING"
title: "Common Bundling Mistakes"
---

**1. Not checking build success:**
```javascript
// WRONG - Ignoring errors
await Bun.build({ entrypoints: ['./index.ts'] });

// RIGHT - Handle failures
const result = await Bun.build({ entrypoints: ['./index.ts'] });
if (!result.success) {
  console.error(result.logs);
  process.exit(1);
}
```

**2. Wrong target for your environment:**
- Browser code with `target: 'node'` won't work in browsers
- Server code with `target: 'browser'` may include unnecessary polyfills

**3. Forgetting to externalize large dependencies:**
```javascript
// For libraries, don't bundle peer dependencies
external: ['react', 'react-dom']
```