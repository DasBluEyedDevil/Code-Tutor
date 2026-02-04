---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Always prefer `val` over `var`** unless you have a specific reason to change a variable's value. Immutability reduces bugs and makes your code easier to reason about—this is one of Kotlin's core design principles.

**Type inference is powerful, but explicit types improve readability** when the type isn't obvious. Use `val name: String = "Alice"` when it helps, but `val count = 42` is perfectly clear on its own.

**Kotlin's type safety catches errors at compile-time** rather than runtime. The compiler won't let you add a String to an Int, preventing entire categories of bugs before your program ever runs.
