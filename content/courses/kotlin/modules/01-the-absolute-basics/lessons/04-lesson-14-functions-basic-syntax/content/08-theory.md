---
type: "THEORY"
title: "Default Parameters"
---


Provide default values for parameters:


### Multiple Default Parameters


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
