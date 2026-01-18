---
type: "THEORY"
title: "The Rules of the Playground"
---

Now that you're writing code in a real environment, there are key concepts to understand about how JavaScript "thinks."

### 1. Top-to-Bottom Execution
Computers are very obedient. They start at line 1 and move to the end. If line 5 depends on something that happens on line 2, you must make sure line 2 stays at the top. This order is called the **Execution Flow**.

### 2. Math vs. Text
In JavaScript, we handle numbers and text differently.
*   **Math:** We use operators like `+`, `-`, `*`, `/`.
    *   `10 + 5` becomes `15`.
*   **Text:** We use strings wrapped in quotes (`'Hello'`) or backticks (`` `Hello` ``).

### 3. Template Literals (Modern JavaScript)
Old JavaScript used the `+` symbol to glue text pieces together (e.g., `'Score: ' + 10`). This is called **concatenation**.

Modern JavaScript uses **Template Literals**.
*   Wrap the text in backticks: `` `...` ``.
*   Put variables or math inside `${...}`.

Example:
```javascript
console.log(`The result is ${10 + 2}`); // Output: The result is 12
```
This is cleaner and prevents mistakes with missing spaces or quotes.

### 4. Order of Operations
Just like in math class, JavaScript follows rules for what to calculate first (often called **Operator Precedence**).
*   Multiplication `*` and Division `/` happen before Addition `+`.
*   Parentheses `( )` can be used to force certain parts of your code to run first.
