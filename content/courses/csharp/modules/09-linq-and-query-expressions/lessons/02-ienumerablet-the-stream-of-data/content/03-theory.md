---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`IEnumerable<T>`**: Interface representing a sequence of items of type T. Any collection (List, array, etc.) can be treated as IEnumerable<T>.

**`Deferred execution`**: LINQ queries return IEnumerable<T> but don't execute immediately! Execution happens when you iterate (foreach) or materialize (.ToList(), .Count(), etc.).

**`.ToList() / .ToArray()`**: Converts IEnumerable<T> to concrete collection. Forces immediate execution. Use when you need to iterate multiple times or need Count/indexing.

**`yield return`**: Advanced: Creates an IEnumerable<T> by returning items one at a time. Enables lazy evaluation. Each 'yield return' pauses execution until next item needed.