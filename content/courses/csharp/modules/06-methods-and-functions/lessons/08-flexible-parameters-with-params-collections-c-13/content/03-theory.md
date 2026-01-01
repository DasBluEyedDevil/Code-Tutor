---
type: "THEORY"
title: "Syntax Breakdown"
---

**Before C# 13** - params only worked with arrays:
`void Method(params string[] items)`

**C# 13 Enhancement** - params works with:
- `params IEnumerable<T>` - Any enumerable
- `params ReadOnlySpan<T>` - Stack-allocated, zero-copy
- `params IReadOnlyList<T>` - Indexed access
- `params IReadOnlyCollection<T>` - With count

**Key Benefits:**
1. **Flexibility**: Callers can pass inline items, collection expressions, or existing collections
2. **Performance**: Span<T> avoids heap allocations
3. **Interoperability**: Works with any collection type the caller already has