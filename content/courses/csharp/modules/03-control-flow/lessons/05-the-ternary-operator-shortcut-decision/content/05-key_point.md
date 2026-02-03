---
type: "KEY_POINT"
title: "Ternary Operator Usage"
---

## Key Takeaways

- **The ternary operator returns a value** -- `string status = age >= 18 ? "adult" : "minor";` assigns based on condition. Use it for simple value selection, not for side effects.

- **Keep ternaries simple** -- if you are nesting ternaries or the expressions are complex, switch to if-else. Readability always beats brevity.

- **Use ternaries for inline decisions** -- they work great inside string interpolation: `$"Status: {(isOnline ? "Online" : "Offline")}"` and method arguments where a full if-else would be verbose.
