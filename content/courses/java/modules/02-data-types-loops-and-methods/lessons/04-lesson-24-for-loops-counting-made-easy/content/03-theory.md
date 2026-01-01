---
type: "THEORY"
title: "For Loop Syntax"
---

for (initialization; condition; update) {
    // Code to repeat
}

Three parts separated by semicolons:

1. INITIALIZATION: Run once at the start
   int i = 0  (Create counter, set starting value)

2. CONDITION: Check before each iteration
   i < 10  (Keep going while this is true)

3. UPDATE: Run after each iteration
   i++  (Change the counter)

Example - Print 0 to 4:

for (int i = 0; i < 5; i++) {
    System.out.println(i);
}

Execution flow:
1. int i = 0  (i is 0)
2. Check: i < 5? YES → run code (print 0)
3. Update: i++ (i is now 1)
4. Check: i < 5? YES → run code (print 1)
5. Update: i++ (i is now 2)
... continues until i is 5
6. Check: i < 5? NO → stop

Output: 0 1 2 3 4