---
type: "THEORY"
title: "Common Map Operations"
---


### Checking Contents


### Filtering Maps


**Output:**

### Map Transformations


---



```kotlin
fun main() {
    val numbers = mapOf("one" to 1, "two" to 2, "three" to 3)

    // Transform values only
    val doubled = numbers.mapValues { it.value * 2 }
    println(doubled)  // {one=2, two=4, three=6}

    // Transform keys only
    val upperKeys = numbers.mapKeys { it.key.uppercase() }
    println(upperKeys)  // {ONE=1, TWO=2, THREE=3}

    // Convert to list of pairs
    val pairs = numbers.toList()
    println(pairs)  // [(one, 1), (two, 2), (three, 3)]
}
```
