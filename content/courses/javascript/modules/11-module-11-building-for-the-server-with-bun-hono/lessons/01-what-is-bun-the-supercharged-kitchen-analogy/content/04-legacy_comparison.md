---
type: "LEGACY_COMPARISON"
title: "Node.js Equivalent"
---

If you've used Node.js before, here's how Bun compares:

**Node.js** requires separate tools:
- `npm` or `yarn` for packages
- `tsc` for TypeScript compilation
- `jest` or `vitest` for testing
- `webpack` or `esbuild` for bundling

**Bun** has everything built-in!

The code patterns are similar, but Bun is faster and simpler.

```javascript
// Node.js way (requires setup):
// 1. npm init
// 2. npm install typescript ts-node
// 3. Create tsconfig.json
// 4. npx ts-node app.ts

// Node.js file reading:
const fs = require('fs');
fs.readFile('data.txt', 'utf8', (err, data) => {
  if (err) throw err;
  console.log(data);
});

// Node.js HTTP server:
const http = require('http');
const server = http.createServer((req, res) => {
  res.writeHead(200, { 'Content-Type': 'text/plain' });
  res.end('Hello from Node.js!');
});
server.listen(3000);
```
