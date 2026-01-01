---
type: "KEY_POINT"
title: "Pattern Matching for instanceof"
---

Traditional instanceof checking is repetitive:

// Old way:
if (obj instanceof String) {
    String s = (String) obj;  // Repetitive cast!
    System.out.println(s.length());
}

// New way with pattern matching:
if (obj instanceof String s) {
    // 's' is already a String - no cast needed!
    System.out.println(s.length());
}

The pattern 'String s' both:
1. Tests if obj is a String
2. Creates variable s with the String type

More examples:

// With negation
if (!(obj instanceof String s)) {
    return;  // obj is not a String
}
// Here 's' is in scope and is a String
System.out.println(s.toUpperCase());

// In complex conditions
if (obj instanceof String s && s.length() > 5) {
    System.out.println("Long string: " + s);
}