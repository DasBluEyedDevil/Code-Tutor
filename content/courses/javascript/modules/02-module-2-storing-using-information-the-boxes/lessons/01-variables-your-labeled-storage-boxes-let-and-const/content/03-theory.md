---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Let's break down the anatomy of creating a variable:

let age = 25;
│   │   │ │
│   │   │ └─ The value (what goes IN the box)
│   │   └─── The equals sign (means 'store this value')
│   └─────── The variable name (the label on the box)
└─────────── The keyword 'let' (tells the computer to create a box)

Think of it as: let [label] = [contents];

Two keywords for creating variables:

1. let - Use this when the value might change later
   - Example: let score = 0; (score will increase during a game)
   
2. const - Use this when the value will NEVER change
   - Example: const PI = 3.14159; (pi is always pi)
   - If you try to change a const, you'll get an error

Variable naming rules:
- Must start with a letter, $, or _
- Can contain letters, numbers, $, or _ (but not spaces!)
- Cannot be a reserved word (like 'let', 'const', 'if', etc.)
- Case sensitive: 'age' and 'Age' are different variables

Naming conventions (not required, but everyone does it):
- Use camelCase: firstName, not firstname or first_name
- Use descriptive names: userAge, not x or ua
- Start with lowercase: age, not Age (unless it's a special case we'll learn later)