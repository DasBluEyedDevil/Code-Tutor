---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`^n` (from-end operator)**: The caret ^ creates an index counting from the end. ^1 is the last element, ^2 is second-to-last, etc. It's equivalent to `array[array.Length - n]`.

**`[^1] = value` in initializers**: C# 13 allows using ^ indexes inside object initializers. Previously, this caused a compiler error - now it works!

**Requires Length property**: The ^ operator needs to know the collection's length. The type must have a Length or Count property for this to work.

**Index type**: ^n actually creates an `Index` struct. You can store it in a variable: `Index last = ^1;` and use it later: `array[last]`.

**Mixing forward and backward**: You can combine regular indexes [0], [1] with from-end indexes [^1], [^2] in the same initializer - great for setting headers and footers!