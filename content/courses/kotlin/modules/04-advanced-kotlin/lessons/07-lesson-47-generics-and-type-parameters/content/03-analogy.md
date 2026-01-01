---
type: "ANALOGY"
title: "The Concept: Why Generics Matter"
---


### The Problem Without Generics

Imagine you need to create a box that can hold different types of items:


### The Solution: Generics


Generics let you write code once and use it with many types, while the compiler ensures everything is type-safe.

---



```kotlin
// ✅ With generics - one class, full type safety
class Box<T>(val value: T)

val intBox = Box(42)           // Box<Int>
val stringBox = Box("Hello")   // Box<String>
val personBox = Box(Person())  // Box<Person>

val str: String = stringBox.value  // Type-safe!
```
