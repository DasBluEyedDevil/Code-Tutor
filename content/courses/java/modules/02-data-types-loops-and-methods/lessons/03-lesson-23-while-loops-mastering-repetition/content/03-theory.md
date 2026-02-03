---
type: "THEORY"
title: "While Loop Syntax"
---

The basic structure:

while (condition) {
    // Code to repeat
}

Real example - Count to 5:

int count = 1;
while (count <= 5) {
    IO.println(count);
    count++;  // CRITICAL: Change the condition!
}

How it works:
1. Check: Is count <= 5? (1 <= 5? YES)
2. Run code: Print 1, then count becomes 2
3. Check again: Is count <= 5? (2 <= 5? YES)
4. Run code: Print 2, then count becomes 3
5. Repeat until count is 6
6. Check: Is count <= 5? (6 <= 5? NO)
7. STOP - exit the loop

Output: 1 2 3 4 5