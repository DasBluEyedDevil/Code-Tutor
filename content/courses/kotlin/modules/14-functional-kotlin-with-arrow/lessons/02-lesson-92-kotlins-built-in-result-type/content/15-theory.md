---
type: "THEORY"
title: "Key Takeaways"
---


- `Result<T>` makes errors explicit in the type system
- Use `runCatching { }` to wrap code that might throw
- `map` transforms success values, `mapCatching` can fail
- `fold` handles both success and failure cases
- `recover` transforms failures back to successes
- Result is best for operations that commonly fail
- For typed errors, see Arrow's `Either` in the next lesson

---

