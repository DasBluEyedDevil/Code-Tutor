---
type: "EXAMPLE"
title: "Basic try-catch Structure"
---

The fundamental pattern for catching errors in JavaScript. The try block contains code that might fail, and the catch block handles the error gracefully.

```javascript
// Basic try-catch pattern
try {
  // Code that might throw an error
  let result = riskyOperation();
  console.log('Success:', result);
} catch (error) {
  // Code that runs if an error occurs
  console.log('Something went wrong:', error.message);
}

// Real-world example: Parsing JSON
let jsonString = '{"name": "Alice", "age": 25}';
let badJsonString = 'not valid json';

// Parsing valid JSON
try {
  let user = JSON.parse(jsonString);
  console.log('User name:', user.name); // Output: User name: Alice
} catch (error) {
  console.log('Failed to parse JSON:', error.message);
}

// Parsing invalid JSON
try {
  let data = JSON.parse(badJsonString);
  console.log('Data:', data); // This line never runs
} catch (error) {
  console.log('Failed to parse JSON:', error.message);
  // Output: Failed to parse JSON: Unexpected token 'o' at position 1
}

console.log('Program continues normally!'); // This still runs!
```
