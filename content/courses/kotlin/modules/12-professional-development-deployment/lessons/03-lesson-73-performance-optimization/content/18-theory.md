---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) Profile to find bottlenecks**

Always measure first:
1. Profile with Android Studio Profiler
2. Find the actual bottleneck
3. Optimize that specific code
4. Measure again to verify

90% of time is in 10% of code - find that 10%!

---

**Question 2: C) Dispatchers.Default**


---

**Question 3: B) Use stable parameters and keys in LazyColumn**


---

**Question 4: B) Making N additional queries in a loop**


---

**Question 5: B) Only recalculates when dependencies change**


---



```kotlin
val filteredItems by remember {
    derivedStateOf {
        items.filter { it.price > 100 }
    }
}
// Only recalculates when 'items' changes
```
