---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) `listOf` is read-only, `mutableListOf` allows adding/removing elements**


---

**Question 2: C) Set**

Sets automatically remove duplicates:


---

**Question 3: C) Both A and B**

Both syntaxes work:


---

**Question 4: C) A new collection with elements matching the condition**

`filter` returns a new collection; it doesn't modify the original:


---

**Question 5: B) `[1, 2, 3]`**

Converting a list to a set removes duplicates:


---



```kotlin
val list = listOf(1, 2, 2, 3)
val set = list.toSet()
println(set)  // [1, 2, 3]
```
