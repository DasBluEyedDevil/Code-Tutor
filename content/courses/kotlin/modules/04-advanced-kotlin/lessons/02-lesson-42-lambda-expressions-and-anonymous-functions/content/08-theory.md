---
type: "THEORY"
title: "Member References"
---


References to class members (properties and methods).

### Instance Method References


### Property References


### Constructor References


### Extension Function References


---



```kotlin
fun String.addExclamation(): String = "$this!"

fun Int.isEven(): Boolean = this % 2 == 0

val words = listOf("hello", "world", "kotlin")
val excited = words.map(String::addExclamation)
println(excited)  // [hello!, world!, kotlin!]

val numbers = listOf(1, 2, 3, 4, 5, 6)
val evens = numbers.filter(Int::isEven)
println(evens)  // [2, 4, 6]
```
