---
type: "KEY_POINT"
title: "Build for Different Targets"
---

**Browser:**
```javascript
await Bun.build({
  target: 'browser',
  // Bundles everything, polyfills Node APIs
});
```

**Node.js:**
```javascript
await Bun.build({
  target: 'node',
  // Keeps require(), doesn't bundle node_modules
});
```

**Bun:**
```javascript
await Bun.build({
  target: 'bun',
  // Optimized for Bun runtime
});
```