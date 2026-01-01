---
type: "CODE"
title: "ES Modules Syntax (import/export)"
---

ES Modules (ESM) is the official JavaScript module standard. It offers cleaner syntax, static analysis for optimization, and native browser support. This is the modern way to write JavaScript modules and should be your default choice for new projects.

```javascript
// ============================================
// NAMED EXPORTS
// ============================================

// math.js - Multiple named exports
export function add(a, b) {
  return a + b;
}

export function subtract(a, b) {
  return a - b;
}

export const PI = 3.14159;

// Or export at the end (equivalent)
function multiply(a, b) {
  return a * b;
}
const E = 2.71828;
export { multiply, E };

// Export with renaming
export { multiply as mult, E as EULER };

// ============================================
// DEFAULT EXPORTS
// ============================================

// logger.js - Default export (one per file)
export default class Logger {
  log(message) {
    console.log(`[LOG]: ${message}`);
  }
}

// Alternative: export default at the end
class Calculator {
  add(a, b) { return a + b; }
}
export default Calculator;

// Can combine default with named exports
export default class User {}
export function createUser() {}
export const DEFAULT_ROLE = 'guest';

// ============================================
// IMPORTING NAMED EXPORTS
// ============================================

// Import specific exports
import { add, subtract } from './math.js';
console.log(add(2, 3)); // 5

// Import with renaming
import { add as sum, PI } from './math.js';
console.log(sum(2, 3)); // 5

// Import all as namespace object
import * as math from './math.js';
console.log(math.add(2, 3)); // 5
console.log(math.PI); // 3.14159

// ============================================
// IMPORTING DEFAULT EXPORTS
// ============================================

// Default import (no curly braces, any name works)
import Logger from './logger.js';
import MyLogger from './logger.js'; // Same thing, different name

const logger = new Logger();

// Combine default and named imports
import User, { createUser, DEFAULT_ROLE } from './user.js';

// ============================================
// RE-EXPORTS (Barrel Exports)
// ============================================

// index.js - Re-export from other modules
export { add, subtract } from './math.js';
export { default as Logger } from './logger.js';
export * from './utils.js'; // Re-export everything

// Now consumers can import from one place:
import { add, Logger } from './index.js';

// ============================================
// DYNAMIC IMPORTS
// ============================================

// Load module at runtime (returns Promise)
const module = await import('./heavy-module.js');
module.doSomething();

// Conditional loading
if (needsFeature) {
  const { feature } = await import('./feature.js');
  feature();
}

// With destructuring
const { add, subtract } = await import('./math.js');
```
