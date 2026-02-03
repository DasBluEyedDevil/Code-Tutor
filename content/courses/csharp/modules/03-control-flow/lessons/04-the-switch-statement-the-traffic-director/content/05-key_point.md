---
type: "KEY_POINT"
title: "Switch Statements and Expressions"
---

## Key Takeaways

- **Use switch expressions (C# 8+) for value mapping** -- `var grade = score switch { >= 90 => "A", >= 80 => "B", _ => "F" };` is concise and returns a value directly. The `_` discard pattern is your default case.

- **Classic switch statements need `break`** -- forgetting `break` causes a compiler error in C# (unlike C/C++ where it falls through silently). Each `case` must end with `break`, `return`, or `throw`.

- **Switch expressions support pattern matching** -- combine relational patterns (`>= 90`), logical patterns (`or`, `and`, `not`), and type patterns for powerful, readable branching.
