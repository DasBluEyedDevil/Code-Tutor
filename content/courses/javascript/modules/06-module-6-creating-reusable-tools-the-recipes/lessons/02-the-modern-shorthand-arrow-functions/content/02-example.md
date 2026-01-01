---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Traditional function
function add(a, b) {
  return a + b;
}

// Arrow function (same thing, shorter)
const add = (a, b) => {
  return a + b;
};

// Even shorter (implicit return for one-liners)
const add = (a, b) => a + b;

// Examples of different arrow function forms

// No parameters
const sayHello = () => console.log('Hello!');
sayHello();  // Hello!

// One parameter (parentheses optional)
const double = num => num * 2;
console.log(double(5));  // 10

// Multiple parameters (need parentheses)
const multiply = (a, b) => a * b;
console.log(multiply(3, 4));  // 12

// Multiple lines (need curly braces and explicit return)
const greetPerson = (name) => {
  let greeting = 'Hello, ' + name;
  return greeting + '!';
};
console.log(greetPerson('Alice'));  // Hello, Alice!

// Using with array methods
let numbers = [1, 2, 3, 4, 5];
let doubled = numbers.map(n => n * 2);
console.log(doubled);  // [2, 4, 6, 8, 10]
```
