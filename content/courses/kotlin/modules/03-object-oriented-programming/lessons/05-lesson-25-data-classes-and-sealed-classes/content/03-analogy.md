---
type: "ANALOGY"
title: "The Concept"
---


### Why Special Class Types?

**Problem with Regular Classes**:


**Solution with Data Classes**:


---



```kotlin
data class User(val name: String, val age: Int)

val user1 = User("Alice", 25)
val user2 = User("Alice", 25)

println(user1 == user2)  // true (compares data!)
println(user1)           // User(name=Alice, age=25) (readable!)
```
