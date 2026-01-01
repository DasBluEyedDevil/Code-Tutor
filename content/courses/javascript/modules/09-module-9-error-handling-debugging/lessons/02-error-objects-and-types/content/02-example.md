---
type: "EXAMPLE"
title: "Built-in Error Types"
---

JavaScript has several built-in error types, each indicating a specific category of problem. Understanding these helps you diagnose issues quickly.

```javascript
// 1. TypeError - Wrong type for an operation
try {
  let num = 42;
  num.toUpperCase(); // Numbers don't have toUpperCase!
} catch (error) {
  console.log(error.name);    // 'TypeError'
  console.log(error.message); // 'num.toUpperCase is not a function'
}

try {
  null.toString(); // Can't call methods on null
} catch (error) {
  console.log('TypeError:', error.message);
  // "Cannot read properties of null (reading 'toString')"
}

// 2. ReferenceError - Variable doesn't exist
try {
  console.log(undefinedVariable); // Never declared!
} catch (error) {
  console.log(error.name);    // 'ReferenceError'
  console.log(error.message); // 'undefinedVariable is not defined'
}

// 3. SyntaxError - Code structure is invalid (usually caught at parse time)
// Note: SyntaxErrors are usually caught before code runs
try {
  eval('let x = ;'); // Invalid syntax
} catch (error) {
  console.log(error.name);    // 'SyntaxError'
  console.log(error.message); // 'Unexpected token ;'
}

// 4. RangeError - Value outside allowed range
try {
  let arr = new Array(-1); // Can't have negative length
} catch (error) {
  console.log(error.name);    // 'RangeError'
  console.log(error.message); // 'Invalid array length'
}

try {
  let num = 1;
  num.toFixed(200); // Max precision is 100
} catch (error) {
  console.log('RangeError:', error.message);
}

// 5. URIError - Invalid URI handling
try {
  decodeURIComponent('%'); // Invalid percent encoding
} catch (error) {
  console.log(error.name);    // 'URIError'
  console.log(error.message); // 'URI malformed'
}

// 6. EvalError - Error in eval() (rarely used in modern JS)
// EvalError is mostly historical; errors in eval() now throw other types

// 7. AggregateError - Multiple errors (ES2021)
// Used with Promise.any() when all promises reject
let errors = [
  new Error('First error'),
  new Error('Second error')
];
let aggError = new AggregateError(errors, 'Multiple errors occurred');
console.log(aggError.name);    // 'AggregateError'
console.log(aggError.errors);  // Array of errors
```
