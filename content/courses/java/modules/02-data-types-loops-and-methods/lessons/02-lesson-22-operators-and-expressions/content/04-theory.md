---
type: "THEORY"
title: "Order of Operations (PEMDAS)"
---

Just like in math, Java follows order of operations:

Parentheses → Multiplication/Division → Addition/Subtraction

Examples:
int result = 5 + 3 * 2;     // 11 (not 16!)
// Multiply first: 3 * 2 = 6, then add: 5 + 6 = 11

int result = (5 + 3) * 2;   // 16
// Parentheses first: 5 + 3 = 8, then multiply: 8 * 2 = 16

int result = 10 / 2 + 3;    // 8
// Divide first: 10 / 2 = 5, then add: 5 + 3 = 8

When in doubt, use parentheses to be explicit:
int total = (price * quantity) + tax;  // Clear intent