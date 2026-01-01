---
type: "KEY_POINT"
title: "Key Takeaways"
---


**Scope Functions Summary**:


**Decision Tree**:
1. Need result from operation? → let, run, with
2. Need object for chaining? → apply, also
3. Null safety? → let
4. Configuration? → apply
5. Logging/side effects? → also

**Best Practices**:
- Don't overuse—sometimes simple code is clearer
- Choose based on intent, not just brevity
- Use meaningful names when using `it` isn't clear
- Chain thoughtfully—too many levels hurt readability

---

**Congratulations on completing Lesson 3.4!** 🎉

Scope functions are a hallmark of idiomatic Kotlin. Mastering them will make your code more elegant and expressive. Practice using them in your daily coding—they quickly become second nature!



```kotlin
// let: nullable handling, transformation
name?.let { it.uppercase() }

// run: configure + compute result
person.run { age + 1 }

// with: multiple ops on existing object
with(config) { host = "localhost"; port = 8080 }

// apply: object configuration
Person().apply { name = "Alice"; age = 25 }

// also: side effects, logging
data.also { println(it) }
```
