---
type: "THEORY"
title: "Property Delegation Basics"
---


**Property delegation** allows you to reuse property logic by delegating to another object.

**Syntax**: `var/val propertyName: Type by delegate`

### Built-in Delegates

**1. `lazy` (already covered)**

**2. `observable` - Notified on property changes**


**3. `vetoable` - Validate changes before accepting**


---



```kotlin
import kotlin.properties.Delegates

class Settings {
    var fontSize: Int by Delegates.vetoable(12) { property, oldValue, newValue ->
        newValue in 8..24  // Only accept values between 8 and 24
    }
}

fun main() {
    val settings = Settings()

    println(settings.fontSize)  // 12

    settings.fontSize = 16
    println(settings.fontSize)  // 16

    settings.fontSize = 50  // Rejected (out of range)
    println(settings.fontSize)  // Still 16
}
```
