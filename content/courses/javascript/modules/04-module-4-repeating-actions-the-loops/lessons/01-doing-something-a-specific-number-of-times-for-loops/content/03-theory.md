---
type: "THEORY"
title: "Anatomy of a For Loop"
---

The `for` loop has a very specific structure in JavaScript. Everything the loop needs to know is contained in one line:

```javascript
for (initialization; condition; increment) {
    // code block
}
```

### 1. Initialization: `let i = 0`
This happens **once** at the very beginning. We usually create a variable called `i` (short for "index" or "iterator") to keep track of where we are.

### 2. Condition: `i < 5`
Before **every** lap (including the first one), the computer checks this. If it's `true`, the loop runs. If it's `false`, the loop ends immediately.

### 3. Increment: `i++`
This happens at the **end** of every lap. `i++` is shorthand for `i = i + 1`. It updates our counter so we can eventually reach the stop condition.

### 4. The Loop Lifecycle
1.  Run the **Initialization**.
2.  Check the **Condition**.
3.  If true, run the **Code Block**.
4.  Run the **Increment**.
5.  Go back to Step 2.

### Scope Note
Because we use `let i = 0` inside the loop parentheses, that variable `i` only exists **inside** the loop. If you try to use it after the loop is finished, your program will crash!
