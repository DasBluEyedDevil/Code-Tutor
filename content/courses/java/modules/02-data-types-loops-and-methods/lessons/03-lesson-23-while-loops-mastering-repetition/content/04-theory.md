---
type: "THEORY"
title: "The Infinite Loop Danger"
---

⚠️ CRITICAL MISTAKE: Forgetting to change the condition

WRONG:
int count = 1;
while (count <= 5) {
    System.out.println(count);
    // Forgot count++!
}

This creates an INFINITE LOOP:
- count is always 1
- 1 <= 5 is always true
- Loop runs FOREVER
- Your program hangs/crashes

GOLDEN RULE: Always modify the loop variable inside the loop!

Common patterns:
• Counting up: count++
• Counting down: count--
• Reading input: get next value from user