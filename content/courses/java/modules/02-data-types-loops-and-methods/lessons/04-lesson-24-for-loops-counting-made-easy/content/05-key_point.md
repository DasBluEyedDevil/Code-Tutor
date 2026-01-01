---
type: "KEY_POINT"
title: "While vs For: When to Use Each"
---

Use FOR when:
✓ You know how many times to loop
✓ You're counting or iterating
✓ You have a clear start, end, and step

for (int i = 0; i < 100; i++) { ... }  // "Do this 100 times"

Use WHILE when:
✓ You DON'T know how many times to loop
✓ The condition is complex
✓ Not necessarily counting

while (userWantsMore) { ... }  // "Keep going until user says stop"

Rule of thumb:
Counting → for
Condition-based → while