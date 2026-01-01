---
type: "THEORY"
title: "What's Next?"
---


Congratulations on completing **Part 1: Absolute Basics**!

You now have a solid foundation in Kotlin fundamentals. You can:
- Write and run Kotlin programs
- Work with variables and different data types
- Create and use functions effectively
- Get user input and display output
- Build complete, working applications

### In Part 2: Object-Oriented Programming, you'll learn:

**Classes & Objects**: Creating custom data types

**Inheritance**: Building on existing code

**Interfaces**: Defining contracts for classes
**Data Classes**: Special classes for holding data
**Object Declarations**: Singletons and companions
**And much more!**

This is where Kotlin really starts to shine!

---



```kotlin
open class Animal {
    open fun makeSound() { }
}

class Dog : Animal() {
    override fun makeSound() {
        println("Woof!")
    }
}
```
