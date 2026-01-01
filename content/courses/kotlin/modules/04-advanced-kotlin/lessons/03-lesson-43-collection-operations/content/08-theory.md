---
type: "THEORY"
title: "Grouping and Partitioning"
---


### groupBy: Group into Map


### partition: Split into Two Groups


### associate: Create Map


---



```kotlin
val people = listOf("Alice", "Bob", "Charlie")

// Create map from list
val ages = people.associateWith { it.length }
println(ages)  // {Alice=5, Bob=3, Charlie=7}

// Associate with key
val byFirstLetter = people.associateBy { it.first() }
println(byFirstLetter)  // {A=Alice, B=Bob, C=Charlie}

// Full control
val custom = people.associate { name ->
    name.uppercase() to name.length
}
println(custom)  // {ALICE=5, BOB=3, CHARLIE=7}
```
