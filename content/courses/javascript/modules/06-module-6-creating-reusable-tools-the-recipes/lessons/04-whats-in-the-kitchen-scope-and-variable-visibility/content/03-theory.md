---
type: "THEORY"
title: "The Hierarchy of Scope"
---

JavaScript uses **Lexical Scoping**, which means that the "inner" parts of your code have access to the "outer" parts, but not the other way around.

### 1. Global Scope
Variables declared outside of any function or block. 
*   **Pros:** Easy to access from anywhere.
*   **Cons:** Can be changed by any part of the program, leading to "spaghetti code" and hard-to-find bugs.

### 2. Function Scope
Each function creates its own "bubble." Any variable declared inside that bubble (including the function's parameters) is invisible to the outside world.

### 3. Block Scope
Introduced with `let` and `const` in ES6. A block is anything inside curly braces `{ }`, such as an `if` statement or a `for` loop. 
*(Note: Older `var` variables do NOT have block scope, which is one reason we don't use them!)*

### 4. Shadowing
When you declare a variable with the same name as one in an outer scope, the inner variable "wins" while inside that scope. This is useful for avoiding name collisions, but it can be confusing if overused.

### 5. Scope Chain
When you use a variable, JavaScript looks for it in this order:
1.  Current local scope.
2.  Next outer scope.
3.  ...repeat until...
4.  Global scope.
If it still isn't found, you get a `ReferenceError`.
