---
type: "THEORY"
title: "Understanding Ranges"
---


Kotlin has several ways to create ranges:

### Inclusive Range (..)


Both 1 and 10 are **included**.

### Exclusive Range (until / ..<)


10 is **excluded** (stops before 10).

You can also write this using the **`..<` operator** (modern Kotlin syntax):

```kotlin
for (i in 0..<10) {
    // Same as 0 until 10
}
```

Both `until` and `..<` produce the same result. The `..<` operator is the newer, more concise form.

**Use case:** Perfect for array/list indices which start at 0:

### Reverse Range (downTo)


Counts **backwards** from 10 to 1.

### Step Ranges (step)


Increments by 2 instead of 1 (counts even numbers).

**Combined example:**

### Range Quick Reference


---



```kotlin
1..10       // 1, 2, 3, ..., 10 (inclusive)
1 until 10  // 1, 2, 3, ..., 9 (exclusive end)
1..<10      // 1, 2, 3, ..., 9 (same as until, modern syntax)
10 downTo 1 // 10, 9, 8, ..., 1 (reverse)
1..10 step 2 // 1, 3, 5, 7, 9 (every 2nd)
```
