---
type: "WARNING"
title: "Function Pitfalls"
---

### 1. Defining vs. Calling
A common mistake is thinking that defining a function makes it run.
```javascript
function alert() {
    console.log("Help!");
}
// Nothing happens!
```
You **must** use the parentheses `alert();` to trigger the execution.

### 2. Forgetting the Parentheses
If you use a function name without parentheses, you are referring to the **function itself**, not its result.
```javascript
console.log(sayGreeting); // Displays "[Function: sayGreeting]"
console.log(sayGreeting()); // Executes the function
```

### 3. Parameter Mismatch
If you define a function with two parameters `function add(a, b)` but call it with only one `add(5)`, the second parameter (`b`) will be `undefined`. This often leads to `NaN` (Not a Number) errors in math functions.

### 4. Too Many Responsibilities
A function should do **one thing well**. If your function is called `saveUserAndEmailAdminAndLogToDatabase()`, it's trying to do too much. Break it down into three smaller, focused functions.
