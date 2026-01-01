---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`using System.Linq;`**: MUST include this namespace to use LINQ! Without it, methods like .Where(), .Select() won't be available.

**`collection.Where(x => condition)`**: Where() filters items. 'x => condition' is a LAMBDA expression (more next lesson!). Returns items where condition is true.

**`Lambda: x => x > 5`**: 'x' is the parameter (each item). '=>' means 'goes to'. 'x > 5' is the expression. Read as: 'each item x goes to x > 5'.

**`LINQ is lazy`**: LINQ doesn't execute immediately! 'var result = list.Where(...)' just creates a QUERY. Execution happens when you iterate (foreach) or call .ToList().