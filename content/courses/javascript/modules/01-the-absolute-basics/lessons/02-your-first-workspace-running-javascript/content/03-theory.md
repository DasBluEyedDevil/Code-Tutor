---
type: "THEORY"
title: "The Rules of the Playground"
---

Now that you're writing code in a real environment, there are two key concepts to understand about how JavaScript "thinks."

### 1. Top-to-Bottom Execution
Computers are very obedient. They start at line 1 and move to the end. If line 5 depends on something that happens on line 2, you must make sure line 2 stays at the top. This order is called the **Execution Flow**.

### 2. The Overloaded `+` Operator
In JavaScript, the plus sign `+` is a bit of a multitasker.
*   **Addition:** When used between two numbers, it adds them: `5 + 5` becomes `10`.
*   **Concatenation:** When used with text, it joins them: `'Hello' + ' World'` becomes `'Hello World'`.

### 3. Order of Operations
Just like in math class, JavaScript follows rules for what to calculate first (often called **Operator Precedence**).
*   Multiplication `*` and Division `/` happen before Addition `+`.
*   Parentheses `( )` can be used to force certain parts of your code to run first.

Example:
`console.log('Result: ' + (10 + 2));` — Here, the computer adds 10 and 2 first, then joins the result (12) to the text.
