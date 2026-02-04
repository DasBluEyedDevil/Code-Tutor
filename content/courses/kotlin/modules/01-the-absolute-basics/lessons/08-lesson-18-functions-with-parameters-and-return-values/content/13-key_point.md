---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Function parameters define the contract between caller and function**. Specify exactly what data you need (type and name), and return exactly what you promise. This clarity makes code self-documenting.

**Return types are mandatory unless the function returns `Unit`** (void in Java). Kotlin can infer return types in single-expression functions, but explicit types often improve readability for complex functions.

**Use `return` explicitly in block-body functions**, but omit it in single-expression functions with `=` syntax. This distinction signals whether the function's purpose is computation (expression) or procedure (statement block).
