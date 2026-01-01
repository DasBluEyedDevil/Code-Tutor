---
type: "THEORY"
title: "Default Parameters"
---


Kotlin lets you provide default values for parameters:


**Output**:

---

### Multiple Default Parameters


**Output**:

---

### Named Arguments

You can specify parameter names when calling functions:


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
