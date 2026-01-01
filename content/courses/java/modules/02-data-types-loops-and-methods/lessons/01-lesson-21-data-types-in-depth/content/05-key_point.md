---
type: "KEY_POINT"
title: "Choosing the Right Type"
---

How to decide which type to use:

Ask yourself:
1. Is it a number? → int or double
   - Whole number? → int
   - Needs decimals? → double

2. Is it text? → String
   - Even if it looks like a number ("123") but you're not doing math

3. Is it yes/no, on/off, true/false? → boolean

Examples:
✓ int numberOfStudents = 30;  // Counting people
✓ double temperature = 98.6;  // Needs precision
✓ String username = "alice2024";  // Text identifier
✓ boolean isValid = true;  // Yes/no flag

✗ double numberOfStudents = 30.0;  // Wasteful (don't need decimals)
✗ int price = 20;  // Risky (what about $20.50?)
✗ String isValid = "true";  // Wrong type (should be boolean)