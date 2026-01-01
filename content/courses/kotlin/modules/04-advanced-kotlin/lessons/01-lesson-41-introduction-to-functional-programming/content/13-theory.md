---
type: "THEORY"
title: "Solution 2: Custom List Filter"
---



**Explanation**:
- `filterList` iterates through the list
- For each item, it calls the predicate function
- If predicate returns true, item is included in result
- Different predicates give different filtered results

---



```kotlin
fun filterList(list: List<Int>, predicate: (Int) -> Boolean): List<Int> {
    val result = mutableListOf<Int>()
    for (item in list) {
        if (predicate(item)) {
            result.add(item)
        }
    }
    return result
}

fun main() {
    val numbers = listOf(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25)

    // Filter even numbers
    val evens = filterList(numbers) { it % 2 == 0 }
    println("Even numbers: $evens")  // [2, 4, 6, 8, 10, 20]

    // Filter numbers greater than 10
    val bigNumbers = filterList(numbers) { it > 10 }
    println("Numbers > 10: $bigNumbers")  // [15, 20, 25]

    // Filter numbers divisible by 5
    val divisibleBy5 = filterList(numbers) { it % 5 == 0 }
    println("Divisible by 5: $divisibleBy5")  // [5, 10, 15, 20, 25]

    // Filter numbers in range 3..7
    val inRange = filterList(numbers) { it in 3..7 }
    println("In range 3-7: $inRange")  // [3, 4, 5, 6, 7]
}
```
