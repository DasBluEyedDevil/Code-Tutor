---
type: "WARNING"
title: "The Return Pitfall"
---

### 1. `console.log` is NOT `return`
This is the most common confusion for new programmers. 
*   `console.log` displays a message to a human.
*   `return` sends a value back to the program.
If your function logs a result but doesn't return it, you won't be able to use that result in another calculation.

### 2. Unreachable Code
Because `return` stops the function, anything written below it will never run.
```javascript
function sayHello() {
    return "Hello";
    console.log("This will never be seen!"); // DEAD CODE
}
```

### 3. Returning "Nothing"
A `return;` statement by itself (with no value) will simply exit the function and return `undefined`. This is often used for "Guard Clauses" where you want to stop a function early if something is wrong.

### 4. Forgetting to return
If you do a bunch of math in a function but forget to `return` the result, the function call will evaluate to `undefined`. Always check that your function ends with the result you want!
