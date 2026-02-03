---
type: "KEY_POINT"
title: "Collection Expressions Syntax"
---

## Key Takeaways

- **`[1, 2, 3]` replaces verbose initialization** -- collection expressions (C# 12) work for arrays, lists, spans, and more. The target type determines what gets created.

- **The spread element `..` combines collections** -- `[..existing, newItem]` expands `existing` inline. Use it to concatenate or prepend without manual loops.

- **Empty collections use `[]`** -- cleaner than `new List<int>()` or `Array.Empty<int>()`. Assign to the appropriate type: `List<int> items = [];`.
