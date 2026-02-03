---
type: "KEY_POINT"
title: "While Loop Essentials"
---

## Key Takeaways

- **Use `while` when the iteration count is unknown** -- keep looping until a condition changes: user input validation, reading a file until end, waiting for a network response.

- **Something inside the loop must change the condition** -- if the condition never becomes false, you get an infinite loop. Always ensure the loop body moves toward termination.

- **Sentinel values signal when to stop** -- a special value like `-1`, `null`, or `"quit"` tells the loop to exit. This is a common pattern for user-driven input loops.
