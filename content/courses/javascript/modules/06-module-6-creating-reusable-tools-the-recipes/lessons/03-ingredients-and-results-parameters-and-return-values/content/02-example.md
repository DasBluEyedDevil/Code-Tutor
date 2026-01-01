---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Function with one parameter
function greet(name) {
  return 'Hello, ' + name + '!';
}

let message = greet('Alice');
console.log(message);  // Hello, Alice!

// Function with multiple parameters
function calculateArea(width, height) {
  return width * height;
}

let area = calculateArea(5, 10);
console.log(area);  // 50

// Parameters with default values (ES2015+)
function greet(name = 'Guest') {
  return 'Hello, ' + name + '!';
}

console.log(greet());        // Hello, Guest!
console.log(greet('Bob'));   // Hello, Bob!

// Function that doesn't return (returns undefined)
function logMessage(msg) {
  console.log(msg);
  // No return statement
}

let result = logMessage('Test');
console.log(result);  // undefined

// Returning early
function divide(a, b) {
  if (b === 0) {
    return 'Cannot divide by zero';
  }
  return a / b;
}

console.log(divide(10, 2));  // 5
console.log(divide(10, 0));  // Cannot divide by zero

// Returning objects
function createUser(name, age) {
  return {
    name: name,
    age: age,
    isAdult: age >= 18
  };
}

let user = createUser('Alice', 25);
console.log(user);  // {name: 'Alice', age: 25, isAdult: true}
```
