---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding sync vs async:

**Synchronous Code (Default):**
- Runs line by line
- Each line waits for the previous to complete
- Blocking (stops everything until done)

let a = 1;
let b = 2;
let c = a + b;  // Waits for a and b
console.log(c);  // Waits for c

**Asynchronous Code:**
- Starts a task
- Doesn't wait for it to finish
- Continues to next line immediately
- Comes back when task completes

Common async operations:
- setTimeout / setInterval (timers)
- fetch() (network requests)
- Reading files (Node.js)
- Database queries
- User interactions (clicks are async events)

**setTimeout Syntax:**

setTimeout(callbackFunction, delayInMilliseconds);

Examples:
setTimeout(() => console.log('Hi'), 1000);  // After 1 second
setTimeout(myFunction, 500);  // After 0.5 seconds
setTimeout(() => {
  console.log('Multiple');
  console.log('Lines');
}, 2000);  // After 2 seconds

**setInterval (Repeating Timer):**

setInterval(callbackFunction, intervalInMilliseconds);

let count = 0;
let intervalId = setInterval(() => {
  count++;
  console.log('Count:', count);
  
  if (count === 5) {
    clearInterval(intervalId);  // Stop the interval
  }
}, 1000);  // Every 1 second

**Why Async Matters:**

// Synchronous (BAD for web):
let data = fetchDataFromServer();  // Takes 3 seconds, UI freezes!
console.log(data);

// Asynchronous (GOOD for web):
fetchDataFromServer((data) => {
  console.log(data);
});  // UI stays responsive!

**The Event Loop:**

JavaScript has:
1. Call stack (current code running)
2. Web APIs (setTimeout, fetch, etc.)
3. Callback queue (waiting callbacks)
4. Event loop (moves callbacks to stack when empty)

This is how async works without multiple threads!