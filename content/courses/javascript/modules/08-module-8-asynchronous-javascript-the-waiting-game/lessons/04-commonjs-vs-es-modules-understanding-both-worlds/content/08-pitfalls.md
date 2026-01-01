---
type: "PITFALLS"
title: "Common Interop Issues"
---

Module interoperability is one of the trickiest areas in JavaScript. Here are the most common issues developers encounter and how to solve them.

**1. Default Export Confusion (CJS to ESM)**
When importing a CommonJS module into ESM, the entire `module.exports` becomes the default export. If the CJS module exports an object with methods, you must access them through the default:
```javascript
// WRONG: Named import from CJS often fails
import { method } from 'cjs-package'; // May not work!

// RIGHT: Default import, then destructure
import pkg from 'cjs-package';
const { method } = pkg;
```

**2. Top-Level Await Only Works in ESM**
You cannot use `await` at the top level of a CommonJS module. This is ESM-only:
```javascript
// ESM (works)
const data = await fetchData();
export { data };

// CJS (ERROR!)
const data = await fetchData(); // SyntaxError!
module.exports = { data };
```

**3. __dirname and __filename Not Available in ESM**
ESM does not have the CommonJS globals. Use `import.meta.url` instead:
```javascript
// CJS
console.log(__dirname);  // /path/to/directory
console.log(__filename); // /path/to/file.js

// ESM - use import.meta
import { fileURLToPath } from 'node:url';
import { dirname } from 'node:path';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

// Or use import.meta.dirname (Node.js 20.11+)
console.log(import.meta.dirname);  // /path/to/directory
console.log(import.meta.filename); // /path/to/file.js
```

**4. Named Imports from CJS May Not Work**
Node.js attempts to detect named exports from CJS, but it is not always possible:
```javascript
// If CJS does: module.exports = function() {}
import fn from 'pkg';     // Works (default)
import { fn } from 'pkg'; // FAILS!

// If CJS does: exports.fn = function() {}
import { fn } from 'pkg'; // Usually works
```

**5. Circular Dependencies Behave Differently**
CJS gives you a partially-initialized object during circular imports. ESM gives you a live binding that updates when the export is set. This can cause subtle bugs when migrating.

**6. JSON Imports Differ Between Systems**
```javascript
// CJS - works directly
const pkg = require('./package.json');

// ESM (pre-ES2025) - needs createRequire or flag
import { createRequire } from 'node:module';
const require = createRequire(import.meta.url);
const pkg = require('./package.json');

// ESM (ES2025+) - use Import Attributes
import pkg from './package.json' with { type: 'json' };
```