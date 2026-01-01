---
type: "EXAMPLE"
title: "Function Parameter Destructuring"
---

Destructuring can be used directly in function parameters, making functions cleaner and more flexible. This is the foundation of the 'options object' pattern used extensively in modern JavaScript.

```javascript
// === BASIC PARAMETER DESTRUCTURING ===

// Old way: access properties inside function
function greetOld(user) {
  console.log('Hello, ' + user.name + '! You are ' + user.age + ' years old.');
}

// New way: destructure in parameter
function greet({ name, age }) {
  console.log(`Hello, ${name}! You are ${age} years old.`);
}

greet({ name: 'Alice', age: 28 });  // Hello, Alice! You are 28 years old.

// === WITH DEFAULTS ===
function createWidget({ width = 100, height = 100, color = 'blue' }) {
  console.log(`Widget: ${width}x${height}, color: ${color}`);
  return { width, height, color };
}

createWidget({ width: 200 });               // Widget: 200x100, color: blue
createWidget({ color: 'red' });             // Widget: 100x100, color: red
createWidget({ width: 50, height: 50 });    // Widget: 50x50, color: blue
createWidget({});                            // Widget: 100x100, color: blue

// === DEFAULT FOR ENTIRE PARAMETER ===
// What if no object is passed at all?
function greetSafe({ name = 'Guest', age = 0 } = {}) {
  console.log(`Hello, ${name}! Age: ${age}`);
}

greetSafe({ name: 'Bob' });  // Hello, Bob! Age: 0
greetSafe({});               // Hello, Guest! Age: 0
greetSafe();                 // Hello, Guest! Age: 0 (works because of = {})

// Without the = {}, calling greetSafe() would crash!
// Cannot destructure property 'name' of undefined

// === ARRAY DESTRUCTURING IN PARAMETERS ===
function processCoordinates([x, y, z = 0]) {
  console.log(`X: ${x}, Y: ${y}, Z: ${z}`);
}

processCoordinates([10, 20]);      // X: 10, Y: 20, Z: 0
processCoordinates([5, 15, 25]);   // X: 5, Y: 15, Z: 25

// Practical: Swap function
function swap([a, b]) {
  return [b, a];
}
console.log(swap([1, 2]));  // [2, 1]

// === NAMED PARAMETERS PATTERN ===
// Much more readable than positional parameters!

// Confusing: what do these arguments mean?
function createButtonBad(text, width, height, primary, disabled) {
  // ...
}
createButtonBad('Click', 120, 40, true, false);  // What's true? What's false?

// Clear: named parameters via destructuring
function createButton({ text, width = 100, height = 40, primary = false, disabled = false }) {
  console.log(`Button: "${text}", ${width}x${height}`);
  console.log(`Primary: ${primary}, Disabled: ${disabled}`);
}

createButton({
  text: 'Submit',
  primary: true,
  width: 150
});
// Arguments are self-documenting!

// === OPTIONS OBJECT PATTERN ===
function fetchData(url, {
  method = 'GET',
  headers = {},
  body = null,
  timeout = 5000,
  retries = 3
} = {}) {
  console.log(`Fetching: ${url}`);
  console.log(`Method: ${method}, Timeout: ${timeout}ms, Retries: ${retries}`);
  // ... actual fetch logic
}

// Use with defaults
fetchData('https://api.example.com/users');
// Fetching: https://api.example.com/users
// Method: GET, Timeout: 5000ms, Retries: 3

// Override some options
fetchData('https://api.example.com/users', {
  method: 'POST',
  body: JSON.stringify({ name: 'Alice' }),
  timeout: 10000
});
// Fetching: https://api.example.com/users
// Method: POST, Timeout: 10000ms, Retries: 3

// === COMBINING WITH REST ===
function logUser({ name, email, ...metadata }) {
  console.log(`User: ${name} (${email})`);
  console.log('Metadata:', metadata);
}

logUser({
  name: 'Diana',
  email: 'd@test.com',
  role: 'admin',
  team: 'engineering',
  lastActive: '2024-01-20'
});
// User: Diana (d@test.com)
// Metadata: { role: 'admin', team: 'engineering', lastActive: '2024-01-20' }
```
