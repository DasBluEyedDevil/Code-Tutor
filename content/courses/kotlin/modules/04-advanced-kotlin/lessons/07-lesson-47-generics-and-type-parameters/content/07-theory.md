---
type: "THEORY"
title: "Variance: In, Out, and Invariant"
---


Variance controls how generic types relate to each other based on their type parameters.

### The Problem: Invariance

By default, generic types are **invariant**:


### Covariance: `out` Keyword

Use `out` when a type is only produced (output), never consumed:


**Rule**: If a generic class only returns `T` (never accepts it), mark it `out T`.

### Contravariance: `in` Keyword

Use `in` when a type is only consumed (input), never produced:


**Rule**: If a generic class only accepts `T` (never returns it), mark it `in T`.

### Real-World Example: List vs MutableList


### Variance Summary

| Variance | Keyword | Usage | Example |
|----------|---------|-------|---------|
| **Covariant** | `out T` | Type is only produced | `List<out T>`, `Producer<out T>` |
| **Contravariant** | `in T` | Type is only consumed | `Comparable<in T>`, `Consumer<in T>` |
| **Invariant** | `T` | Type is both produced and consumed | `MutableList<T>`, `Box<T>` |

---



```kotlin
fun main() {
    // List<T> is covariant (out T)
    val dogs: List<Dog> = listOf(Dog(), Dog())
    val animals: List<Animal> = dogs  // ✅ Works!

    // MutableList<T> is invariant (can't be covariant or contravariant)
    val mutableDogs: MutableList<Dog> = mutableListOf(Dog())
    // val mutableAnimals: MutableList<Animal> = mutableDogs  // ❌ Error!
    // Why? Because MutableList both produces and consumes
}
```
