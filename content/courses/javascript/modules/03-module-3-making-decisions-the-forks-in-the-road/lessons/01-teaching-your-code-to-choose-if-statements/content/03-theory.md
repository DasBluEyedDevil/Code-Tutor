---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Let's break down an if statement:

if (condition) {
│  │          │ │
│  │          │ └─ The code to run (the 'action')
│  │          └─── Closing parenthesis
│  └─────────────── The condition (must be true/false)
└────────────────── The 'if' keyword

Key points:

1. if - This keyword says "I'm about to check a condition"

2. (condition) - This must be something that evaluates to true or false
   - temperature > 70 → either true or false
   - isRaining → already a boolean (true or false)
   - !hasKeys → the ! flips the boolean (false becomes true)

3. { } - Curly braces contain the code that runs IF the condition is true
   - If the condition is false, everything inside { } is skipped
   - You can have multiple lines of code inside { }

4. Code outside the if statement runs no matter what

The ! operator (NOT):
- !true → false
- !false → true
- It flips/inverts the boolean value