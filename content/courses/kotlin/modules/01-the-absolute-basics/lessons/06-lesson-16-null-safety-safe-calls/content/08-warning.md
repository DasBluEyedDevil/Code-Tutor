---
type: "WARNING"
title: "⚠️ The Preferred Approach: ?. + ?: and let"
---


**IMPORTANT: Before learning about !!, understand the CORRECT patterns:**

In professional Kotlin code, you should ALMOST ALWAYS use these patterns:

**Pattern 1: Safe Call + Elvis (Most Common)**
```kotlin
val length = name?.length ?: 0  // Safe, provides default
val upper = text?.uppercase() ?: ""  // Never crashes
```

**Pattern 2: let for Complex Operations**
```kotlin
user?.let { u ->
    sendEmail(u.email)
    logActivity(u.id)
}  // Block only runs if user is not null
```

**Pattern 3: Explicit Null Check (Smart Cast)**
```kotlin
if (name != null) {
    println(name.length)  // Compiler knows it's not null here
}
```

**These three patterns handle 99% of nullable scenarios safely.**

---

