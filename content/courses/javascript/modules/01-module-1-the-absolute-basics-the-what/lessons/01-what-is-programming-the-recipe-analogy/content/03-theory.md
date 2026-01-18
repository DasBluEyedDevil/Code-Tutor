---
type: "THEORY"
title: "Anatomy of a Statement"
---

A single line of code that tells the computer to do something is called a **statement**. Let's deconstruct the most famous statement in programming:

```javascript
console.log('Hello, World!');
```

### 1. The Object: `console`
The `console` is a built-in "object" in JavaScript that handles input and output. Think of it as the control panel of your application.

### 2. The Method: `.log()`
The `.log` is an action (or "method") that belongs to the console. It tells the console to record or display information.

### 3. The Argument: `('Hello, World!')`
The information you put inside the parentheses is called an **argument**. It's the "input" for the action. In this case, we are providing a **String** (text) wrapped in single quotes.

### 4. The Terminator: `;`
The semicolon is like the period at the end of a sentence. It tells the JavaScript engine that this specific instruction is finished. While JavaScript can often guess where a line ends, explicitly using `;` is a professional habit that prevents bugs.
