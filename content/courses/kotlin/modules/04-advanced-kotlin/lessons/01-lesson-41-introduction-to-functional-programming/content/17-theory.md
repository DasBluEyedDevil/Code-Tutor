---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) Functions can be treated as values—stored in variables, passed as parameters, and returned from functions**

First-class functions mean functions are treated like any other value in the language:


This is fundamental to functional programming and enables powerful abstractions.

---

**Question 2: C) A function that takes another function as a parameter or returns a function**

Higher-order functions work with other functions:


This enables generic, reusable code patterns.

---

**Question 3: B) `{ x -> x * 2 }`**

Lambda syntax in Kotlin:


Curly braces delimit the lambda, arrow separates parameters from body.

---

**Question 4: C) `(Int, Int) -> Int`**

Function type syntax: `(ParameterTypes) -> ReturnType`


This describes a function taking two Ints and returning an Int.

---

**Question 5: B) The single parameter when a lambda has exactly one parameter**

`it` is shorthand for the single parameter:


Only works with single-parameter lambdas.

---



```kotlin
// Explicit parameter
numbers.map({ x -> x * 2 })

// Using 'it'
numbers.map({ it * 2 })

// Even shorter
numbers.map { it * 2 }

// But with multiple parameters, must use names:
numbers.fold(0) { acc, n -> acc + n }  // Can't use 'it' here
```
