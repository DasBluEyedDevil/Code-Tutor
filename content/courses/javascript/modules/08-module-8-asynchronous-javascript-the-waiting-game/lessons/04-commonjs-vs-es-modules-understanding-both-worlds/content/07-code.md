---
type: "EXAMPLE"
title: "Dual Package Authoring"
---

If you are publishing a library to npm, you may want to support both CommonJS and ES Module consumers. This is called dual package authoring. The package.json exports field with conditional exports makes this possible.

```javascript
// ============================================
// DUAL PACKAGE STRUCTURE
// ============================================

// my-library/
// ├── package.json
// ├── src/
// │   └── index.js         (Source code)
// ├── dist/
// │   ├── index.mjs        (ES Module build)
// │   ├── index.cjs        (CommonJS build)
// │   └── index.d.ts       (TypeScript types)
// └── README.md

// ============================================
// package.json WITH CONDITIONAL EXPORTS
// ============================================

{
  "name": "my-awesome-library",
  "version": "1.0.0",
  "type": "module",
  
  "main": "./dist/index.cjs",
  "module": "./dist/index.mjs",
  "types": "./dist/index.d.ts",
  
  "exports": {
    ".": {
      "types": "./dist/index.d.ts",
      "import": "./dist/index.mjs",
      "require": "./dist/index.cjs",
      "default": "./dist/index.mjs"
    },
    "./utils": {
      "types": "./dist/utils.d.ts",
      "import": "./dist/utils.mjs",
      "require": "./dist/utils.cjs"
    },
    "./package.json": "./package.json"
  },
  
  "files": ["dist"],
  
  "scripts": {
    "build": "tsup src/index.ts --format cjs,esm --dts",
    "build:watch": "tsup src/index.ts --format cjs,esm --dts --watch"
  }
}

// ============================================
// CONDITION ORDER MATTERS!
// ============================================

// Node.js checks conditions in order, uses first match
"exports": {
  ".": {
    // TypeScript should come first (for tooling)
    "types": "./dist/index.d.ts",
    
    // Bun-specific (if needed)
    "bun": "./dist/index.bun.js",
    
    // Deno-specific (if needed)  
    "deno": "./dist/index.deno.js",
    
    // ES Module consumers (import)
    "import": "./dist/index.mjs",
    
    // CommonJS consumers (require)
    "require": "./dist/index.cjs",
    
    // Fallback (should be last)
    "default": "./dist/index.mjs"
  }
}

// ============================================
// BUILD TOOLS FOR DUAL PACKAGES
// ============================================

// Using tsup (recommended, simple)
// tsup.config.ts
import { defineConfig } from 'tsup';

export default defineConfig({
  entry: ['src/index.ts'],
  format: ['cjs', 'esm'],
  dts: true,
  clean: true,
  splitting: false,
  sourcemap: true
});

// Using esbuild
import * as esbuild from 'esbuild';

// ESM build
await esbuild.build({
  entryPoints: ['src/index.ts'],
  bundle: true,
  format: 'esm',
  outfile: 'dist/index.mjs'
});

// CJS build
await esbuild.build({
  entryPoints: ['src/index.ts'],
  bundle: true,
  format: 'cjs',
  outfile: 'dist/index.cjs'
});

// ============================================
// CONSUMERS USE IT SEAMLESSLY
// ============================================

// ESM consumer
import { myFunction } from 'my-awesome-library';
import { helper } from 'my-awesome-library/utils';

// CJS consumer  
const { myFunction } = require('my-awesome-library');
const { helper } = require('my-awesome-library/utils');

// TypeScript consumer (types just work!)
import { myFunction } from 'my-awesome-library';
// Hover shows: function myFunction(arg: string): number
```
