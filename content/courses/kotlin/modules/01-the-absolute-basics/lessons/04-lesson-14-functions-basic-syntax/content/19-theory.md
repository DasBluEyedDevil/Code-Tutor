---
type: "THEORY"
title: "Best Practices for Functions"
---


### 1. Single Responsibility Principle
Each function should do ONE thing well. If a function is named `saveUserAndSendEmail`, it should probably be two separate functions.

### 2. Descriptive Function Names
Use verbs for function names since they represent actions.
- `val x = data()` ❌ (What is data doing?)
- `val x = calculateTax()` ✅ (Clear and obvious)

### 3. Keep Functions Short
Aim for functions that fit on one screen. If a function is too long, it's hard to read and test. Try to break complex functions into smaller helper functions.

### 4. Avoid Side Effects When Possible
A function is "pure" if it only depends on its inputs and doesn't change anything outside itself. Pure functions are much easier to debug.

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
