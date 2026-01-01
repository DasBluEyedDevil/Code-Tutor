---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Function anatomy:

function functionName(parameter1, parameter2) {
│        │            │                      │
│        │            └──────────────────────┴─ Parameters (inputs)
│        └──────────────────────────────────── Name (you choose)
└───────────────────────────────────────────── 'function' keyword
  // Code to run
  return result;  // Optional: send back a value
}

Key parts:

1. **function keyword** - Tells JavaScript you're creating a function

2. **Name** - What you call the function (use camelCase)
   - Should describe what it does: calculateTotal, getUserName, etc.

3. **Parameters** - Inputs the function needs (inside parentheses)
   - Can have 0, 1, 2, or many parameters
   - Separated by commas
   - Like variables that exist only inside the function

4. **Function body** - The code inside { }
   - The instructions to execute

5. **return statement** - Sends a value back (optional)
   - Function stops executing when it hits return
   - If no return, function returns undefined

Calling a function:
functionName(argument1, argument2);

- Use the function name
- Add parentheses ()
- Pass arguments (values) for parameters

Without () it's just a reference:
greet     // The function itself (reference)
greet()   // Calling the function (execution)