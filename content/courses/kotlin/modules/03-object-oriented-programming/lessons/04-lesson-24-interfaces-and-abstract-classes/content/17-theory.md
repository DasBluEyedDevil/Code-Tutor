---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) A class can implement multiple interfaces but inherit from only one abstract class**

This is one of the key differences and a major reason to use interfaces.


---

**Question 2: B) No, never**

Interfaces can't have backing fields. Properties must either be abstract or have custom getters.


---

**Question 3: D) Yes, since Kotlin 1.0**

Kotlin interfaces can have default method implementations from the start.


---

**Question 4: D) Both B and C**

Use interfaces when you want to define capabilities ("can-do") without shared state, and when you need multiple inheritance.


---

**Question 5: B) It must be overridden by implementing classes (unless it has a default getter)**

Interface properties without default getters must be overridden.


---



```kotlin
interface Vehicle {
    val speed: Int  // Must override
    val type: String
        get() = "Generic"  // Has default, override optional
}
```
