---
type: "WARNING"
title: "Common Pitfalls"
---

**Forgetting 'return':** If your method has a return type (not void), every code path MUST return a value. `public int Add(int a, int b) { a + b; }` is WRONG - needs `return a + b;`

**Missing parentheses:** `player.Attack` is a reference to the method. `player.Attack()` actually CALLS it. Parentheses are REQUIRED to execute a method!

**Return type mismatch:** If method returns int, you can't `return "hello"`. The return value type MUST match the method signature.

**Ignoring return values:** `calc.Add(5, 3);` works but throws away the result! Use `int sum = calc.Add(5, 3);` to capture returned values.