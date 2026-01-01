---
type: "THEORY"
title: "Best Practices for Functions"
---


### 1. Single Responsibility Principle

Each function should do ONE thing well:


### 2. Descriptive Function Names


### 3. Keep Functions Short

Aim for functions that fit on one screen (~20-30 lines max).

### 4. Avoid Side Effects When Possible


---



```kotlin
// ❌ Bad - modifies external state
var total = 0
fun addToTotal(amount: Int) {
    total += amount
}

// ✅ Good - returns new value
fun add(current: Int, amount: Int): Int {
    return current + amount
}
```
