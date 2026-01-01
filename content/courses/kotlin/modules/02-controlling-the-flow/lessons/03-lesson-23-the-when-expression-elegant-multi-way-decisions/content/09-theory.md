---
type: "THEORY"
title: "When with Type Checking (Smart Casts)"
---


Kotlin's `when` can check types and automatically cast variables:


**Output:**

**Note:** After `is String`, Kotlin knows `value` is a String and lets you use `.length` without casting!

---



```kotlin
Text: 'Hello' (length: 5)
Number: 42 (doubled: 84)
Boolean: true (opposite: false)
List with 3 items
```
