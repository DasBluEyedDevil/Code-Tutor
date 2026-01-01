---
type: "THEORY"
title: "Pattern Matching in Switch"
---

Pattern matching in switch expressions:

// Type patterns in switch
String describe(Object obj) {
    return switch (obj) {
        case Integer i -> "Integer: " + i;
        case Long l -> "Long: " + l;
        case Double d -> "Double: " + d;
        case String s -> "String of length " + s.length();
        case null -> "It's null!";
        default -> "Unknown type";
    };
}

// With guards (when clause)
String categorize(Object obj) {
    return switch (obj) {
        case Integer i when i < 0 -> "Negative number";
        case Integer i when i == 0 -> "Zero";
        case Integer i -> "Positive number: " + i;
        case String s when s.isEmpty() -> "Empty string";
        case String s -> "String: " + s;
        case null -> "null value";
        default -> "Something else";
    };
}

Key features:
- Type patterns: case Integer i -> ...
- Guards: case Integer i when i > 0 -> ...
- Null handling: case null -> ...
- Record patterns: case Point(int x, int y) -> ...