---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) A blueprint or template for creating objects**

A class defines the structure (properties) and behavior (methods) that objects will have. It's like a blueprint for a house—the blueprint itself isn't a house, but you can build many houses from it.


---

**Question 2: B) `val` is immutable (read-only), `var` is mutable (read-write)**


---

**Question 3: C) The current instance of the class**

`this` refers to the object itself. It's useful when you need to distinguish between properties and parameters with the same name.


---

**Question 4: B) A special function that initializes objects when they're created**

Constructors set up the initial state of an object.


---

**Question 5: B) `val car = Car()`**

Kotlin doesn't use the `new` keyword like Java. You create objects by calling the class name with parentheses.


---



```kotlin
// ✅ Correct Kotlin syntax
val car = Car("Toyota")

// ❌ Wrong - Java syntax
// Car car = new Car("Toyota")
```
