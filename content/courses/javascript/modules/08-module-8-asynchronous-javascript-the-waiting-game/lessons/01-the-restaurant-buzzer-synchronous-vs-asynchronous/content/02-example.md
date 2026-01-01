---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// SYNCHRONOUS (Blocking) - Everything waits
console.log('Start');
for (let i = 0; i < 3; i++) {
  console.log('Step ' + i);
}
console.log('End');
// Output: Start, Step 0, Step 1, Step 2, End (in order)

// ASYNCHRONOUS (Non-blocking) - Using setTimeout
console.log('Start');

setTimeout(function() {
  console.log('This runs after 2 seconds');
}, 2000);

console.log('End');
// Output: Start, End, (wait 2 seconds), This runs after 2 seconds
// Notice 'End' comes BEFORE the timeout!

// Practical example: Loading data
console.log('Fetching user data...');

// Simulating a slow network request (async)
setTimeout(function() {
  console.log('User data loaded: Alice, age 25');
}, 1000);

console.log('Continuing with other tasks...');
// Output:
// Fetching user data...
// Continuing with other tasks...
// (1 second later) User data loaded: Alice, age 25

// Multiple async operations
console.log('Cooking eggs');

setTimeout(() => console.log('Eggs done!'), 2000);
setTimeout(() => console.log('Toast done!'), 1000);
setTimeout(() => console.log('Coffee done!'), 1500);

console.log('Started all cooking tasks');
// Output:
// Cooking eggs
// Started all cooking tasks
// (1s) Toast done!
// (1.5s) Coffee done!
// (2s) Eggs done!
```
