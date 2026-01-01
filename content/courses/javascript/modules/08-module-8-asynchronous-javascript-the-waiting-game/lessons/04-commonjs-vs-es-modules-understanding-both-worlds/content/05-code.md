---
type: "CODE"
title: "Importing CJS from ESM"
---

When working in ES Modules, you will often need to import CommonJS packages. Node.js provides good interoperability, but there are some gotchas to understand. Most npm packages are still CommonJS, so this knowledge is essential.

```javascript
// ============================================
// BASIC CJS IMPORT IN ESM
// ============================================

// Most CJS packages work with default import
import express from 'express';        // CJS package
import lodash from 'lodash';          // CJS package
import chalk from 'chalk';            // CJS package

const app = express();
const result = lodash.chunk([1, 2, 3, 4], 2);

// ============================================
// THE DEFAULT EXPORT GOTCHA
// ============================================

// CJS module (old-package.js)
module.exports = {
  foo: 'bar',
  doSomething: function() {}
};

// In ESM, this becomes the DEFAULT export
import pkg from 'old-package';
console.log(pkg.foo);      // 'bar'
pkg.doSomething();

// Named imports MAY work (Node.js tries to detect them)
import { foo, doSomething } from 'old-package'; // Sometimes works!

// But this is NOT guaranteed - depends on how CJS exports
// When in doubt, use default import and destructure:
import pkg from 'old-package';
const { foo, doSomething } = pkg;

// ============================================
// USING createRequire() FOR ADVANCED CASES
// ============================================

import { createRequire } from 'node:module';
const require = createRequire(import.meta.url);

// Now you can use require() in ESM!
const cjsModule = require('./legacy-module.cjs');
const jsonData = require('./data.json'); // Before import attributes

// Useful for:
// - Loading JSON (before ES2025 Import Attributes)
// - CJS modules with complex exports
// - __dirname/__filename equivalents

// ============================================
// DYNAMIC IMPORT FOR CJS MODULES
// ============================================

// Dynamic import always works with CJS
const cjsModule = await import('cjs-package');
console.log(cjsModule.default); // The module.exports value

// Destructure if needed
const { default: pkg } = await import('cjs-package');
const { foo, bar } = pkg;

// ============================================
// COMMON INTEROP PATTERNS
// ============================================

// Pattern 1: Default import + destructure (safest)
import pkg from 'cjs-package';
const { methodA, methodB } = pkg;

// Pattern 2: Try named imports first, fall back to default
try {
  // This might work if Node.js detects named exports
  const { specificMethod } = await import('cjs-package');
} catch {
  const pkg = await import('cjs-package');
  const { specificMethod } = pkg.default;
}

// Pattern 3: Namespace import for exploration
import * as pkg from 'cjs-package';
console.log(Object.keys(pkg)); // See what's available
console.log(pkg.default);      // The actual CJS exports
```
