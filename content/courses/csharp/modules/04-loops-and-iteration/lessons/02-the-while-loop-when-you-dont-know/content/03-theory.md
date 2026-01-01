---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`while (condition)`**: The loop checks the condition BEFORE each iteration. If true, the loop body runs. If false, the loop stops and continues after the braces.

**`Condition checked FIRST`**: Unlike do-while (coming soon!), while checks BEFORE running. If the condition starts as false, the loop body NEVER runs, not even once!

**`Must change something!`**: CRITICAL: Something inside the loop MUST eventually make the condition false! Otherwise you get an INFINITE LOOP and your program hangs forever.

**`while vs for`**: Use FOR when you know the count (repeat 10 times). Use WHILE when you're checking a condition (repeat until user enters valid input).

**`Sentinel values`**: A common pattern uses a special value (like -1 or null) to signal when to stop. The while loop keeps processing until it encounters this sentinel.