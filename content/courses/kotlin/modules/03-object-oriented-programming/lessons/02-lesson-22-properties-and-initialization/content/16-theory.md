---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) Custom getters compute the value each time the property is accessed**

Custom getters don't store a value—they compute it when accessed.


---

**Question 2: B) For properties that will be initialized later, before first use**

`lateinit` is perfect for dependency injection, Android views, or any scenario where you can't initialize in the constructor but will initialize before use.


**Note**: Can't be used with primitive types (Int, Double, etc.) or nullable types.

---

**Question 3: B) The backing field (actual storage) of the property**

`field` lets you access the actual stored value in custom accessors.


Without `field`, you'd get infinite recursion!

---

**Question 4: B) Expensive operations are deferred until needed**

Lazy initialization improves performance by delaying expensive operations until they're actually needed.


---

**Question 5: C) It throws `UninitializedPropertyAccessException`**

Always initialize `lateinit` properties before using them, or check with `::property.isInitialized`.


---



```kotlin
lateinit var name: String

// println(name)  // ❌ UninitializedPropertyAccessException

if (::name.isInitialized) {
    println(name)  // ✅ Safe
}
```
