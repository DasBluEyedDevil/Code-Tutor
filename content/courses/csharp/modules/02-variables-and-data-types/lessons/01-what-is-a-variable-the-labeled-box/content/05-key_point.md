---
type: "KEY_POINT"
title: "Variable Declaration and Assignment"
---

## Key Takeaways

- **Declare then assign, or do both at once** -- `string name; name = "Alex";` and `string name = "Alex";` are equivalent. The second form is preferred because it prevents using uninitialized variables.

- **`var` lets C# infer the type** -- `var score = 100;` is the same as `int score = 100;`. Use `var` when the type is obvious from the right side. Always prefer explicit types when the type is not immediately clear.

- **No quotes around variable names when reading them** -- `Console.WriteLine(name)` displays the value inside `name`. Adding quotes (`"name"`) displays the literal text "name" instead.
