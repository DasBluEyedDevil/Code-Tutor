---
type: "THEORY"
title: "Anatomy of an Error"
---

Every error in JavaScript is an **Object** based on the built-in `Error` class.

### 1. The Big 6 Error Types
*   **`Error`:** The generic base class.
*   **`ReferenceError`:** Using a variable that hasn't been declared.
*   **`TypeError`:** Using a value in an incompatible way (like `null.name`).
*   **`SyntaxError`:** Interpreting code that violates the rules of JavaScript.
*   **`RangeError`:** Using a number that is out of range (e.g., `new Array(9999999999)`).
*   **`URIError`:** Using global URI handling functions incorrectly.

### 2. Error Properties
When you catch an error object `e`, it has three standard properties:
1.  **`e.name`:** The string name of the error type (e.g., `"TypeError"`).
2.  **`e.message`:** The human-readable string you provided when throwing the error.
3.  **`e.stack`:** A non-standard but widely supported property showing the "trace"—the sequence of function calls that led to the crash.

### 3. The `instanceof` Operator
Because errors are classes, you can use the `instanceof` keyword to differentiate them in your `catch` block. This allows you to handle specific problems (like a missing variable) differently than generic crashes.

### 4. Why distinguish?
Professional apps use different error types to provide better feedback. A `TypeError` might mean the programmer made a mistake, while a custom `NetworkError` might mean the user's internet is down.
