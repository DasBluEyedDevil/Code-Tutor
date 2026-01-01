---
type: "THEORY"
title: "Operators"
---


Operators perform operations on values.

### Arithmetic Operators


**Important**: Integer division truncates decimals:

### Compound Assignment Operators

Shortcut operators that modify a variable:


### Increment and Decrement Operators


**Prefix vs Postfix**:

### Comparison Operators

Return `true` or `false`:


**String Comparison**:

### Logical Operators

Combine boolean values:


**Truth Tables**:

| A | B | A && B | A \|\| B | !A |
|---|---|--------|----------|-----|
| T | T | T      | T        | F   |
| T | F | F      | T        | F   |
| F | T | F      | T        | T   |
| F | F | F      | F        | T   |

**Short-Circuit Evaluation**:

---



```kotlin
val a = true
val b = false

// && stops if first is false
if (b && expensiveFunction()) {  // expensiveFunction() NOT called
    // ...
}

// || stops if first is true
if (a || expensiveFunction()) {  // expensiveFunction() NOT called
    // ...
}
```
