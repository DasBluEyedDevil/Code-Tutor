---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding the two equality operators:

== (Loose Equality)
- Also called 'abstract equality'
- Converts types before comparing
- Can lead to unexpected results
- Example: '5' == 5 → JavaScript converts '5' to 5, then compares → true

=== (Strict Equality)
- Also called 'strict equality'
- No type conversion
- Both value AND type must match
- Example: '5' === 5 → Different types (string vs number) → false

The same applies to inequality:

!= (Loose Inequality)
- Converts types before comparing
- 5 != '5' → false (they're 'equal' after conversion)

!== (Strict Inequality)  
- No type conversion
- 5 !== '5' → true (different types, so not equal)

Type coercion with ==:
- true == 1 → true
- false == 0 → true
- '' == 0 → true (empty string)
- ' ' == 0 → true (space string)
- [] == 0 → true (empty array)
- null == undefined → true

These are all FALSE with ===!

**Best Practice**: Use === and !== exclusively. The only time to use == is if you specifically want type coercion, which is rare.