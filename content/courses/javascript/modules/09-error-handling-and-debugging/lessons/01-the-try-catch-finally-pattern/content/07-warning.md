---
type: "WARNING"
title: "Error Handling Pitfalls"
---

### 1. Swallowing Errors
The most dangerous thing you can do is have an empty `catch` block.
```javascript
try {
    saveData();
} catch (e) {
    // Empty! No log, no alert.
}
```
If `saveData()` fails, your user will never know, and you will have no idea why your database is empty. **Always at least log the error.**

### 2. Over-wrapping
Don't wrap your entire 5,000-line program in a single `try/catch`. 
*   **Result:** If an error happens, you'll know "something" went wrong, but you won't know where.
*   **Fix:** Wrap only the specific pieces of code that you *expect* might fail (like API calls or file reading).

### 3. Syntax Errors
`try/catch` only catches **Runtime Errors** (things that happen while the code is running). It **cannot** catch Syntax Errors (like forgetting a closing bracket). If your code has a syntax error, it won't even start running.

### 4. `finally` vs. `return`
If you `return` a value in the `try` block and also have code in the `finally` block, the `finally` block will run **before** the value is actually returned to the caller. If you also `return` a value in the `finally` block, it will overwrite the previous return value!
