---
type: "THEORY"
title: "Solution 1"
---



---



```kotlin
fun main() {
    val secretNumber = (1..10).random()
    var guess: Int

    do {
        println("Guess a number between 1 and 10:")
        guess = readln().toInt()

        when {
            guess < secretNumber -> println("Too low!")
            guess > secretNumber -> println("Too high!")
            else -> println("Correct!")
        }
    } while (guess != secretNumber)
}
```
