---
type: "THEORY"
title: "Implementing Multiple Interfaces"
---


Unlike classes (single inheritance), you can implement multiple interfaces!

```kotlin
interface Flyable {
    fun fly() {
        println("Flying through the sky")
    }
}

interface Swimmable {
    fun swim() {
        println("Swimming in the water")
    }
}

interface Walkable {
    fun walk() {
        println("Walking on land")
    }
}

// Duck can fly, swim, and walk!
class Duck : Flyable, Swimmable, Walkable {
    override fun fly() = println("Duck is flying")
    override fun swim() = println("Duck is swimming")
    override fun walk() = println("Duck is walking")
}

// Fish can only swim
class Fish : Swimmable {
    override fun swim() = println("Fish is swimming")
}

// Bird can fly and walk
class Bird : Flyable, Walkable {
    override fun fly() = println("Bird is flying")
    override fun walk() = println("Bird is walking")
}

fun main() {
    val duck = Duck()
    duck.fly()
    duck.swim()
    duck.walk()

    println()

    val fish = Fish()
    fish.swim()

    println()

    val bird = Bird()
    bird.fly()
    bird.walk()
}
```

**Output**:
```text
Duck is flying
Duck is swimming
Duck is walking

Fish is swimming

Bird is flying
Bird is walking
```

### Why Multiple Interfaces are Powerful

- **Flexible composition**: Combine capabilities as needed
- **No diamond problem**: Each interface provides independent behavior
- **Polymorphism**: Treat objects by their capabilities, not their class

```kotlin
fun makeItFly(flyer: Flyable) {
    flyer.fly()
}

makeItFly(duck)  // Works!
makeItFly(bird)  // Works!
// makeItFly(fish)  // Error: Fish doesn't implement Flyable
```

---
