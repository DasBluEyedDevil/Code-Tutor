---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Confusing break and continue**: `break` EXITS the entire loop. `continue` SKIPS to the next iteration. They do very different things!

**Using outside a loop**: `break` and `continue` only work INSIDE loops! Using them outside causes a compile error: "No enclosing loop."

**Break only exits innermost loop**: In nested loops, `break` only exits the current (innermost) loop. To exit multiple levels, use a flag variable or restructure your code.

**Unreachable code after break**: Code after `break` in the same block never runs: `if (x) { break; DoSomething(); }` - DoSomething is dead code!

**Continue placement matters**: Putting `continue` before important code means that code gets skipped. Place your condition checks carefully to avoid logic bugs.