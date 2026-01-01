---
type: "THEORY"
title: "Operators"
---


Operators perform operations on values.

### Arithmetic Operators
Used for basic math:
- `+` (Addition)
- `-` (Subtraction)
- `*` (Multiplication)
- `/` (Division)
- `%` (Modulus - remainder of division)

**Important**: Integer division truncates decimals. `5 / 2` equals `2`, not `2.5`. To get `2.5`, at least one number must be a `Double`: `5.0 / 2`.

### Compound Assignment Operators
Shortcut operators that modify a variable and assign the result back to it:
- `+=`, `-=`, `*=`, `/=`, `%=`

Example: `score += 10` is the same as `score = score + 10`.

### Increment and Decrement Operators
- `++`: Increases value by 1
- `--`: Decreases value by 1

**Prefix vs Postfix**: 
- `++a`: Increments first, then uses the value.
- `a++`: Uses the value first, then increments.

### Comparison Operators
These return a `Boolean` (`true` or `false`):
- `==` (Equal to)
- `!=` (Not equal to)
- `<` (Less than), `>` (Greater than)
- `<=` (Less than or equal), `>=` (Greater than or equal)

**String Comparison**: In Kotlin, `==` compares the *content* of strings. `"abc" == "abc"` is `true`.

### Logical Operators
Combine boolean values:
- `&&` (AND): True if both sides are true.
- `||` (OR): True if at least one side is true.
- `!` (NOT): Flips the value (true becomes false).

**Truth Tables**:
...

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
