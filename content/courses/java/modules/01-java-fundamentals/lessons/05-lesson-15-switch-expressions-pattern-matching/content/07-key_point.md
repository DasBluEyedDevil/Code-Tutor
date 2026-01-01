---
type: "KEY_POINT"
title: "Primitive Type Patterns (Java 23+)"
---

Pattern matching now works with primitive types:

// Match on int values with guards
String describe(int value) {
    return switch (value) {
        case 0 -> "zero";
        case int i when i > 0 -> "positive: " + i;
        case int i -> "negative: " + i;
    };
}

Key points:
- Declare type: case int i or case long l
- Add guards with when clause
- Must cover all cases (final case with no guard)
- Works with all primitives: int, long, double, etc.