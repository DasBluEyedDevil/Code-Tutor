---
type: "KEY_POINT"
title: "IEnumerable<T> and Deferred Execution"
---

## Key Takeaways

- **`IEnumerable<T>` represents a sequence you can iterate** -- arrays, lists, and LINQ queries all implement it. Use it as parameter types for maximum flexibility.

- **Deferred execution means queries run only when consumed** -- `.Where()` and `.Select()` build a query plan. `.ToList()`, `.Count()`, or `foreach` triggers actual execution.

- **`yield return` creates lazy sequences** -- each item is produced on demand. The method pauses after each yield and resumes when the next item is requested. Ideal for large or infinite sequences.
