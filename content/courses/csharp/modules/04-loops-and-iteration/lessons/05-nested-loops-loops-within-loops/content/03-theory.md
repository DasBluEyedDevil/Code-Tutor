---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`Outer and Inner loops`**: Outer loop runs first. For EACH outer iteration, inner loop runs COMPLETELY. If outer runs 3 times, inner 4 times = total 3x4=12 inner executions!

**`Loop variables`**: Use different variable names! Outer: i, Inner: j. Or row/col, x/y. Can't reuse same name: `for (int i...) { for (int i...) }` is an ERROR!

**`break in nested loops`**: break only exits CURRENT (innermost) loop! To exit both loops, use a flag variable checked in the outer loop's condition.

**`Performance consideration`**: Nested loops multiply iterations! 100 outer x 100 inner = 10,000 total. For CPU-intensive work on large datasets, consider `Parallel.For` or `Parallel.ForEach` for multi-threaded execution.

**`foreach with nested loops`**: You can nest `foreach` loops just like `for` loops. Great for iterating over collections of collections.