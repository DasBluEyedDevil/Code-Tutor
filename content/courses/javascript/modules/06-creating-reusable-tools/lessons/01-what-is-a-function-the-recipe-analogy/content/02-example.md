---
type: "EXAMPLE"
title: "Declaring and Calling Functions"
---

```javascript
// 1. Defining a Function
// We use the 'function' keyword, a name, and parentheses ()
function sayGreeting() {
    console.log("-------------------");
    console.log("Hello, User!");
    console.log("Welcome back to the app.");
    console.log("-------------------");
}

// 2. Calling (Executing) the Function
// The function code doesn't run until you "call" its name
sayGreeting();
sayGreeting(); // You can call it as many times as you want!

// 3. Functions with Input (Parameters)
function greetUser(username) {
    console.log(`Hello, ${username}!`);
}

greetUser('Alice');
greetUser('Bob');

// 4. Multiple Parameters
function showSum(a, b) {
    const result = a + b;
    console.log(`The sum of ${a} and ${b} is: ${result}`);
}

showSum(10, 20);
```