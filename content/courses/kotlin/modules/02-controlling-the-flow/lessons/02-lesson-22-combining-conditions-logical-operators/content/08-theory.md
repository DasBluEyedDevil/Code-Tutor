---
type: "THEORY"
title: "Short-Circuit Evaluation"
---


This is an important optimization that logical operators use:

### AND Short-Circuit

With `&&`, if the **first** condition is false, the second condition **isn't even checked** (because the result will be false regardless).


Since `a` is false, `b` is **never evaluated**! This saves time.

**Practical example:**

If the list is empty, `numbers[0]` would crash the program! But short-circuit evaluation saves us—it never checks `numbers[0]` because `numbers.isNotEmpty()` is already false.

### OR Short-Circuit

With `||`, if the **first** condition is true, the second condition **isn't checked** (because the result will be true regardless).


Since `isAdmin` is true, `hasSpecialPermission` is **never checked**!

**Important:** Be careful with side effects! Don't put critical code in conditions that might not execute:

❌ **WRONG:**

---



```kotlin
if (isLoggedIn || performLogin()) {  // performLogin might not run!
    // ...
}
```
