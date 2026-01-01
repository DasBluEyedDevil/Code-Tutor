---
type: "THEORY"
title: "Default Parameters"
---


Kotlin lets you provide default values for parameters. If the caller doesn't provide a value for that parameter, the default value is used.

```kotlin
fun greet(name: String = "Guest", message: String = "Hello") {
    println("$message, $name!")
}

fun main() {
    greet()                   // Output: Hello, Guest!
    greet("Alice")            // Output: Hello, Alice!
    greet("Bob", "Hi")        // Output: Hi, Bob!
}
```

**Output**:
```text
Hello, Guest!
Hello, Alice!
Hi, Bob!
```

---

### Multiple Default Parameters
You can have multiple parameters with default values. When calling the function, you can choose which ones to override.

---

### Named Arguments

You can specify parameter names when calling functions. This makes the code much more readable and allows you to pass arguments in a different order, especially useful with default parameters.

```kotlin
fun configureGame(
    players: Int = 2,
    difficulty: String = "Normal",
    soundEnabled: Boolean = true
) {
    println("Game configured with $players players, $difficulty difficulty, sound: $soundEnabled")
}

fun main() {
    configureGame(difficulty = "Hard", players = 4) // Order doesn't matter
    configureGame(soundEnabled = false) // Only override one default
}
```

**Benefits of named arguments**:
- Code is more readable
- Order doesn't matter
- Great when functions have many parameters
- Especially useful with default parameters

---



```kotlin
fun makeRecipe(dish: String, cookTime: Int, difficulty: String, serves: Int) {
    println("$dish - Serves $serves")
    println("Cooking time: $cookTime minutes")
    println("Difficulty: $difficulty")
    println()
}

fun main() {
    // Positional arguments (order matters)
    makeRecipe("Pizza", 30, "Easy", 4)

    // Named arguments (order doesn't matter!)
    makeRecipe(
        dish = "Pasta",
        serves = 2,
        difficulty = "Medium",
        cookTime = 20
    )

    // Mix of both
    makeRecipe("Cake", cookTime = 45, difficulty = "Hard", serves = 8)
}
```
