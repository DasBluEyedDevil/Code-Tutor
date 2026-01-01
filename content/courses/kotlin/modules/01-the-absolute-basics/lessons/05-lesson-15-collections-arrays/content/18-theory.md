---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) `listOf` is read-only, `mutableListOf` allows adding/removing elements**

```kotlin
val list = listOf(1, 2)         // Fixed
val mutable = mutableListOf(1) // Can change
mutable.add(2)
```

---

**Question 2: C) Set**

Sets automatically remove duplicates:

```kotlin
val unique = setOf(1, 1, 2) // [1, 2]
```

---

**Question 3: C) Both A and B**

Both syntaxes work:

```kotlin
val map = mapOf("A" to 1)
println(map["A"])
println(map.get("A"))
```

---

**Question 4: C) A new collection with elements matching the condition**

`filter` returns a new collection; it doesn't modify the original:

```kotlin
val numbers = listOf(1, 2, 3)
val evens = numbers.filter { it % 2 == 0 } // [2]
```

---

**Question 5: B) `[1, 2, 3]`**

Converting a list to a set removes duplicates.

Converting a list to a set removes duplicates:


---



```kotlin
val list = listOf(1, 2, 2, 3)
val set = list.toSet()
println(set)  // [1, 2, 3]
```
