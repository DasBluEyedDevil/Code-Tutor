---
type: "THEORY"
title: "Phase 3: Delegation Patterns (45 minutes)"
---


### Lazy Task Resource


### Observable Task State


### Validated Configuration


---



```kotlin
class ValidatedProperty<T>(
    private var value: T,
    private val validator: (T) -> Boolean,
    private val errorMessage: (T) -> String
) {
    operator fun getValue(thisRef: Any?, property: KProperty<*>): T {
        return value
    }

    operator fun setValue(thisRef: Any?, property: KProperty<*>, newValue: T) {
        if (!validator(newValue)) {
            throw IllegalArgumentException(errorMessage(newValue))
        }
        value = newValue
    }
}

fun <T> validated(
    initialValue: T,
    validator: (T) -> Boolean,
    errorMessage: (T) -> String = { "Invalid value: $it" }
) = ValidatedProperty(initialValue, validator, errorMessage)
```
