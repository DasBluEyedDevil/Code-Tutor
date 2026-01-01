---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) The single parameter when the lambda has exactly one parameter**


`it` is shorthand provided by Kotlin for single-parameter lambdas.

---

**Question 2: B) Moving the lambda parameter outside parentheses when it's the last parameter**


This makes code more readable and is idiomatic Kotlin.

---

**Question 3: C) `return` in lambda exits enclosing function; in anonymous function exits only that function**


Understanding this prevents subtle bugs.

---

**Question 4: B) A property reference to the length property of String**


`::` creates a reference to an existing member (property or function).

---

**Question 5: C) When the lambda is complex, nested, or the parameter type isn't obvious**


Choose readability over brevity in complex scenarios.

---



```kotlin
// ✅ Simple: 'it' is fine
numbers.filter { it > 10 }

// ❌ Complex: named parameter is clearer
users.filter { it.age > 18 && it.isActive && it.hasRole("admin") }
// Better:
users.filter { user -> user.age > 18 && user.isActive && user.hasRole("admin") }

// ❌ Nested: named parameters prevent confusion
orders.map { it.items.filter { it.price > 100 } }  // Which 'it'?
// Better:
orders.map { order -> order.items.filter { item -> item.price > 100 } }
```
