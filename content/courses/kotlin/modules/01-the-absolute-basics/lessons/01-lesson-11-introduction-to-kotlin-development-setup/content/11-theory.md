---
type: "THEORY"
title: "Solution: Personalized Greeting"
---



**Explanation**:
- We use `val` three times to store three pieces of user input
- String interpolation (`$name`, `$color`, `$hobby`) inserts values into our message
- `\n` creates a blank line for better formatting

---



```kotlin
fun main() {
    println("=== Personal Profile ===")

    println("What's your name?")
    val name = readln()

    println("What's your favorite color?")
    val color = readln()

    println("What's your hobby?")
    val hobby = readln()

    println("\n--- Your Profile ---")
    println("Hi $name! Your favorite color is $color and you love $hobby!")
}
```
