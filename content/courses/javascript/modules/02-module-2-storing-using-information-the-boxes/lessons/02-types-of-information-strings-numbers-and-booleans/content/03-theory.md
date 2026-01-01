---
type: "THEORY"
title: "The Core Data Types"
---

JavaScript is a **dynamically typed** language, meaning you don't have to explicitly tell the computer "this variable is a string." It figures it out based on the value you provide.

### 1. Strings (Text)
Strings can be created with single quotes (`'...'`), double quotes (`"..."`), or **backticks** (`` `...` ``).
*   **Template Literals (Backticks):** These are powerful because they allow you to "interpolate" variables directly into the text using `${variableName}` syntax. No more messy `+` signs!

### 2. Numbers
Unlike some languages, JavaScript only has one type for numbers. Whether it's an integer (`5`) or a decimal (`5.5`), it's a `Number`.
*   **NaN (Not a Number):** This is a special value that appears when you try to do math with something that isn't a number (like `"hello" / 2`).

### 3. Booleans
Named after mathematician George Boole, these represent logical states. They are the foundation of all computer decision-making (if something is true, do X; if false, do Y).

### 4. Null vs. Undefined (The "Nothing" Types)
This is a frequent source of confusion:
*   **undefined:** Means the variable exists, but no value has been put in it yet. It's the default state of a new `let` variable.
*   **null:** Is an **intentional** absence of a value. It's like a box that you've purposely marked as "empty."

### 5. Checking Types
You can use the `typeof` keyword to ask JavaScript what type a variable is:
```javascript
console.log(typeof 42);      // "number"
console.log(typeof "Alice"); // "string"
```
