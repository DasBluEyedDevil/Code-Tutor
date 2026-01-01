---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Anatomy of a for loop:

for (initialization; condition; update) {
     │              │          │
     │              │          └─ What happens after each loop
     │              └──────────── When to keep looping
     └─────────────────────────── Where to start
  // Code to repeat
}

Let's break down: for (let i = 0; i < 5; i++)

1. **Initialization** (let i = 0)
   - Runs ONCE at the very beginning
   - Creates a counter variable (usually named i)
   - Sets its starting value

2. **Condition** (i < 5)
   - Checked BEFORE each loop iteration
   - If true, run the loop body
   - If false, exit the loop

3. **Update** (i++)
   - Runs AFTER each loop iteration
   - Usually increments the counter
   - i++ means 'add 1 to i' (same as i = i + 1)

How it flows:
1. let i = 0           (start)
2. Is i < 5? Yes (0 < 5)  → run loop body
3. i++ → i is now 1
4. Is i < 5? Yes (1 < 5)  → run loop body
5. i++ → i is now 2
6. Is i < 5? Yes (2 < 5)  → run loop body
7. i++ → i is now 3
8. Is i < 5? Yes (3 < 5)  → run loop body
9. i++ → i is now 4
10. Is i < 5? Yes (4 < 5) → run loop body
11. i++ → i is now 5
12. Is i < 5? No (5 is NOT < 5) → EXIT LOOP

Common patterns:
- Count up: i++
- Count down: i--
- Skip by 2: i += 2
- Count by 10s: i += 10