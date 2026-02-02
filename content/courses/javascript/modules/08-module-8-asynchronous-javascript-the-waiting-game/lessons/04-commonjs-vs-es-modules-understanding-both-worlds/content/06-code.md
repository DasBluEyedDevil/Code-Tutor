---
type: "EXAMPLE"
title: "Node.js 22+ require(esm) Support"
---

Node.js 22 introduced a groundbreaking feature: the ability to require() ES Modules from CommonJS code! This makes migration easier and improves interoperability. However, there are important limitations to understand.

```javascript
// ============================================
// THE NEW require(esm) FEATURE (Node.js 22+)
// ============================================

// esm-module.mjs (or .js with "type": "module")
export function greet(name) {
  return `Hello, ${name}!`;
}

export default class User {
  constructor(name) {
    this.name = name;
  }
}

// cjs-consumer.cjs - NOW YOU CAN require() ESM!
const esmModule = require('./esm-module.mjs');

console.log(esmModule.greet('World')); // Hello, World!

const User = esmModule.default;
const user = new User('Alice');

// ============================================
// HOW IT WORKS
// ============================================

// Node.js 22 loads ESM synchronously when required
// This is possible because the module is evaluated eagerly

// Named exports become properties on the required object
const { greet, default: User } = require('./esm-module.mjs');

// ============================================
// LIMITATIONS: NO TOP-LEVEL AWAIT
// ============================================

// This ESM module CANNOT be required:
// async-module.mjs
const data = await fetch('https://api.example.com/data');
export const config = await loadConfig();
export default data;

// Trying to require it throws an error!
// const mod = require('./async-module.mjs'); 
// Error: Cannot require() ES Module with top-level await

// ============================================
// DETECTION AND COMPATIBILITY
// ============================================

// Check if require(esm) is supported
function canRequireESM() {
  try {
    // Node.js 22+ supports this
    const [major] = process.versions.node.split('.');
    return parseInt(major) >= 22;
  } catch {
    return false;
  }
}

// Conditional loading pattern
let esmModule;
if (canRequireESM()) {
  esmModule = require('./module.mjs');
} else {
  // Fallback for older Node.js
  esmModule = await import('./module.mjs');
}

// ============================================
// PRACTICAL MIGRATION SCENARIO
// ============================================

// Before Node.js 22: Dynamic import required in CJS
// old-way.cjs
async function loadESM() {
  const { greet } = await import('./esm-module.mjs');
  return greet('World');
}

// Node.js 22+: Direct require works!
// new-way.cjs
const { greet } = require('./esm-module.mjs');
console.log(greet('World')); // Synchronous, no async needed!
```
