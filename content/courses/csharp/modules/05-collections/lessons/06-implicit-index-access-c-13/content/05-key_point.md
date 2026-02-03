---
type: "KEY_POINT"
title: "From-End Indexing with ^"
---

## Key Takeaways

- **`^1` is the last element, `^2` is second-to-last** -- the `^` operator counts from the end. It is equivalent to `array[array.Length - n]` but much more readable.

- **C# 13 allows `^` in object initializers** -- you can now write `new int[3] { [^1] = 99 }` to set the last element during initialization. Previously this was a compiler error.

- **The `Index` type is reusable** -- `Index last = ^1;` stores a from-end index in a variable. Use it with any collection that has a `Length` or `Count` property.
