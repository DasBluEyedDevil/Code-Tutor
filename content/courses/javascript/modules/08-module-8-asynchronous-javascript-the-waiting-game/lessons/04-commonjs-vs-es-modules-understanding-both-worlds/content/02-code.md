---
type: "CODE"
title: "CommonJS Syntax (require/exports)"
---

CommonJS is the traditional Node.js module system. It uses the `require()` function to import modules and `module.exports` or `exports` to expose functionality. Understanding this syntax is essential because many npm packages still use it.

```javascript
// ============================================
// EXPORTING IN COMMONJS
// ============================================

// math.js - Exporting multiple values
function add(a, b) {
  return a + b;
}

function subtract(a, b) {
  return a - b;
}

// Method 1: Assign to module.exports object
module.exports = {
  add: add,
  subtract: subtract
};

// Method 2: Shorthand with ES6 property shorthand
module.exports = { add, subtract };

// Method 3: Add properties individually
module.exports.add = add;
module.exports.subtract = subtract;

// Method 4: Using exports shorthand (same as module.exports)
exports.add = add;
exports.subtract = subtract;

// WARNING: This BREAKS exports (reassigning the reference)
exports = { add, subtract }; // WRONG! This doesn't work!

// ============================================
// EXPORTING A SINGLE VALUE (Default-like)
// ============================================

// logger.js - Single export
class Logger {
  log(message) {
    console.log(`[LOG]: ${message}`);
  }
}

module.exports = Logger; // Export the class itself

// ============================================
// IMPORTING IN COMMONJS
// ============================================

// app.js - Importing modules

// Import the entire module object
const math = require('./math.js');
console.log(math.add(2, 3)); // 5
console.log(math.subtract(5, 2)); // 3

// Destructure during import
const { add, subtract } = require('./math.js');
console.log(add(2, 3)); // 5

// Import single export
const Logger = require('./logger.js');
const logger = new Logger();
logger.log('Hello!'); // [LOG]: Hello!

// ============================================
// MODULE RESOLUTION
// ============================================

// Relative paths (your own files)
const utils = require('./utils');      // ./utils.js
const config = require('../config');   // ../config.js
const data = require('./data/users');  // ./data/users.js

// Node modules (from node_modules folder)
const express = require('express');    // node_modules/express
const lodash = require('lodash');      // node_modules/lodash

// Built-in Node.js modules
const fs = require('fs');              // File system
const path = require('path');          // Path utilities
const http = require('http');          // HTTP server

// Node.js 16+ recommends 'node:' prefix for built-ins
const fs = require('node:fs');
const path = require('node:path');
```
