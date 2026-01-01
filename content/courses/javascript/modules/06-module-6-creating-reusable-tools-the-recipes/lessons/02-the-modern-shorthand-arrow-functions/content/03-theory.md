---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Arrow function syntax:

const functionName = (parameters) => { body };
│     │              │            │  │      │
│     │              │            │  │      └─ Function body
│     │              │            │  └──────── Arrow
│     │              └────────────┴─────────── Parameters
│     └──────────────────────────────────────── Name
└────────────────────────────────────────────── const (or let)

Shorthand rules:

1. **No parameters**: Use empty ()
   const greet = () => 'Hello';

2. **One parameter**: Parentheses optional
   const double = x => x * 2;
   const double = (x) => x * 2;  // Also valid

3. **Multiple parameters**: Need parentheses
   const add = (a, b) => a + b;

4. **One-line body**: Can omit { } and return
   const add = (a, b) => a + b;  // Implicit return

5. **Multi-line body**: Need { } and explicit return
   const greet = (name) => {
     let msg = 'Hello, ' + name;
     return msg;
   };

Comparing to traditional functions:

// Traditional
function add(a, b) {
  return a + b;
}

// Arrow (full form)
const add = (a, b) => {
  return a + b;
};

// Arrow (short form)
const add = (a, b) => a + b;

When to use arrow functions:
✓ Short, simple functions
✓ Callback functions (map, filter, etc.)
✓ Modern JavaScript style

When to use traditional functions:
✓ Methods in objects/classes
✓ Need 'this' keyword (advanced)
✓ Personal preference for readability