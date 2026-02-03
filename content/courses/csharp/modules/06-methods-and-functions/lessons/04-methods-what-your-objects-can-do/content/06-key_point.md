---
type: "KEY_POINT"
title: "Method Design Basics"
---

## Key Takeaways

- **Return type declares what comes back** -- `int Add(int a, int b)` returns an `int`. `void DisplayResult()` returns nothing. The `return` keyword sends the value and exits the method.

- **Method overloading uses different parameter lists** -- `Add(int, int)` and `Add(double, double)` can coexist. C# picks the correct version based on the arguments you pass.

- **Keep methods focused** -- each method should do one thing well. If a method name needs "And" in it (`ValidateAndSave`), it probably should be two methods.
