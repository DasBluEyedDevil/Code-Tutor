---
type: "KEY_POINT"
title: "Source Generators Replace Reflection"
---

## Key Takeaways

- **Source generators produce code at compile time** -- instead of discovering types at runtime via reflection (which AOT cannot support), generators analyze your code and emit C# source files during compilation.

- **JSON serialization requires `[JsonSerializable]` for AOT** -- create a `JsonSerializerContext` with `[JsonSerializable(typeof(Product))]` for each type. The generator creates optimized serialization code.

- **Use `partial` classes and methods** -- generators add code to your existing partial classes. You declare the signature; the generator provides the implementation. This is how `System.Text.Json` and logging work with AOT.
