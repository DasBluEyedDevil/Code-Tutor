---
type: "THEORY"
title: "Anatomy of an 'if' Statement"
---

The `if` statement is the most fundamental building block of logic. Here is how it breaks down:

```javascript
if (condition) {
    // Code to run if condition is true
}
```

### 1. The Condition `( ... )`
The part inside the parentheses must eventually boil down to a **Boolean** (`true` or `false`). 
*   It can be a simple variable: `if (isLoggedIn)`
*   It can be a comparison: `if (age > 18)`

### 2. The Code Block `{ ... }`
The curly braces `{ }` define the "scope" of the decision. Everything inside those braces belongs to that `if` statement. If the condition is false, the computer jumps directly from the opening `{` to the closing `}` and continues with the rest of the program.

### 3. Automatic "Truthiness"
JavaScript is flexible. You don't always have to use a boolean.
*   The number `0` is treated as `false`.
*   Any other number (like `1` or `-50`) is treated as `true`.
*   An empty string `""` is `false`, while a string with text `"Hello"` is `true`.

We call this **Truthy** and **Falsy**.

### 4. Code Flow
The most important thing to remember is that an `if` statement is **additive**. It doesn't stop the rest of the code from running; it just decides whether to run an *extra* piece of code before moving on.
