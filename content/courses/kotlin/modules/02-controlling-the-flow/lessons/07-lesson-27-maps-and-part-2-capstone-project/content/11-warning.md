---
type: "WARNING"
title: "Common Pitfalls and Best Practices"
---


### Pitfall 1: Modifying While Iterating

❌ **Dangerous:**

✅ **Safe:**

### Pitfall 2: Null Values from Missing Keys

❌ **Can crash:**

✅ **Safe:**

### Best Practice 1: Use Appropriate Map Type


### Best Practice 2: Descriptive Key Names


### Best Practice 3: Check Before Access


---



```kotlin
// ✅ Safe pattern
if ("Alice" in contacts) {
    val contact = contacts["Alice"]!!
    // Use contact
} else {
    println("Contact not found")
}
```
