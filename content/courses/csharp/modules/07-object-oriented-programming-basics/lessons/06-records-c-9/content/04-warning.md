---
type: "WARNING"
title: "Record Struct and Important Gotchas"
---

## Record Struct (C# 10+)

For performance-critical code, use `record struct` - a VALUE type with record features:

```csharp
// Reference type (heap allocated)
public record PersonRecord(string Name, int Age);

// Value type (stack allocated, better for small data)
public record struct Point(int X, int Y);

// Immutable value type (best of both worlds!)
public readonly record struct ImmutablePoint(int X, int Y);
```

**Key differences:**
- `record` / `record class`: Reference type, properties are init-only (immutable)
- `record struct`: Value type, properties are MUTABLE by default!
- `readonly record struct`: Value type, properties are immutable

**With expressions work on all record types:**
```csharp
var p1 = new Point(10, 20);
var p2 = p1 with { X = 30 };  // Works! p2 = Point { X = 30, Y = 20 }
```

**When NOT to use records:**
- Entity Framework entities (EF needs reference equality)
- Classes with complex mutable state
- When you need inheritance hierarchies (records support inheritance but it gets complex)

**Use records for:** DTOs, configuration, API responses, value objects, anything that represents 'data' rather than 'behavior'.