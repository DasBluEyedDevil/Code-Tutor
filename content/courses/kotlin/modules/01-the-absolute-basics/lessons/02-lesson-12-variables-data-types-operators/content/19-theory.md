---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) `val` cannot be reassigned, `var` can be reassigned**

```kotlin
val pi = 3.14  // Fixed value
var score = 0  // Can change later
```
`val` = immutable (read-only), `var` = mutable (changeable).

---

**Question 2: C) 3**

Integer division in Kotlin truncates the decimal part:

```kotlin
val result = 10 / 3 // result is 3
```

---

**Question 3: C) Double**

`Double` is the default type for decimal numbers:

```kotlin
val pi = 3.14 // Inferred as Double
```

`Double` has higher precision (64 bits) than `Float` (32 bits).

---

**Question 4: B) 5**

`.length` is a property that returns the number of characters:

```kotlin
val len = "Hello".length // len is 5
```

---

**Question 5: B) 1**

The `%` operator (modulus) returns the remainder after division:
...


Useful for checking if a number is even: `number % 2 == 0`

---



```kotlin
10 % 3  // 1 (10 ÷ 3 = 3 remainder 1)
15 % 4  // 3 (15 ÷ 4 = 3 remainder 3)
20 % 5  // 0 (20 ÷ 5 = 4 remainder 0)
```
