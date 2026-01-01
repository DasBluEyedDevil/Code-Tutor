---
type: "KEY_POINT"
title: "Dual Syntax: Pattern Matching vs Traditional instanceof"
---

When checking object types, you'll see TWO styles:

MODERN SYNTAX (Java 16+):
if (obj instanceof String s) {
    println(s.length());  // s is already a String!
}

Use this when: Type check AND use the value together.

TRADITIONAL SYNTAX (Java 8-15):
if (obj instanceof String) {
    String s = (String) obj;  // Must cast separately
    System.out.println(s.length());
}

Use this when: Working with Java 8-15 LTS or older codebases.

WHY THE DIFFERENCE?
- Pattern matching (Java 16+) combines the type check AND creates the variable in one step
- Traditional syntax requires a separate cast, which is repetitive and error-prone
- Both achieve the same result, but pattern matching is cleaner

NOTE: Basic if/else works the same in all Java versions. Pattern matching is an enhancement for type checking specifically.