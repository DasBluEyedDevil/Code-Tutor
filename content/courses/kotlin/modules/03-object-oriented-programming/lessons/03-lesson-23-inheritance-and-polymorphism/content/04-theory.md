---
type: "THEORY"
title: "Inheritance Basics"
---


### The `open` Keyword

In Kotlin, classes are **final by default** (cannot be inherited). Use `open` to allow inheritance.


**Why are classes final by default?**
- Safety: Prevents unintended inheritance
- Performance: Compiler optimizations
- Design: Encourages composition over inheritance

### Creating a Subclass

Use a colon (`:`) to inherit from a superclass.


**Key Points**:
- `Dog` and `Cat` inherit from `Animal`
- They inherit `sleep()` (can use it without redefining)
- They override `makeSound()` with their own implementation
- They add unique methods (`fetch()`, `scratch()`)

---



```kotlin
open class Animal(val name: String) {
    open fun makeSound() {
        println("Some generic animal sound")
    }

    fun sleep() {
        println("$name is sleeping...")
    }
}

class Dog(name: String) : Animal(name) {
    override fun makeSound() {
        println("$name says: Woof! Woof!")
    }

    fun fetch() {
        println("$name is fetching the ball!")
    }
}

class Cat(name: String) : Animal(name) {
    override fun makeSound() {
        println("$name says: Meow!")
    }

    fun scratch() {
        println("$name is scratching the furniture!")
    }
}

fun main() {
    val dog = Dog("Buddy")
    dog.makeSound()  // Buddy says: Woof! Woof!
    dog.sleep()      // Buddy is sleeping...
    dog.fetch()      // Buddy is fetching the ball!

    val cat = Cat("Whiskers")
    cat.makeSound()  // Whiskers says: Meow!
    cat.sleep()      // Whiskers is sleeping...
    cat.scratch()    // Whiskers is scratching the furniture!
}
```
