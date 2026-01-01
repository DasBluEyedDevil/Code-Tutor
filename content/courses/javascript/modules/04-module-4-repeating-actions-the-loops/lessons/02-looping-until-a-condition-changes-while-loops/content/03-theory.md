---
type: "THEORY"
title: "Condition-Based Iteration"
---

The `while` loop is simpler in syntax than the `for` loop, but it requires more careful management of your variables.

### 1. The `while` Syntax
```javascript
while (condition) {
    // code block
}
```
The computer checks the condition **before** every run. If the condition is false on the very first try, the loop never runs at all.

### 2. The `do...while` Syntax
This is a variation where the code block runs **before** the condition is checked. 
```javascript
do {
    // code block
} while (condition);
```
**Key Difference:** A `do...while` loop is guaranteed to run **at least once**, regardless of the condition.

### 3. Loop Management
In a `for` loop, the counter (`i++`) is built-in. In a `while` loop, you are responsible for updating the state inside the code block. If you forget to change the variable that the condition depends on, you will create an **Infinite Loop**.

### 4. When to use which?
*   Use **For** when you know the start and end points (e.g., "Loop through 10 items").
*   Use **While** when the end point depends on logic (e.g., "Loop while the user's password is incorrect").
