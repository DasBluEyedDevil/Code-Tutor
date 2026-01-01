---
type: "THEORY"
title: "Summary"
---


Congratulations! You've mastered delegation in Kotlin. Here's what you learned:

✅ **Class Delegation** - Composing objects with `by` keyword
✅ **Property Delegation** - Delegating property accessors
✅ **Lazy Initialization** - Deferring expensive computations
✅ **Observable Properties** - Tracking property changes
✅ **Standard Delegates** - `notNull`, `vetoable`, `observable`
✅ **Custom Delegates** - Creating your own delegation logic

### Key Takeaways

1. **Class delegation** promotes composition over inheritance
2. **`lazy`** initializes properties only on first access
3. **`observable`** notifies on changes, **`vetoable`** can reject changes
4. **Custom delegates** implement `getValue`/`setValue` operators
5. **Map delegation** is great for dynamic property storage

### Next Steps

In the next lesson, we'll explore **Annotations and Reflection** - powerful metaprogramming features that let you inspect and modify code at runtime!

---

**Practice Challenge**: Create a preferences system that saves properties to a file automatically when they change, using custom delegates and observable patterns.

