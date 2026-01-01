---
type: "THEORY"
title: "Checkpoint Quiz"
---


Test your understanding of generics!

### Question 1: Type Parameter Syntax

What does this function signature mean?

**A)** T can be any type
**B)** T must be Number or its subtype
**C)** T must be exactly Number
**D)** T can be Number or Any

**Answer**: **B** - The `: Number` constraint means T must be Number or any of its subtypes (Int, Double, Float, etc.)

---

### Question 2: Variance

Which statement is correct about variance?

**A)** `out` is used when a type is only consumed
**B)** `in` is used when a type is only produced
**C)** `out` makes a type covariant (producer)
**D)** Invariant types can be used as both covariant and contravariant

**Answer**: **C** - `out` makes a type covariant, meaning it can only be produced/returned, not consumed. `in` makes it contravariant (consumer).

---

### Question 3: Reified Type Parameters

What is required to use reified type parameters?

**A)** The function must be suspend
**B)** The function must be inline
**C)** The class must be open
**D)** The type must be nullable

**Answer**: **B** - Reified type parameters require the function to be `inline` so the compiler can substitute the actual type at call sites.

---

### Question 4: Star Projection

What can you do with a `MutableList<*>`?

**A)** Add and remove elements
**B)** Only add elements
**C)** Only read elements
**D)** Nothing at all

**Answer**: **C** - `MutableList<*>` can only read elements (as `Any?`). You cannot add elements because the compiler doesn't know the actual type.

---

### Question 5: Multiple Constraints

How do you specify multiple type constraints?


**A)** Separate with commas inside angle brackets
**B)** Use `where` clause with commas
**C)** Use multiple angle brackets
**D)** Not possible in Kotlin

**Answer**: **B** - Multiple constraints use the `where` clause: `fun <T> process(item: T) where T : Constraint1, T : Constraint2`

---



```kotlin
fun <T> process(item: T) where T : _____, T : _____
```
