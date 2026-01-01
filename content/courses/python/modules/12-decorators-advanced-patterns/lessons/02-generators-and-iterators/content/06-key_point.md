---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Generators use yield instead of return** - pause and resume execution
- **Memory efficient** - generate values on demand, don't store all
- **One-time use** - exhausted after iteration, can't reuse
- **Generator expressions:** (x for x in iterable) - like list comp with ()
- **Perfect for large datasets** - process millions of items with minimal memory
- **Pipeline processing** - chain generators for efficient data transformation
- **Infinite sequences possible** - while True: yield x
- **Use next(gen) to get next value** - for loops call this automatically