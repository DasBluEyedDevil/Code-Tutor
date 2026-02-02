---
type: "EXAMPLE"
title: "package.json Configuration"
---

The package.json file controls how Node.js interprets your modules. The `type` field determines whether .js files are treated as CommonJS or ES Modules. Understanding this configuration is essential for avoiding confusing module errors.

```json
// ============================================
// BASIC MODULE TYPE CONFIGURATION
// ============================================

// package.json - ES Modules project (RECOMMENDED for new projects)
{
  "name": "my-esm-project",
  "type": "module",
  "version": "1.0.0"
}
// With "type": "module":
//   - .js files are treated as ES Modules
//   - Use import/export syntax
//   - Use .cjs extension for CommonJS files

// package.json - CommonJS project (legacy default)
{
  "name": "my-cjs-project",
  "type": "commonjs",
  "version": "1.0.0"
}
// With "type": "commonjs" (or no type field):
//   - .js files are treated as CommonJS
//   - Use require/module.exports syntax
//   - Use .mjs extension for ES Module files

// ============================================
// FILE EXTENSIONS OVERRIDE package.json
// ============================================

// .mjs - ALWAYS ES Module (regardless of package.json)
import { something } from './other.mjs';
export const value = 42;

// .cjs - ALWAYS CommonJS (regardless of package.json)
const something = require('./other.cjs');
module.exports = { value: 42 };

// .js - Determined by nearest package.json "type" field

// ============================================
// THE "exports" FIELD (Modern Package Authoring)
// ============================================

// package.json - Modern package with exports field
{
  "name": "my-library",
  "version": "2.0.0",
  "type": "module",
  
  // Main entry (legacy, still useful for older tools)
  "main": "./dist/index.cjs",
  
  // Module entry (for bundlers)
  "module": "./dist/index.mjs",
  
  // Exports field (Node.js 12+, recommended)
  "exports": {
    // Main entry point
    ".": {
      "import": "./dist/index.mjs",
      "require": "./dist/index.cjs",
      "types": "./dist/index.d.ts"
    },
    // Subpath exports
    "./utils": {
      "import": "./dist/utils.mjs",
      "require": "./dist/utils.cjs"
    },
    // Pattern exports
    "./components/*": "./dist/components/*.js"
  },
  
  // TypeScript types
  "types": "./dist/index.d.ts"
}

// Usage by consumers:
import lib from 'my-library';           // Uses exports["."]
import { helper } from 'my-library/utils'; // Uses exports["./utils"]
```
