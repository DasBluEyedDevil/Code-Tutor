---
type: "ANALOGY"
title: "The Concept"
---


### What is Object-Oriented Programming?

**Object-Oriented Programming (OOP)** is a programming paradigm that organizes code around **objects**—self-contained units that combine data (properties) and behavior (methods).

**Real-World Analogy: A Car**

Think about a car in the real world:

**Properties (Data)**:
- Color: "Red"
- Brand: "Toyota"
- Model: "Camry"
- Current Speed: 0 mph
- Fuel Level: 100%

**Behaviors (Actions)**:
- Start engine
- Accelerate
- Brake
- Turn left/right
- Refuel

A car is an **object** with both data and functionality. OOP lets you model concepts like this in code!

### Why OOP Matters

**Before OOP (Procedural Programming)**:


**Problems**:
- Data and behavior are disconnected
- Hard to manage multiple cars
- No clear organization
- Prone to errors (which car are we accelerating?)

**With OOP**:


**Benefits**:
- ✅ Data and behavior are bundled together
- ✅ Easy to create multiple cars
- ✅ Clear organization and structure
- ✅ Safer and more maintainable

---



```kotlin
class Car(val color: String, val brand: String) {
    var speed = 0

    fun accelerate() {
        speed += 10
    }

    fun brake() {
        speed -= 10
    }
}

val myCar = Car("Red", "Toyota")
myCar.accelerate()
println(myCar.speed)  // 10
```
