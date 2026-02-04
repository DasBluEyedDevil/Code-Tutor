---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Functions are the building blocks of reusable code**. Write functions to encapsulate logic you'll need multiple times, making your programs more maintainable and testable.

**Single-expression functions with `=` syntax** are a Kotlin idiom for simple functions that immediately return a value. `fun double(x: Int) = x * 2` is more concise than the full block syntax when the function body is trivial.

**Named parameters and default arguments** make function calls self-documenting and reduce the need for multiple overloaded versions. Call `format(name = "Alice", uppercase = true)` instead of remembering positional order.
