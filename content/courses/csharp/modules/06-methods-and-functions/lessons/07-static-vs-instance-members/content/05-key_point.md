---
type: "KEY_POINT"
title: "Static vs Instance Members"
---

## Key Takeaways

- **Static members belong to the class, not objects** -- `Math.PI` and `Console.WriteLine()` are static. Access them via the class name, not through an instance.

- **Instance members belong to each object** -- `player1.Score` is different from `player2.Score`. Each object gets its own copy of instance fields.

- **Static methods cannot access instance data** -- a static method has no `this` reference. If a method does not need object state, make it static for clarity and slight performance benefit.
