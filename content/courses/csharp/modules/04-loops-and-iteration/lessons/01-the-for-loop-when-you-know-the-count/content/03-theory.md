---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`for (initialization; condition; increment)`**: Three parts separated by semicolons: 1) Start point (int i = 0), 2) When to stop (i < 10), 3) How to change i each time (i++)

**`int i = 1`**: INITIALIZATION: Creates a counter variable (usually called i, j, or k). This runs ONCE at the start. i typically starts at 0 or 1.

**`i <= 5`**: CONDITION: Checked BEFORE each loop. If true, the loop runs. If false, the loop stops. Here: 'keep going while i is 5 or less'.

**`i++`**: INCREMENT: Runs AFTER each loop iteration. i++ adds 1 to i. Could also be i--, i += 2, etc. This moves you closer to the end!

**`Loop body { }`**: The code in braces runs each time. If the loop runs 5 times, this code executes 5 times with different values of i!

**`for vs foreach`**: Use `for` when you need the index or exact count control. Use `foreach` when iterating collections in forward order without modifying them - it's cleaner and less error-prone.