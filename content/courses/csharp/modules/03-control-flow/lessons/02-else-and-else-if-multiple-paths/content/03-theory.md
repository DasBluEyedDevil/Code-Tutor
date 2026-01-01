---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`else { }`**: The 'else' block runs ONLY if the 'if' condition was false. Think of it as the 'otherwise' path. It's optional!

**`else if (condition) { }`**: 'else if' adds MORE conditions to check. It only runs if all previous conditions were false AND this condition is true.

**`Order matters!`**: C# checks from top to bottom. Once a condition is true, it runs that block and SKIPS the rest. score=85 hits the 'B' grade and never checks 'C'!

**`Final else`**: The final 'else' is the 'catch-all' - it runs if NONE of the conditions above were true. It's like the 'default' option.