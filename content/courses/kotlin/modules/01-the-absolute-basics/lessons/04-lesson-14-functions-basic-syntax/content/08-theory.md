---
type: "THEORY"
title: "Default Parameters"
---


Kotlin allows you to provide default values for function parameters. This makes your functions more flexible by making certain inputs optional.

```kotlin
fun greet(name: String = "Guest") {
    println("Hello, $name!")
}

fun main() {
    greet()         // Uses default: "Hello, Guest!"
    greet("Alice")  // Overrides default: "Hello, Alice!"
}
```

### Multiple Default Parameters
You can have multiple parameters with default values. When calling the function, you can provide values for some, all, or none of them.

---



```kotlin
fun createUser(
    name: String,
    age: Int = 18,
    country: String = "USA",
    isPremium: Boolean = false
) {
    println("User: $name, Age: $age, Country: $country, Premium: $isPremium")
}

fun main() {
    createUser("Alice")
    // User: Alice, Age: 18, Country: USA, Premium: false

    createUser("Bob", 25)
    // User: Bob, Age: 25, Country: USA, Premium: false

    createUser("Carol", 30, "Canada", true)
    // User: Carol, Age: 30, Country: Canada, Premium: true
}
```
