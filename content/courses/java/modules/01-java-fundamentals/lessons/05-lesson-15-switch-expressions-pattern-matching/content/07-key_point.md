---
type: "KEY_POINT"
title: "Primitive Type Patterns (Preview Feature)"
---

Pattern matching can also work with primitive types, though this is still a PREVIEW feature (JEP 507 in JDK 25, 3rd preview):

// Match on int values with guards (preview feature)
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

NOTE: This is a preview feature as of JDK 25. It may change before being finalized.